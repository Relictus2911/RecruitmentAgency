using RecruitmentAgency.Data_Access_Layer;
using RecruitmentAgency.Models;
using RecruitmentAgency.Models.ViewModels;
using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Web;
using System.Web.Mvc;

namespace RecruitmentAgency.Controllers
{
    public class CandidateController : Controller
    {
        // GET: Candidate
        [Authorize]
        public ActionResult Candidate()
        {
            return View();
        }

        [Authorize]
        [HttpPost]
        public ActionResult Candidate(CandidateViewModel model, HttpPostedFileBase uploadImage)
        {
            if (ModelState.IsValid)
            {

                byte[] imageData = null;
                if (uploadImage != null)
                {
                    using (var binaryReader = new BinaryReader(uploadImage.InputStream))
                    {
                        imageData = binaryReader.ReadBytes(uploadImage.ContentLength);
                    }
                }
                if (CandidateDAL.Create(new Candidate { FullName = model.FullName, Birthday = model.Birthday, KeyWords = model.KeyWords, Photo = imageData, UserId = UserDAL.GetUserByName(User.Identity.Name), WorkExpirience = model.WorkExpirience }))
                {
                    return View("CandidateDetails", "Candidate", model);
                }
                else
                {
                    ModelState.AddModelError(string.Empty, "Что-то пошло не так, попробуйте ещё раз");
                }
            }
            else
            {
                ModelState.AddModelError(string.Empty, "Registration failed.");
            }
            return View();
        }

        [Authorize]
        public ActionResult CandidateDetails(int id)
        {
            if (CandidateDAL.GetCandidateById(id) != null)
            {
                if (UserDAL.GetUserByName(User.Identity.Name).Role == Enum.Role.candidate && UserDAL.GetUserByName(User.Identity.Name).Id != CandidateDAL.GetCandidateById(id).UserId.Id)
                {
                    return RedirectToAction("Index", "Home");
                }
            }
            var model = CandidateDAL.GetCandidateById(id);
            return View(model);
        }

        [Authorize]
        public ActionResult VacanciesForCandidate(int id)
        {
            if (UserDAL.GetUserByName(User.Identity.Name).Role == Enum.Role.employer)
            {
                return RedirectToAction("Index", "Home");
            }
            var model = VacancyDAL.SearchForVacancy(CandidateDAL.GetCandidateById(id).KeyWords, CandidateDAL.GetCandidateById(id).WorkExpirience);
            return View("VacanciesForCandidate", model);
        }
    }
}
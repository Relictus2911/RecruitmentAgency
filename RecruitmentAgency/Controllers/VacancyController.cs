using RecruitmentAgency.Data_Access_Layer;
using RecruitmentAgency.Models;
using RecruitmentAgency.Models.ViewModels;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.Mvc;

namespace RecruitmentAgency.Controllers
{
    public class VacancyController : Controller
    {
        // GET: Vacancy
        [Authorize]
        public ActionResult VacancyDetails(int id, bool? isOpen)
        {
            if(UserDAL.GetUserByName(User.Identity.Name).Role== Enum.Role.employer && UserDAL.GetUserByName(User.Identity.Name).Id != VacancyDAL.GetVacancyById(id).UserId.Id)
            {
                return View("Index", "Home");
            }
            if (isOpen != null)
            {
                VacancyDAL.CloseVacancy(id, isOpen);
            }
            var vacancy = VacancyDAL.GetVacancyById(id);
            return View("VacancyDetails", vacancy);
        }

        //}

        [Authorize]
        public ActionResult Vacancy()
        {
            return View();
        }
        [HttpPost]
        [Authorize]
        public ActionResult Vacancy(VacancyViewModel model)
        {
            if (ModelState.IsValid)
            {
                VacancyDAL.Create(new Vacancy { Name = model.Name, CompanyName = model.CompanyName, Description = model.Description, Requirements = model.Requirements, Payment = model.Payment, KeyWords = model.KeyWords, CloseTime = model.CloseTime, IsOpen = model.IsOpen, WorkExpirience = model.WorkExpirience, UserId = UserDAL.GetUserByName(User.Identity.Name) });
                ViewBag.Message = "Your contact page.";
                return RedirectToAction("Index", "Home");
            }
            else
            {
                ModelState.AddModelError(string.Empty, "Registration failed.");
            }
            return View();
        }

        [Authorize]
        public ActionResult ListOfVacancies(string sortOrder, string searchStringByName, string searchByPayment, DateTime? searchByDate)
        {
            if(UserDAL.GetUserByName(User.Identity.Name).Role == Enum.Role.employer)
            {
                return View("Index", "Home");
            }
            var vacancies = VacancyDAL.GetVacancies().Select(
                        x =>
                        new VacancyViewModel
                        {
                            Id = x.Id,
                            Name = x.Name,
                            CompanyName = x.CompanyName,
                            Description = x.Description,
                            CloseTime = x.CloseTime,
                            Requirements = x.Requirements,
                            WorkExpirience = x.WorkExpirience,
                            Payment = x.Payment
                        });

            if (!String.IsNullOrEmpty(searchStringByName))
            {
                vacancies = vacancies.Where(s => s.Name.Contains(searchStringByName)
                                       || s.Description.Contains(searchStringByName));
            }
            if (!String.IsNullOrEmpty(searchByPayment))
            {
                vacancies = vacancies.Where(s => int.Parse(s.Payment) > int.Parse(searchByPayment));
            }
            if (searchByDate != null)
            {
                vacancies = vacancies.Where(s => s.CloseTime >= (searchByDate));
            }

            ViewBag.NameSortParm = String.IsNullOrEmpty(sortOrder) ? "Name_desc" : "";
            ViewBag.DescripSortParm = String.IsNullOrEmpty(sortOrder) ? "Descrip_desc" : "Description";
            ViewBag.PaymentSortParm = String.IsNullOrEmpty(sortOrder) ? "Payment_desc" : "Payment";
            ViewBag.CloseDateSortParm = sortOrder == "CloseDate" ? "CloseDate_desc" : "CloseDate";
            ViewBag.IsOpenSortParam = sortOrder == "IsOpen" ? "IsOpen_desc" : "IsOpen";
            switch (sortOrder)
            {
                case "Name_desc":
                    vacancies = vacancies.OrderByDescending(x => x.Name);
                    break;
                case "Descrip_desc":
                    vacancies = vacancies.OrderByDescending(x => x.Description);
                    break;
                case "Description":
                    vacancies = vacancies.OrderByDescending(x => x.Description);
                    break;
                case "Payment_desc":
                    vacancies = vacancies.OrderByDescending(x => x.Payment);
                    break;
                case "Payment":
                    vacancies = vacancies.OrderBy(x => x.Payment);
                    break;
                case "Date":
                    vacancies = vacancies.OrderBy(x => x.CloseTime);
                    break;
                case "CloseDate_desc":
                    vacancies = vacancies.OrderByDescending(x => x.CloseTime);
                    break;
                case "IsOpen":
                    vacancies = vacancies.OrderBy(x => x.IsOpen);
                    break;
                case "IsOpen_desc":
                    vacancies = vacancies.OrderByDescending(x => x.IsOpen);
                    break;
                default:
                    vacancies = vacancies.OrderBy(x => x.Name);
                    break;
            }

            return View("ListOfVacancies", vacancies);
        }

        [Authorize]
        public ActionResult MyVacancies(string sortOrder)
        {
            if (ModelState.IsValid)
            {
                int id = UserDAL.GetUserByName(User.Identity.Name).Id;
                var vacancies = VacancyDAL.GetVacanciesByUserId(id);

                ViewBag.NameSortParm = String.IsNullOrEmpty(sortOrder) ? "Name_desc" : "";
                ViewBag.DescripSortParm = String.IsNullOrEmpty(sortOrder) ? "Descrip_desc" : "Description";
                ViewBag.PaymentSortParm = String.IsNullOrEmpty(sortOrder) ? "Payment_desc" : "Payment";
                ViewBag.CloseDateSortParm = sortOrder == "CloseDate" ? "CloseDate_desc" : "CloseDate";
                ViewBag.IsOpenSortParam = sortOrder == "IsOpen" ? "IsOpen_desc" : "IsOpen";
                switch (sortOrder)
                {
                    case "Name_desc":
                        vacancies = vacancies.OrderByDescending(x => x.Name);
                        break;
                    case "Descrip_desc":
                        vacancies = vacancies.OrderByDescending(x => x.Description);
                        break;
                    case "Description":
                        vacancies = vacancies.OrderByDescending(x => x.Description);
                        break;
                    case "Payment_desc":
                        vacancies = vacancies.OrderByDescending(x => x.Payment);
                        break;
                    case "Payment":
                        vacancies = vacancies.OrderBy(x => x.Payment);
                        break;
                    case "Date":
                        vacancies = vacancies.OrderBy(x => x.CloseTime);
                        break;
                    case "CloseDate_desc":
                        vacancies = vacancies.OrderByDescending(x => x.CloseTime);
                        break;
                    case "IsOpen":
                        vacancies = vacancies.OrderBy(x => x.IsOpen);
                        break;
                    case "IsOpen_desc":
                        vacancies = vacancies.OrderByDescending(x => x.IsOpen);
                        break;
                    default:
                        vacancies = vacancies.OrderBy(x => x.Name);
                        break;
                }
                return View("MyVacancies", vacancies);
            }
            return RedirectToAction("Index", "Home");
        }

        [Authorize]
        public ActionResult CandidateForVacancy(int id)
        {
            if (UserDAL.GetUserByName(User.Identity.Name).Role == Enum.Role.candidate)
            {
                return RedirectToAction("Index", "Home");
            }
            var model = CandidateDAL.SearchForCandidate(VacancyDAL.GetVacancyById(id).KeyWords, VacancyDAL.GetVacancyById(id).WorkExpirience);
            return View("CandidateForVacancy", model);
        }
    }
}
using RecruitmentAgency.Data_Access_Layer;
using RecruitmentAgency.Helpers;
using RecruitmentAgency.Models;
using RecruitmentAgency.Models.ViewModels;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.Mvc;
using System.Web.Security;

namespace RecruitmentAgency.Controllers
{
    public class AccountController : Controller
    {
        // GET: Account
        public ActionResult Register()
        {
            return View();
        }
        [HttpPost]
        [AllowAnonymous]
        [ValidateAntiForgeryToken]
        public ActionResult Register(RegisterViewModel model)
        {
            if(model.Role == Enum.Role.admin)
            {
                ModelState.AddModelError(string.Empty, "Вам нельзя регистрировать пользователей этого типа");
                return View(model);
            }
            if (ModelState.IsValid)
            {
                if (ValidationHelper.CheckUserName(new User { UserName = model.UserName }))
                {
                    if (UserDAL.Create(new User { UserName = model.UserName, Password = ValidationHelper.Encode(model.Password), Role = model.Role }))
                    {
                        if (ValidationHelper.ValidateUser(new User { UserName = model.UserName, Password = ValidationHelper.Encode(model.Password) }))
                        {
                            FormsAuthentication.SetAuthCookie(model.UserName, false);

                            return RedirectToAction("Index", "Home");
                        }
                        else
                        {
                            ModelState.AddModelError(string.Empty, "Войти не удалось");
                        }
                    }
                }
                else
                {
                    ModelState.AddModelError(string.Empty, "Пользователь с таким именем уже существует");
                }
            }
            else
            {
                ModelState.AddModelError(string.Empty, "Регистрация провалилась");
            }

            return View(model);
        }

        public ActionResult Login()
        {
            return View();
        }
        [HttpPost]
        [AllowAnonymous]
        public ActionResult Login(LoginViewModel model)
        {
            if (ModelState.IsValid)
            {
                if (ValidationHelper.ValidateUser(new User { UserName = model.UserName, Password = ValidationHelper.Encode(model.Password) }))
                {
                    FormsAuthentication.SetAuthCookie(model.UserName, false);
                    return RedirectToAction("Index", "Home");
                }
                else
                {
                    ModelState.AddModelError(string.Empty, "Login failed.");
                }
            }
            return View(model);
        }

        public ActionResult LogOff()
        {
            FormsAuthentication.SignOut();
            return RedirectToAction("Index", "Home");
        }
    }
}
using RecruitmentAgency.Data_Access_Layer;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.Mvc;

namespace RecruitmentAgency.Controllers
{
    public class AdminController : Controller
    {
        // GET: Admin
        [Authorize]
        public ActionResult UserList()
        {
            if(UserDAL.GetUserByName(User.Identity.Name).Role != Enum.Role.admin)
            {
                return View("Index", "Home");
            }
            var model = UserDAL.GetUsers();
            return View("UserList",model);
        }
    }
}
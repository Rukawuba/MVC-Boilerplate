using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.Mvc;
using System.Web.Security;
using Reggie.Models;

namespace Reggie.Controllers
{
    public class UserController : Controller
    {
        [HttpGet]
        public ActionResult Login()
        {
            return View();
        }

        [HttpPost]
        public ActionResult Login(User userModel)
        {
            using (DbModels dbModel = new DbModels())
            {
                var user = dbModel.Users.FirstOrDefault(x => x.Username == userModel.Username && x.Password == userModel.Password);
                if (user != null)
                {
                    FormsAuthentication.SetAuthCookie(user.Username, false);
                    return RedirectToAction("Index", "Home");
                }
                else
                {
                    ModelState.AddModelError("", "Invalid username or password.");
                    return View(userModel);
                }
            }
        }

        [HttpGet]
        public ActionResult Logout()
        {
            FormsAuthentication.SignOut();
            return RedirectToAction("Login", "User");
        }

        [Authorize]
        public ActionResult Profile()
        {
            var username = User.Identity.Name;
            var user = dbModel.Users.FirstOrDefault(x => x.Username == username);
            return View(user);
        }

        // ... rest of your AddOrEdit actions ...
    }
}

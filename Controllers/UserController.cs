using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.Mvc;
using Reggie.Models;

namespace Reggie.Controllers
{
    public class UserController : Controller
    {
        [HttpGet]
        public ActionResult AddOrEdit(int id=0)
        {
            User userModel = new User();
            return View(userModel);
        }

        [HttpPost]
        public ActionResult AddOrEdit(User userModel)
        {
            using(DbModels dbModel= new DbModels())
            {
                if (dbModel.Users.Any( x => x.Username == userModel.Username))
                {
                    ViewBag.DuplicateMessage = "Username aready exists.";
                    return View("AddOrEdit", userModel);
                }
                dbModel.Users.Add(userModel);
                dbModel.SaveChanges();
            }
            ModelState.Clear();
            ViewBag.SuccessMessage = "Registration Succesful!";
            return View("AddOrEdit", new User());
        }
    }
}
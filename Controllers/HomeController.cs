using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.Mvc;
using Assigner.Models;

namespace Assigner.Controllers
{
    public class HomeController : Controller
    {
        ApplicationDbContext db = new ApplicationDbContext();   
        public ActionResult Index()
        {
            ViewBag.MajorID = new SelectList(db.Majors, "ID", "MajorString");
            if ((Request.Form["role_id"] == null) || (Request.Form["role_id"].ToLower().Equals("student")))
            {
                ViewBag.RankID = new SelectList(db.RankStudents, "ID", "RankString");
            }
            else if (Request.Form["role_id"].ToLower().Equals("teacher"))
            {
                ViewBag.RankID = new SelectList(db.RankTeachers, "ID", "RankString");
            }
            else
            {
                throw new OperationCanceledException("No proper role is selected. Select a role prior to registering");
            }
            return View();
        }

        public ActionResult About()
        {
            ViewBag.Message = "Your application description page.";

            return View();
        }

        public ActionResult Contact()
        {
            ViewBag.Message = "Your contact page.";

            return View();
        }

        public ActionResult SeeCookie()
        {
            return View();
        }
    }
}
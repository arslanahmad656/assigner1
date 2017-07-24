using System;
using System.Collections.Generic;
using System.Data;
using System.Data.Entity;
using System.Linq;
using System.Net;
using System.Web;
using System.Web.Mvc;
using Assigner.Models;
using Assigner.Models.CoreEntities;
using Assigner.Models.ViewModels;
using Microsoft.AspNet.Identity;

namespace Assigner.Controllers.CoreEntitiesControllers
{
    public class TeacherController : Controller
    {
        private ApplicationDbContext db = new ApplicationDbContext();

        // GET: Teacher
        public ActionResult Index()
        {
            App_Start.Globals.Role = App_Start.Roles.Teacher;
            var teachers = db.Teachers.Include(t => t.Major).Include(t => t.Rank);
            return View(teachers.ToList());
            //return Content("what??");
        }

        // Get: Teacher/Profile
        [Authorize]
        public ActionResult Dashboard()
        {
            var currentlyLoggedInTeacher = GetCurrentLoggedInTeacher();
            if (currentlyLoggedInTeacher == null)
            {
                throw new KeyNotFoundException($"No teacher is logged in. You need to log in as teacher in order to proceed");
            }
            return View(currentlyLoggedInTeacher);
        }

        // GET: Teacher/Details/5
        public ActionResult Details(int? id)
        {
            if (id == null)
            {
                return new HttpStatusCodeResult(HttpStatusCode.BadRequest);
            }
            Teacher teacher = db.Teachers.Find(id);
            if (teacher == null)
            {
                return HttpNotFound();
            }
            return View(teacher);
        }

        // GET: Teacher/Create
        public ActionResult Create()
        {
            ViewBag.MajorID = new SelectList(db.Majors, "ID", "MajorString");
            ViewBag.RankID = new SelectList(db.RankTeachers, "ID", "RankString");
            return View();
        }

        // POST: Teacher/Create
        // To protect from overposting attacks, please enable the specific properties you want to bind to, for 
        // more details see https://go.microsoft.com/fwlink/?LinkId=317598.
        [HttpPost]
        [ValidateAntiForgeryToken]
        public ActionResult Create([Bind(Include = "ID,Name,DateOfBirth,MajorID,RankID")] Teacher teacher)
        {
            if (ModelState.IsValid)
            {
                db.Teachers.Add(teacher);
                db.SaveChanges();
                return RedirectToAction("Index");
            }

            ViewBag.MajorID = new SelectList(db.Majors, "ID", "MajorString", teacher.MajorID);
            ViewBag.RankID = new SelectList(db.RankTeachers, "ID", "RankString", teacher.RankID);
            return View(teacher);
        }

        // GET: Teacher/Edit/5
        public ActionResult Edit(int? id)
        {
            if (id == null)
            {
                return new HttpStatusCodeResult(HttpStatusCode.BadRequest);
            }
            Teacher teacher = db.Teachers.Find(id);
            if (teacher == null)
            {
                return HttpNotFound();
            }
            ViewBag.MajorID = new SelectList(db.Majors, "ID", "MajorString", teacher.MajorID);
            ViewBag.RankID = new SelectList(db.RankTeachers, "ID", "RankString", teacher.RankID);
            return View(teacher);
        }

        // POST: Teacher/Edit/5
        // To protect from overposting attacks, please enable the specific properties you want to bind to, for 
        // more details see https://go.microsoft.com/fwlink/?LinkId=317598.
        [HttpPost]
        [ValidateAntiForgeryToken]
        public ActionResult Edit([Bind(Include = "ID,Name,DateOfBirth,MajorID,RankID")] Teacher teacher)
        {
            if (ModelState.IsValid)
            {
                db.Entry(teacher).State = EntityState.Modified;
                db.SaveChanges();
                return RedirectToAction("Index");
            }
            ViewBag.MajorID = new SelectList(db.Majors, "ID", "MajorString", teacher.MajorID);
            ViewBag.RankID = new SelectList(db.RankTeachers, "ID", "RankString", teacher.RankID);
            return View(teacher);
        }

        // GET: Teacher/Delete/5
        public ActionResult Delete(int? id)
        {
            if (id == null)
            {
                return new HttpStatusCodeResult(HttpStatusCode.BadRequest);
            }
            Teacher teacher = db.Teachers.Find(id);
            if (teacher == null)
            {
                return HttpNotFound();
            }
            return View(teacher);
        }

        // POST: Teacher/Delete/5
        [HttpPost, ActionName("Delete")]
        [ValidateAntiForgeryToken]
        public ActionResult DeleteConfirmed(int id)
        {
            Teacher teacher = db.Teachers.Find(id);
            db.Teachers.Remove(teacher);
            db.SaveChanges();
            //return RedirectToAction("Index");
            return Content($"Teacher with id {id} deleted successfully");
        }

        [HttpPost]
        public ActionResult DeleteTeacher(int id)
        {
            Teacher teacher = db.Teachers.Find(id);
            db.Teachers.Remove(teacher);
            db.SaveChanges();
            System.Threading.Thread.Sleep(500);
            return Content($"Teacher with id {id} deleted successfully");
        }

        // Get: Teacher/_Details/5
        public ActionResult _Details(int? id)
        {
            if (id == null)
            {
                throw new ArgumentNullException("The id was not passed to the action _Details");
            }
            var teacher = db.Teachers.Find(id);
            if (teacher == null)
            {
                throw new ArgumentException($"Could not find teacher with given id {id}");
            }
            System.Threading.Thread.Sleep(500);
            return PartialView("_Details", teacher);
        }

        public ActionResult TeacherTable()
        {
            var teachers = db.Teachers.Include(t => t.Major).Include(t => t.Rank);
            return PartialView("_TeacherTable", teachers.ToList());
        }

        public ActionResult _TeacherTableOnly()
        {
            var teachers = db.Teachers.Include(t => t.Major).Include(t => t.Rank);
            System.Threading.Thread.Sleep(1000);
            return View("_TeacherTableOnly", teachers.ToList());
        }

        public ActionResult _GetRanks()
        {
            ViewBag.RankID = new SelectList(db.RankTeachers, "ID", "RankString");
            return PartialView("_GetRanks", new Teacher());
        }

        public ActionResult _GetMajors()
        {
            ViewBag.MajorID = new SelectList(db.Majors, "ID", "MajorString");
            return PartialView("_GetMajors");
        }

        [Authorize]
        public ActionResult CreatedProjectList()
        {
            var loggedInTeacher = GetCurrentLoggedInTeacher();
            if(loggedInTeacher == null)
            {
                throw new Exception($"Only a logged in teacher can access the list");
            }
            var associatedProjectList = db.Projects.Where(project => project.TeacherID == loggedInTeacher.ID).ToList();
            return View(associatedProjectList);
        }

        [Authorize]
        public ActionResult CreateProject()
        {
            var loggedInTeacher = GetCurrentLoggedInTeacher();
            if(loggedInTeacher == null)
            {
                throw new Exception("Only a logged in teacher may create a project");
            }
            ViewBag.RankID = new SelectList(db.RankStudents, "ID", "RankString");
            return View(new ProjectRegister());
        }

        [HttpPost]
        [ValidateAntiForgeryToken]
        public ActionResult CreateProject([Bind(Include = "Title,Description,RankID")] ProjectRegister projectViewModel)
        {
            if (ModelState.IsValid)
            {
                Project project = new Project
                {
                    CreationDate = DateTime.Now,
                    Description = projectViewModel.Description,
                    RankID = projectViewModel.RankID,
                    TeacherID = GetCurrentLoggedInTeacher().ID,
                    Title = projectViewModel.Title
                };
                db.Projects.Add(project);
                db.SaveChanges();

                //db.Teachers.Add(teacher);
                //db.SaveChanges();
                return RedirectToAction("Index", "Project");
            }
            ViewBag.RankID = new SelectList(db.RankTeachers, "ID", "RankString", projectViewModel.RankID);
            return View(projectViewModel);
        }

        [Authorize]
        public ActionResult ViewSubmissions()
        {
            var loggedInTeacher = GetCurrentLoggedInTeacher();
            if(loggedInTeacher == null)
            {
                throw new Exception($"Only a teacher can view the submitted projects");
            }
            var associatedSubmissions = db.Submissions
                .Where(submission => submission.Project.TeacherID == loggedInTeacher.ID)
                .OrderBy(submission => submission.SubmissionDate);
            return View(associatedSubmissions);
        }

        public ActionResult AjaxTest(int id)
        {
            return Content($"You have sent {id} as id");
        }

        protected override void Dispose(bool disposing)
        {
            if (disposing)
            {
                db.Dispose();
            }
            base.Dispose(disposing);
        }

        private Teacher GetCurrentLoggedInTeacher()
        {
            var loggedInUserId = User.Identity.GetUserId();
            var currentlyLoggedInTeacher = db.Teachers
                .Where(teacher => teacher.ApplicationUserID.Equals(loggedInUserId))
                .First();
            return currentlyLoggedInTeacher;
        }
    }
}

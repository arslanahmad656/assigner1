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
using Microsoft.AspNet.Identity;

namespace Assigner.Controllers.CoreEntitiesControllers
{
    public class StudentController : Controller
    {
        private ApplicationDbContext db = new ApplicationDbContext();

        // GET: Student
        public ActionResult Index()
        {
            App_Start.Globals.Role = App_Start.Roles.Student;
            var students = db.Students.Include(s => s.ApplicationUser).Include(s => s.Major).Include(s => s.Rank);
            return View(students.ToList());
        }

        // GET: Student/Details/5
        public ActionResult Details(int? id)
        {
            if (id == null)
            {
                return new HttpStatusCodeResult(HttpStatusCode.BadRequest);
            }
            Student student = db.Students.Find(id);
            if (student == null)
            {
                return HttpNotFound();
            }
            return View(student);
        }

        // GET: Student/Create
        public ActionResult Create()
        {
            //ViewBag.ApplicationUserID = new SelectList(db.Users, "Id", "Email");
            ViewBag.MajorID = new SelectList(db.Majors, "ID", "MajorString");
            ViewBag.RankID = new SelectList(db.RankStudents, "ID", "RankString");
            return View();
        }

        // POST: Student/Create
        // To protect from overposting attacks, please enable the specific properties you want to bind to, for 
        // more details see https://go.microsoft.com/fwlink/?LinkId=317598.
        [HttpPost]
        [ValidateAntiForgeryToken]
        public ActionResult Create([Bind(Include = "ID,Name,DateOfBirth,MajorID,RankID,ApplicationUserID")] Student student)
        {
            if (ModelState.IsValid)
            {
                db.Students.Add(student);
                db.SaveChanges();
                return RedirectToAction("Index");
            }

            ViewBag.ApplicationUserID = new SelectList(db.Users, "Id", "Email", student.ApplicationUserID);
            ViewBag.MajorID = new SelectList(db.Majors, "ID", "MajorString", student.MajorID);
            ViewBag.RankID = new SelectList(db.RankStudents, "ID", "RankString", student.RankID);
            return View(student);
        }

        // GET: Student/Edit/5
        public ActionResult Edit(int? id)
        {
            if (id == null)
            {
                return new HttpStatusCodeResult(HttpStatusCode.BadRequest);
            }
            Student student = db.Students.Find(id);
            if (student == null)
            {
                return HttpNotFound();
            }
            ViewBag.ApplicationUserID = new SelectList(db.Users, "Id", "Email", student.ApplicationUserID);
            ViewBag.MajorID = new SelectList(db.Majors, "ID", "MajorString", student.MajorID);
            ViewBag.RankID = new SelectList(db.RankStudents, "ID", "RankString", student.RankID);
            return View(student);
        }

        // POST: Student/Edit/5
        // To protect from overposting attacks, please enable the specific properties you want to bind to, for 
        // more details see https://go.microsoft.com/fwlink/?LinkId=317598.
        [HttpPost]
        [ValidateAntiForgeryToken]
        public ActionResult Edit([Bind(Include = "ID,Name,DateOfBirth,MajorID,RankID,ApplicationUserID")] Student student)
        {
            if (ModelState.IsValid)
            {
                db.Entry(student).State = EntityState.Modified;
                db.SaveChanges();
                return RedirectToAction("Index");
            }
            ViewBag.ApplicationUserID = new SelectList(db.Users, "Id", "Email", student.ApplicationUserID);
            ViewBag.MajorID = new SelectList(db.Majors, "ID", "MajorString", student.MajorID);
            ViewBag.RankID = new SelectList(db.RankStudents, "ID", "RankString", student.RankID);
            return View(student);
        }

        // GET: Student/Delete/5
        public ActionResult Delete(int? id)
        {
            if (id == null)
            {
                return new HttpStatusCodeResult(HttpStatusCode.BadRequest);
            }
            Student student = db.Students.Find(id);
            if (student == null)
            {
                return HttpNotFound();
            }
            return View(student);
        }

        // POST: Student/Delete/5
        [HttpPost, ActionName("Delete")]
        [ValidateAntiForgeryToken]
        public ActionResult DeleteConfirmed(int id)
        {
            Student student = db.Students.Find(id);
            db.Students.Remove(student);
            db.SaveChanges();
            return RedirectToAction("Index");
        }

        // Get: Student/_Details/5
        public ActionResult _Details(int? id)
        {
            if (id == null)
            {
                throw new ArgumentNullException("The id was not passed to the action _Details");
            }
            var student = db.Students.Find(id);
            if (student == null)
            {
                throw new ArgumentException($"Could not find student with given id {id}");
            }
            return PartialView("_details", student);
        }

        // Get: Teacher/Profile
        [Authorize]
        public ActionResult Dashboard()
        {
            var currentlyLoggedInStuent = GetCurrentLoggedInStudent();
            if (currentlyLoggedInStuent == null)
            {
                throw new KeyNotFoundException($"No teacher is logged in. You need to log in as teacher in order to proceed");
            }
            return View(currentlyLoggedInStuent);
        }

        [Authorize]
        public ActionResult ProjectList()
        {
            var loggedInStudent = GetCurrentLoggedInStudent();
            if (loggedInStudent == null)
            {
                throw new Exception($"Only a logged in teacher can access the list");
            }
            var associatedProjectList = db.Projects
                .Where(project => project.RankID == loggedInStudent.RankID)
                .OrderBy(project => project.CreationDate)
                .ToList();
            return View(associatedProjectList);
        }

        [Authorize]
        public ActionResult ChosenProjectList()
        {
            var loggedInStudent = GetCurrentLoggedInStudent();
            if (loggedInStudent == null)
            {
                throw new Exception($"Only a logged in teacher can access the list");
            }
            var chosenProjectList = db.StudentChosenProjects
                .Where(chosenProject => chosenProject.StudentID == loggedInStudent.ID)
                .OrderBy(chosenProject => chosenProject.Project.CreationDate)
                .ToList();
            return View(chosenProjectList);
        }

        [Authorize]
        public ActionResult SubmittedProjectList()
        {
            var loggedInStudent = GetCurrentLoggedInStudent();
            if (loggedInStudent == null)
            {
                throw new Exception($"Only a logged in teacher can access the list");
            }
            return RedirectToAction("SubmittedProjectsByStudentId", "Submission", new { id = loggedInStudent.ID });
        }

        private Student GetCurrentLoggedInStudent()
        {
            var loggedInUserId = User.Identity.GetUserId();
            var currentlyLoggedInStudent = db.Students
                .Where(student => student.ApplicationUserID.Equals(loggedInUserId))
                .First();
            return currentlyLoggedInStudent;
        }

        protected override void Dispose(bool disposing)
        {
            if (disposing)
            {
                db.Dispose();
            }
            base.Dispose(disposing);
        }
    }
}

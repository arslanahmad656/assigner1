using System;
using System.Collections.Generic;
using System.Data;
using System.Data.Entity;
using System.Linq;
using System.Net;
using System.Web;
using System.Web.Mvc;
using Assigner.Models;
using Assigner.Models.CoreEntities.BridgeEntities;
using Microsoft.AspNet.Identity;

namespace Assigner.Controllers.CoreEntitiesControllers.BridgeEntitiesControllers
{
    public class StudentChosenProjectController : Controller
    {
        private ApplicationDbContext db = new ApplicationDbContext();

        // GET: StudentChosenProject
        public ActionResult Index()
        {
            var studentChosenProjects = db.StudentChosenProjects.Include(s => s.Project).Include(s => s.Student);
            return View(studentChosenProjects.ToList());
        }

        // GET: StudentChosenProject/Details/5
        public ActionResult Details(int? id)
        {
            if (id == null)
            {
                return new HttpStatusCodeResult(HttpStatusCode.BadRequest);
            }
            StudentChosenProject studentChosenProject = db.StudentChosenProjects.Find(id);
            if (studentChosenProject == null)
            {
                return HttpNotFound();
            }
            return View(studentChosenProject);
        }

        // GET: StudentChosenProject/Create
        public ActionResult Create()
        {
            ViewBag.ProjectID = new SelectList(db.Projects, "ID", "Title");
            ViewBag.StudentID = new SelectList(db.Students, "ID", "Name");
            return View();
        }

        [Authorize]
        [HttpPost]
        public ActionResult CreateProject(int id)
        {
            var loggedInUserId = User.Identity.GetUserId();
            var loggedInStudent = db.Students
                .Where(student => student.ApplicationUserID.Equals(loggedInUserId))
                .First();
            if (loggedInUserId == null)
            {
                return new HttpStatusCodeResult(HttpStatusCode.Unauthorized, "Only a logged in student is allowed to choose a project");
            }
            var selectedProject = db.Projects
                .Where(project => project.ID == id)
                .First();
            if(selectedProject == null)
            {
                return new HttpStatusCodeResult(HttpStatusCode.NotFound, "The project either doen't exist or it can't be chosen");
            }
            var chosenProject = new StudentChosenProject()
            {
                ProjectID = id,
                StudentID = loggedInStudent.ID,
                Submitted = false

            };
            db.StudentChosenProjects.Add(chosenProject);
            db.SaveChanges();
            return new HttpStatusCodeResult(200, $"Project with ID {id} has been successfuly chosen by student with Id {loggedInStudent.ID}");
        }

        // POST: StudentChosenProject/Create
        // To protect from overposting attacks, please enable the specific properties you want to bind to, for 
        // more details see https://go.microsoft.com/fwlink/?LinkId=317598.
        [HttpPost]
        [ValidateAntiForgeryToken]
        public ActionResult Create([Bind(Include = "ID,StudentID,ProjectID,Submitted")] StudentChosenProject studentChosenProject)
        {
            if (ModelState.IsValid)
            {
                db.StudentChosenProjects.Add(studentChosenProject);
                db.SaveChanges();
                return RedirectToAction("Index");
            }

            ViewBag.ProjectID = new SelectList(db.Projects, "ID", "Title", studentChosenProject.ProjectID);
            ViewBag.StudentID = new SelectList(db.Students, "ID", "Name", studentChosenProject.StudentID);
            return View(studentChosenProject);
        }

        // GET: StudentChosenProject/Edit/5
        public ActionResult Edit(int? id)
        {
            if (id == null)
            {
                return new HttpStatusCodeResult(HttpStatusCode.BadRequest);
            }
            StudentChosenProject studentChosenProject = db.StudentChosenProjects.Find(id);
            if (studentChosenProject == null)
            {
                return HttpNotFound();
            }
            ViewBag.ProjectID = new SelectList(db.Projects, "ID", "Title", studentChosenProject.ProjectID);
            ViewBag.StudentID = new SelectList(db.Students, "ID", "Name", studentChosenProject.StudentID);
            return View(studentChosenProject);
        }

        // POST: StudentChosenProject/Edit/5
        // To protect from overposting attacks, please enable the specific properties you want to bind to, for 
        // more details see https://go.microsoft.com/fwlink/?LinkId=317598.
        [HttpPost]
        [ValidateAntiForgeryToken]
        public ActionResult Edit([Bind(Include = "ID,StudentID,ProjectID,Submitted")] StudentChosenProject studentChosenProject)
        {
            if (ModelState.IsValid)
            {
                db.Entry(studentChosenProject).State = EntityState.Modified;
                db.SaveChanges();
                return RedirectToAction("Index");
            }
            ViewBag.ProjectID = new SelectList(db.Projects, "ID", "Title", studentChosenProject.ProjectID);
            ViewBag.StudentID = new SelectList(db.Students, "ID", "Name", studentChosenProject.StudentID);
            return View(studentChosenProject);
        }

        // GET: StudentChosenProject/Delete/5
        public ActionResult Delete(int? id)
        {
            if (id == null)
            {
                return new HttpStatusCodeResult(HttpStatusCode.BadRequest);
            }
            StudentChosenProject studentChosenProject = db.StudentChosenProjects.Find(id);
            if (studentChosenProject == null)
            {
                return HttpNotFound();
            }
            return View(studentChosenProject);
        }

        // POST: StudentChosenProject/Delete/5
        [HttpPost, ActionName("Delete")]
        [ValidateAntiForgeryToken]
        public ActionResult DeleteConfirmed(int id)
        {
            StudentChosenProject studentChosenProject = db.StudentChosenProjects.Find(id);
            db.StudentChosenProjects.Remove(studentChosenProject);
            db.SaveChanges();
            return RedirectToAction("Index");
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

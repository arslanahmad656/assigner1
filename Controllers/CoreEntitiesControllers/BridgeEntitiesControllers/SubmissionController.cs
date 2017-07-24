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
using Assigner.Models.CoreEntities;

namespace Assigner.Controllers.CoreEntitiesControllers.BridgeEntitiesControllers
{
    public class SubmissionController : Controller
    {
        private ApplicationDbContext db = new ApplicationDbContext();

        // GET: Submission
        public ActionResult Index()
        {
            var submissions = db.Submissions.Include(s => s.Project).Include(s => s.Student);
            return View(submissions.ToList());
        }

        [Authorize]
        public ActionResult SubmittedProjectsByStudentId(int id)
        {
            var loggedInStudent = GetCurrentLoggedInStudent();
            if (loggedInStudent == null)
            {
                throw new Exception($"Only a logged in student can access the list");
            }

            var submissions = db.Submissions
                .Where(submission => submission.StudentID == loggedInStudent.ID)
                .OrderBy(submission => submission.SubmissionDate)
                .Distinct()
                .ToList();
            return View(submissions);
        }

        // GET: Submission/Details/5
        public ActionResult Details(int? id)
        {
            if (id == null)
            {
                return new HttpStatusCodeResult(HttpStatusCode.BadRequest);
            }
            Submission submission = db.Submissions.Find(id);
            if (submission == null)
            {
                return HttpNotFound();
            }
            return View(submission);
        }

        // GET: Submission/Create
        public ActionResult Create()
        {
            ViewBag.ProjectID = new SelectList(db.Projects, "ID", "Title");
            ViewBag.StudentID = new SelectList(db.Students, "ID", "Name");
            return View();
        }

        // POST: Submission/Create
        // To protect from overposting attacks, please enable the specific properties you want to bind to, for 
        // more details see https://go.microsoft.com/fwlink/?LinkId=317598.
        [HttpPost]
        [ValidateAntiForgeryToken]
        public ActionResult Create([Bind(Include = "ID,StudentID,ProjectID,SubmissionDate")] Submission submission)
        {
            if (ModelState.IsValid)
            {
                db.Submissions.Add(submission);
                db.SaveChanges();
                return RedirectToAction("Index");
            }

            ViewBag.ProjectID = new SelectList(db.Projects, "ID", "Title", submission.ProjectID);
            ViewBag.StudentID = new SelectList(db.Students, "ID", "Name", submission.StudentID);
            return View(submission);
        }

        // GET: Submission/Edit/5
        public ActionResult Edit(int? id)
        {
            if (id == null)
            {
                return new HttpStatusCodeResult(HttpStatusCode.BadRequest);
            }
            Submission submission = db.Submissions.Find(id);
            if (submission == null)
            {
                return HttpNotFound();
            }
            ViewBag.ProjectID = new SelectList(db.Projects, "ID", "Title", submission.ProjectID);
            ViewBag.StudentID = new SelectList(db.Students, "ID", "Name", submission.StudentID);
            return View(submission);
        }

        // POST: Submission/Edit/5
        // To protect from overposting attacks, please enable the specific properties you want to bind to, for 
        // more details see https://go.microsoft.com/fwlink/?LinkId=317598.
        [HttpPost]
        [ValidateAntiForgeryToken]
        public ActionResult Edit([Bind(Include = "ID,StudentID,ProjectID,SubmissionDate")] Submission submission)
        {
            if (ModelState.IsValid)
            {
                db.Entry(submission).State = EntityState.Modified;
                db.SaveChanges();
                return RedirectToAction("Index");
            }
            ViewBag.ProjectID = new SelectList(db.Projects, "ID", "Title", submission.ProjectID);
            ViewBag.StudentID = new SelectList(db.Students, "ID", "Name", submission.StudentID);
            return View(submission);
        }

        // GET: Submission/Delete/5
        public ActionResult Delete(int? id)
        {
            if (id == null)
            {
                return new HttpStatusCodeResult(HttpStatusCode.BadRequest);
            }
            Submission submission = db.Submissions.Find(id);
            if (submission == null)
            {
                return HttpNotFound();
            }
            return View(submission);
        }

        // POST: Submission/Delete/5
        [HttpPost, ActionName("Delete")]
        [ValidateAntiForgeryToken]
        public ActionResult DeleteConfirmed(int id)
        {
            Submission submission = db.Submissions.Find(id);
            db.Submissions.Remove(submission);
            db.SaveChanges();
            return RedirectToAction("Index");
        }

        [Authorize]
        [HttpPost]
        public ActionResult SubmitProject(int id)
        {
            var loggedInStudent = GetCurrentLoggedInStudent();
            if (loggedInStudent == null)
            {
                throw new Exception($"Only a logged in student can access the list");
            }
            var submission = new Submission
            {
                ProjectID = id,
                StudentID = loggedInStudent.ID,
                SubmissionDate = DateTime.Now
            };

            db.Submissions.Add(submission);
            db.SaveChanges();

            System.Threading.Thread.Sleep(1500);

            return new HttpStatusCodeResult(HttpStatusCode.OK, "Submitted Successfully");
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

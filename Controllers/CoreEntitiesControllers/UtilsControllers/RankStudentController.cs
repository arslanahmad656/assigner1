using System;
using System.Collections.Generic;
using System.Data;
using System.Data.Entity;
using System.Linq;
using System.Net;
using System.Web;
using System.Web.Mvc;
using Assigner.Models;
using Assigner.Models.CoreEntities.Utils;

namespace Assigner.Controllers.CoreEntitiesControllers.UtilsControllers
{
    public class RankStudentController : Controller
    {
        private ApplicationDbContext db = new ApplicationDbContext();

        // GET: RankStudent
        public ActionResult Index()
        {
            return View(db.RankStudents.ToList());
        }

        // GET: RankStudent/Details/5
        public ActionResult Details(int? id)
        {
            if (id == null)
            {
                return new HttpStatusCodeResult(HttpStatusCode.BadRequest);
            }
            RankStudent rankStudent = db.RankStudents.Find(id);
            if (rankStudent == null)
            {
                return HttpNotFound();
            }
            return View(rankStudent);
        }

        // GET: RankStudent/Create
        public ActionResult Create()
        {
            return View();
        }

        // POST: RankStudent/Create
        // To protect from overposting attacks, please enable the specific properties you want to bind to, for 
        // more details see https://go.microsoft.com/fwlink/?LinkId=317598.
        [HttpPost]
        [ValidateAntiForgeryToken]
        public ActionResult Create([Bind(Include = "ID,Rank")] RankStudent rankStudent)
        {
            if (ModelState.IsValid)
            {
                db.RankStudents.Add(rankStudent);
                db.SaveChanges();
                return RedirectToAction("Index");
            }

            return View(rankStudent);
        }

        // GET: RankStudent/Edit/5
        public ActionResult Edit(int? id)
        {
            if (id == null)
            {
                return new HttpStatusCodeResult(HttpStatusCode.BadRequest);
            }
            RankStudent rankStudent = db.RankStudents.Find(id);
            if (rankStudent == null)
            {
                return HttpNotFound();
            }
            return View(rankStudent);
        }

        // POST: RankStudent/Edit/5
        // To protect from overposting attacks, please enable the specific properties you want to bind to, for 
        // more details see https://go.microsoft.com/fwlink/?LinkId=317598.
        [HttpPost]
        [ValidateAntiForgeryToken]
        public ActionResult Edit([Bind(Include = "ID,Rank")] RankStudent rankStudent)
        {
            if (ModelState.IsValid)
            {
                db.Entry(rankStudent).State = EntityState.Modified;
                db.SaveChanges();
                return RedirectToAction("Index");
            }
            return View(rankStudent);
        }

        // GET: RankStudent/Delete/5
        public ActionResult Delete(int? id)
        {
            if (id == null)
            {
                return new HttpStatusCodeResult(HttpStatusCode.BadRequest);
            }
            RankStudent rankStudent = db.RankStudents.Find(id);
            if (rankStudent == null)
            {
                return HttpNotFound();
            }
            return View(rankStudent);
        }

        // POST: RankStudent/Delete/5
        [HttpPost, ActionName("Delete")]
        [ValidateAntiForgeryToken]
        public ActionResult DeleteConfirmed(int id)
        {
            RankStudent rankStudent = db.RankStudents.Find(id);
            db.RankStudents.Remove(rankStudent);
            db.SaveChanges();
            return RedirectToAction("Index");
        }

        public ActionResult RandomRank()
        {
            return PartialView("_RandomRank", GetRandomRank());
        }

        public ActionResult SomeContent()
        {
            return Content("This is a content");
        }

        public ActionResult Practice()
        {
            return View();
        }

        private RankStudent GetRandomRank()
        {
            return db.RankStudents.OrderBy(t => Guid.NewGuid()).First();
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

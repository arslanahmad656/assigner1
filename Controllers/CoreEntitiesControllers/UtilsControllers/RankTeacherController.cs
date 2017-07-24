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
    public class RankTeacherController : Controller
    {
        private ApplicationDbContext db = new ApplicationDbContext();

        // GET: RankTeacher
        public ActionResult Index()
        {
            return View(db.RankTeachers.ToList());
        }

        // GET: RankTeacher/Details/5
        public ActionResult Details(int? id)
        {
            if (id == null)
            {
                return new HttpStatusCodeResult(HttpStatusCode.BadRequest);
            }
            RankTeacher rankTeacher = db.RankTeachers.Find(id);
            if (rankTeacher == null)
            {
                return HttpNotFound();
            }
            return View(rankTeacher);
        }

        // GET: RankTeacher/Create
        public ActionResult Create()
        {
            return View();
        }

        // POST: RankTeacher/Create
        // To protect from overposting attacks, please enable the specific properties you want to bind to, for 
        // more details see https://go.microsoft.com/fwlink/?LinkId=317598.
        [HttpPost]
        [ValidateAntiForgeryToken]
        public ActionResult Create([Bind(Include = "ID,Rank")] RankTeacher rankTeacher)
        {
            if (ModelState.IsValid)
            {
                db.RankTeachers.Add(rankTeacher);
                db.SaveChanges();
                return RedirectToAction("Index");
            }

            return View(rankTeacher);
        }

        // GET: RankTeacher/Edit/5
        public ActionResult Edit(int? id)
        {
            if (id == null)
            {
                return new HttpStatusCodeResult(HttpStatusCode.BadRequest);
            }
            RankTeacher rankTeacher = db.RankTeachers.Find(id);
            if (rankTeacher == null)
            {
                return HttpNotFound();
            }
            return View(rankTeacher);
        }

        // POST: RankTeacher/Edit/5
        // To protect from overposting attacks, please enable the specific properties you want to bind to, for 
        // more details see https://go.microsoft.com/fwlink/?LinkId=317598.
        [HttpPost]
        [ValidateAntiForgeryToken]
        public ActionResult Edit([Bind(Include = "ID,Rank")] RankTeacher rankTeacher)
        {
            if (ModelState.IsValid)
            {
                db.Entry(rankTeacher).State = EntityState.Modified;
                db.SaveChanges();
                return RedirectToAction("Index");
            }
            return View(rankTeacher);
        }

        // GET: RankTeacher/Delete/5
        public ActionResult Delete(int? id)
        {
            if (id == null)
            {
                return new HttpStatusCodeResult(HttpStatusCode.BadRequest);
            }
            RankTeacher rankTeacher = db.RankTeachers.Find(id);
            if (rankTeacher == null)
            {
                return HttpNotFound();
            }
            return View(rankTeacher);
        }

        // POST: RankTeacher/Delete/5
        [HttpPost, ActionName("Delete")]
        [ValidateAntiForgeryToken]
        public ActionResult DeleteConfirmed(int id)
        {
            RankTeacher rankTeacher = db.RankTeachers.Find(id);
            db.RankTeachers.Remove(rankTeacher);
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

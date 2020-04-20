using System;
using System.Collections.Generic;
using System.Data;
using System.Data.Entity;
using System.Linq;
using System.Net;
using System.Web;
using System.Web.Mvc;
using DashReportingTool.Models;

namespace DashReportingTool.Areas.Manage.Controllers
{
    [AuthenticateFilter(Roles = "admin,manager,team lead")]

    public class CourseMastersController : Controller
    {
        private dbReportingEntities db = new dbReportingEntities();

        // GET: Manage/CourseMasters
        public ActionResult Index()
        {
            return View(db.CourseMasters.ToList());
        }

        // GET: Manage/CourseMasters/Details/5
        public ActionResult Details(int? id)
        {
            if (id == null)
            {
                return new HttpStatusCodeResult(HttpStatusCode.BadRequest);
            }
            CourseMaster courseMaster = db.CourseMasters.Find(id);
            if (courseMaster == null)
            {
                return HttpNotFound();
            }
            return View(courseMaster);
        }

        // GET: Manage/CourseMasters/Create
        public ActionResult Create()
        {
            return View();
        }

        // POST: Manage/CourseMasters/Create
        // To protect from overposting attacks, please enable the specific properties you want to bind to, for 
        // more details see https://go.microsoft.com/fwlink/?LinkId=317598.
        [HttpPost]
        [ValidateAntiForgeryToken]
        public ActionResult Create([Bind(Include = "CourseId,CourseTitle,Duration,Fees,CourseContent,IsCourseEnabled")] CourseMaster courseMaster)
        {
            try
            {
                if (ModelState.IsValid)
                {
                    db.CourseMasters.Add(courseMaster);
                    db.SaveChanges();
                    return RedirectToAction("Index");
                }

                return View(courseMaster);
            }
            catch (Exception ex)
            {
                TempData["err"] = ex.Message;
                return RedirectToAction("Index");
            }
          
        }

        // GET: Manage/CourseMasters/Edit/5
        public ActionResult Edit(int? id)
        {
            if (id == null)
            {
                return new HttpStatusCodeResult(HttpStatusCode.BadRequest);
            }
            CourseMaster courseMaster = db.CourseMasters.Find(id);
            if (courseMaster == null)
            {
                return HttpNotFound();
            }
            return View(courseMaster);
        }

        // POST: Manage/CourseMasters/Edit/5
        // To protect from overposting attacks, please enable the specific properties you want to bind to, for 
        // more details see https://go.microsoft.com/fwlink/?LinkId=317598.
        [HttpPost]
        [ValidateAntiForgeryToken]
        public ActionResult Edit([Bind(Include = "CourseId,CourseTitle,Duration,Fees,CourseContent,IsCourseEnabled")] CourseMaster courseMaster)
        {
            try
            {

                if (ModelState.IsValid)
                {
                    db.Entry(courseMaster).State = EntityState.Modified;
                    db.SaveChanges();
                    return RedirectToAction("Index");
                }
                return View(courseMaster);
            }
            catch (Exception ex)
            {
                TempData["err"] = ex.Message;
                return RedirectToAction("Index");
            }
        }

        // GET: Manage/CourseMasters/Delete/5
        public ActionResult Delete(int? id)
        {
            if (id == null)
            {
                return new HttpStatusCodeResult(HttpStatusCode.BadRequest);
            }
            CourseMaster courseMaster = db.CourseMasters.Find(id);
            if (courseMaster == null)
            {
                return HttpNotFound();
            }
            return View(courseMaster);
        }

        // POST: Manage/CourseMasters/Delete/5
        [HttpPost, ActionName("Delete")]
        [ValidateAntiForgeryToken]
        public ActionResult DeleteConfirmed(int id)
        {
            CourseMaster courseMaster = db.CourseMasters.Find(id);
            db.CourseMasters.Remove(courseMaster);
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

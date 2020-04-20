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

    public class CandidateCourseMastersController : Controller
    {
        private dbReportingEntities db = new dbReportingEntities();

        // GET: Manage/CandidateCourseMasters
        public ActionResult Index()
        {
            var candidateCourseMasters = db.CandidateCourseMasters.Include(c => c.CandidateMaster).Include(c => c.CourseMaster);
            return View(candidateCourseMasters.ToList());
        }

        // GET: Manage/CandidateCourseMasters/Details/5
        public ActionResult Details(int? id)
        {
            if (id == null)
            {
                return new HttpStatusCodeResult(HttpStatusCode.BadRequest);
            }
            CandidateCourseMaster candidateCourseMaster = db.CandidateCourseMasters.Find(id);
            if (candidateCourseMaster == null)
            {
                return HttpNotFound();
            }
            return View(candidateCourseMaster);
        }

        // GET: Manage/CandidateCourseMasters/Create
        public ActionResult Create()
        {
            ViewBag.RefCandidateTitle = new SelectList(db.CandidateMasters.OrderBy(a=>a.CandidateName), "CandidateId", "CandidateName");
            ViewBag.RefCourseTitle = new SelectList(db.CourseMasters, "CourseId", "CourseTitle");
            return View();
        }

        // POST: Manage/CandidateCourseMasters/Create
        // To protect from overposting attacks, please enable the specific properties you want to bind to, for 
        // more details see https://go.microsoft.com/fwlink/?LinkId=317598.
        [HttpPost]
        [ValidateAntiForgeryToken]
        public ActionResult Create([Bind(Include = "CCId,RefCandidateTitle,RefCourseTitle")] CandidateCourseMaster candidateCourseMaster)
        {
            try
            {
                if (ModelState.IsValid)
                {
                    db.CandidateCourseMasters.Add(candidateCourseMaster);
                    db.SaveChanges();
                    return RedirectToAction("Index");
                }

                ViewBag.RefCandidateTitle = new SelectList(db.CandidateMasters, "CandidateId", "CandidateName", candidateCourseMaster.RefCandidateTitle);
                ViewBag.RefCourseTitle = new SelectList(db.CourseMasters, "CourseId", "CourseTitle", candidateCourseMaster.RefCourseTitle);
                return View(candidateCourseMaster);
            }
            catch (Exception ex)
            {
                TempData["err"] = ex.Message;
                return RedirectToAction("Index");
            }
           
        }

        // GET: Manage/CandidateCourseMasters/Edit/5
        public ActionResult Edit(int? id)
        {
            if (id == null)
            {
                return new HttpStatusCodeResult(HttpStatusCode.BadRequest);
            }
            CandidateCourseMaster candidateCourseMaster = db.CandidateCourseMasters.Find(id);
            if (candidateCourseMaster == null)
            {
                return HttpNotFound();
            }
            ViewBag.RefCandidateTitle = new SelectList(db.CandidateMasters, "CandidateId", "CandidateName", candidateCourseMaster.RefCandidateTitle);
            ViewBag.RefCourseTitle = new SelectList(db.CourseMasters, "CourseId", "CourseTitle", candidateCourseMaster.RefCourseTitle);
            return View(candidateCourseMaster);
        }

        // POST: Manage/CandidateCourseMasters/Edit/5
        // To protect from overposting attacks, please enable the specific properties you want to bind to, for 
        // more details see https://go.microsoft.com/fwlink/?LinkId=317598.
        [HttpPost]
        [ValidateAntiForgeryToken]
        public ActionResult Edit([Bind(Include = "CCId,RefCandidateTitle,RefCourseTitle")] CandidateCourseMaster candidateCourseMaster)
        {
            try
            {
                if (ModelState.IsValid)
                {
                    db.Entry(candidateCourseMaster).State = EntityState.Modified;
                    db.SaveChanges();
                    return RedirectToAction("Index");
                }
                ViewBag.RefCandidateTitle = new SelectList(db.CandidateMasters, "CandidateId", "CandidateName", candidateCourseMaster.RefCandidateTitle);
                ViewBag.RefCourseTitle = new SelectList(db.CourseMasters, "CourseId", "CourseTitle", candidateCourseMaster.RefCourseTitle);
                return View(candidateCourseMaster);
            }
            catch (Exception ex)
            {
                TempData["err"] = ex.Message;
                return RedirectToAction("Index");
            }
           
        }

        // GET: Manage/CandidateCourseMasters/Delete/5
        public ActionResult Delete(int? id)
        {
            if (id == null)
            {
                return new HttpStatusCodeResult(HttpStatusCode.BadRequest);
            }
            CandidateCourseMaster candidateCourseMaster = db.CandidateCourseMasters.Find(id);
            if (candidateCourseMaster == null)
            {
                return HttpNotFound();
            }
            return View(candidateCourseMaster);
        }

        // POST: Manage/CandidateCourseMasters/Delete/5
        [HttpPost, ActionName("Delete")]
        [ValidateAntiForgeryToken]
        public ActionResult DeleteConfirmed(int id)
        {
            CandidateCourseMaster candidateCourseMaster = db.CandidateCourseMasters.Find(id);
            db.CandidateCourseMasters.Remove(candidateCourseMaster);
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

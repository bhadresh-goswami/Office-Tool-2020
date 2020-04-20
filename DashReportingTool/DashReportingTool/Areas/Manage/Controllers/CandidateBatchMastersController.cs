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
    class mylist
    {
        public int CCId { get; set; }
        public string Name { get; set; }
    }

    [AuthenticateFilter(Roles = "admin,manager,team lead")]

    public class CandidateBatchMastersController : Controller
    {
        private dbReportingEntities db = new dbReportingEntities();

        // GET: Manage/CandidateBatchMasters
        public ActionResult Index()
        {
            var candidateBatchMasters = db.CandidateBatchMasters.Include(c => c.BatchMaster).Include(c => c.CandidateCourseMaster);
            return View(candidateBatchMasters.ToList());
        }

        // GET: Manage/CandidateBatchMasters/Details/5
        public ActionResult Details(int? id)
        {
            if (id == null)
            {
                return new HttpStatusCodeResult(HttpStatusCode.BadRequest);
            }
            CandidateBatchMaster candidateBatchMaster = db.CandidateBatchMasters.Find(id);
            if (candidateBatchMaster == null)
            {
                return HttpNotFound();
            }
            return View(candidateBatchMaster);
        }

        // GET: Manage/CandidateBatchMasters/Create
        public ActionResult Create()
        {
            List<mylist> list = new List<mylist>();
            foreach (var item in db.CandidateCourseMasters)
            {
                var str = item.CandidateMaster.CandidateName + " " + item.CourseMaster.CourseTitle;
                list.Add(new mylist()
                {
                    CCId = item.CCId,
                    Name = str
                });
                
            }
            ViewBag.RefCandidateCourseId = new SelectList(list, "CCId", "Name");
            ViewBag.RefBatchTitle = new SelectList(db.BatchMasters, "BatchId", "BatchName");
            
            return View();
        }

        // POST: Manage/CandidateBatchMasters/Create
        // To protect from overposting attacks, please enable the specific properties you want to bind to, for 
        // more details see https://go.microsoft.com/fwlink/?LinkId=317598.
        [HttpPost]
        [ValidateAntiForgeryToken]
        public ActionResult Create([Bind(Include = "CBId,RefCandidateCourseId,RefBatchTitle")] CandidateBatchMaster candidateBatchMaster)
        {
            if (ModelState.IsValid)
            {
                db.CandidateBatchMasters.Add(candidateBatchMaster);
                db.SaveChanges();
                return RedirectToAction("Index");
            }

            List<mylist> list = new List<mylist>();
            foreach (var item in db.CandidateCourseMasters)
            {
                var str = item.CandidateMaster.CandidateName + " " + item.CourseMaster.CourseTitle;
                list.Add(new mylist()
                {
                    CCId = item.CCId,
                    Name = str
                });

            }
            ViewBag.RefCandidateCourseId = new SelectList(list, "CCId", "Name");
            ViewBag.RefBatchTitle = new SelectList(db.BatchMasters, "BatchId", "BatchName", candidateBatchMaster.RefBatchTitle);
           // ViewBag.RefCandidateCourseId = new SelectList(db.CandidateCourseMasters, "CCId", "CCId", candidateBatchMaster.RefCandidateCourseId);
            return View(candidateBatchMaster);
        }

        // GET: Manage/CandidateBatchMasters/Edit/5
        public ActionResult Edit(int? id)
        {
            if (id == null)
            {
                return new HttpStatusCodeResult(HttpStatusCode.BadRequest);
            }
            CandidateBatchMaster candidateBatchMaster = db.CandidateBatchMasters.Find(id);
            if (candidateBatchMaster == null)
            {
                return HttpNotFound();
            }

            List<mylist> list = new List<mylist>();
            foreach (var item in db.CandidateCourseMasters)
            {
                var str = item.CandidateMaster.CandidateName + " " + item.CourseMaster.CourseTitle;
                list.Add(new mylist()
                {
                    CCId = item.CCId,
                    Name = str
                });

            }
            ViewBag.RefCandidateCourseId = new SelectList(list, "CCId", "Name");
            ViewBag.RefBatchTitle = new SelectList(db.BatchMasters, "BatchId", "BatchName", candidateBatchMaster.RefBatchTitle);
            //ViewBag.RefCandidateCourseId = new SelectList(db.CandidateCourseMasters, "CCId", "CCId", candidateBatchMaster.RefCandidateCourseId);
            return View(candidateBatchMaster);
        }

        // POST: Manage/CandidateBatchMasters/Edit/5
        // To protect from overposting attacks, please enable the specific properties you want to bind to, for 
        // more details see https://go.microsoft.com/fwlink/?LinkId=317598.
        [HttpPost]
        [ValidateAntiForgeryToken]
        public ActionResult Edit([Bind(Include = "CBId,RefCandidateCourseId,RefBatchTitle")] CandidateBatchMaster candidateBatchMaster)
        {
            if (ModelState.IsValid)
            {
                db.Entry(candidateBatchMaster).State = EntityState.Modified;
                db.SaveChanges();
                return RedirectToAction("Index");
            }

            List<mylist> list = new List<mylist>();
            foreach (var item in db.CandidateCourseMasters)
            {
                var str = item.CandidateMaster.CandidateName + " " + item.CourseMaster.CourseTitle;
                list.Add(new mylist()
                {
                    CCId = item.CCId,
                    Name = str
                });

            }
            ViewBag.RefCandidateCourseId = new SelectList(list, "CCId", "Name");
            ViewBag.RefBatchTitle = new SelectList(db.BatchMasters, "BatchId", "BatchName", candidateBatchMaster.RefBatchTitle);
            //ViewBag.RefCandidateCourseId = new SelectList(db.CandidateCourseMasters, "CCId", "CCId", candidateBatchMaster.RefCandidateCourseId);
            return View(candidateBatchMaster);
        }

        // GET: Manage/CandidateBatchMasters/Delete/5
        public ActionResult Delete(int? id)
        {
            if (id == null)
            {
                return new HttpStatusCodeResult(HttpStatusCode.BadRequest);
            }
            CandidateBatchMaster candidateBatchMaster = db.CandidateBatchMasters.Find(id);
            if (candidateBatchMaster == null)
            {
                return HttpNotFound();
            }
            return View(candidateBatchMaster);
        }

        // POST: Manage/CandidateBatchMasters/Delete/5
        [HttpPost, ActionName("Delete")]
        [ValidateAntiForgeryToken]
        public ActionResult DeleteConfirmed(int id)
        {
            CandidateBatchMaster candidateBatchMaster = db.CandidateBatchMasters.Find(id);
            db.CandidateBatchMasters.Remove(candidateBatchMaster);
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

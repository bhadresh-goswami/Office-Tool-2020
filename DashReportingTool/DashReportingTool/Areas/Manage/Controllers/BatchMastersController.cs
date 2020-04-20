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
    public class BatchMastersController : Controller
    {
        private dbReportingEntities db = new dbReportingEntities();

        // GET: Manage/BatchMasters
        public ActionResult Index()
        {
            var batchMasters = db.BatchMasters.Include(b => b.StatusMaster).Include(b => b.ExpertMaster);
            return View(batchMasters.ToList());
        }

        // GET: Manage/BatchMasters/Details/5
        public ActionResult Details(int? id)
        {
            if (id == null)
            {
                return new HttpStatusCodeResult(HttpStatusCode.BadRequest);
            }
            BatchMaster batchMaster = db.BatchMasters.Find(id);
            if (batchMaster == null)
            {
                return HttpNotFound();
            }
            return View(batchMaster);
        }

        // GET: Manage/BatchMasters/Create
        public ActionResult Create()
        {
            ViewBag.BatchStatus = new SelectList(db.StatusMasters, "StatusId", "StatusTitle");
            ViewBag.RefExpertName = new SelectList(db.ExpertMasters, "ExpertId", "ExpertName");
            return View();
        }

        // POST: Manage/BatchMasters/Create
        // To protect from overposting attacks, please enable the specific properties you want to bind to, for 
        // more details see https://go.microsoft.com/fwlink/?LinkId=317598.
        [HttpPost]
        [ValidateAntiForgeryToken]
        public ActionResult Create([Bind(Include = "BatchId,BatchName,RefExpertName,StartTime,EndTime,StartDate,CompletedDate,BatchStatus,Technology")] BatchMaster batchMaster)
        {
            if (ModelState.IsValid)
            {
                db.BatchMasters.Add(batchMaster);
                db.SaveChanges();
                return RedirectToAction("Index");
            }

            ViewBag.BatchStatus = new SelectList(db.StatusMasters, "StatusId", "StatusTitle", batchMaster.BatchStatus);
            ViewBag.RefExpertName = new SelectList(db.ExpertMasters, "ExpertId", "ExpertName", batchMaster.RefExpertName);
            return View(batchMaster);
        }

        // GET: Manage/BatchMasters/Edit/5
        public ActionResult Edit(int? id)
        {
            if (id == null)
            {
                return new HttpStatusCodeResult(HttpStatusCode.BadRequest);
            }
            BatchMaster batchMaster = db.BatchMasters.Find(id);
            if (batchMaster == null)
            {
                return HttpNotFound();
            }
            ViewBag.BatchStatus = new SelectList(db.StatusMasters, "StatusId", "StatusTitle", batchMaster.BatchStatus);
            ViewBag.RefExpertName = new SelectList(db.ExpertMasters, "ExpertId", "ExpertName", batchMaster.RefExpertName);
            return View(batchMaster);
        }

        // POST: Manage/BatchMasters/Edit/5
        // To protect from overposting attacks, please enable the specific properties you want to bind to, for 
        // more details see https://go.microsoft.com/fwlink/?LinkId=317598.
        [HttpPost]
        [ValidateAntiForgeryToken]
        public ActionResult Edit([Bind(Include = "BatchId,BatchName,RefExpertName,StartTime,EndTime,StartDate,CompletedDate,BatchStatus,Technology")] BatchMaster batchMaster)
        {
            if (ModelState.IsValid)
            {
                db.Entry(batchMaster).State = EntityState.Modified;
                db.SaveChanges();
                return RedirectToAction("Index");
            }
            ViewBag.BatchStatus = new SelectList(db.StatusMasters, "StatusId", "StatusTitle", batchMaster.BatchStatus);
            ViewBag.RefExpertName = new SelectList(db.ExpertMasters, "ExpertId", "ExpertName", batchMaster.RefExpertName);
            return View(batchMaster);
        }

        // GET: Manage/BatchMasters/Delete/5
        public ActionResult Delete(int? id)
        {
            if (id == null)
            {
                return new HttpStatusCodeResult(HttpStatusCode.BadRequest);
            }
            BatchMaster batchMaster = db.BatchMasters.Find(id);
            if (batchMaster == null)
            {
                return HttpNotFound();
            }
            return View(batchMaster);
        }

        // POST: Manage/BatchMasters/Delete/5
        [HttpPost, ActionName("Delete")]
        [ValidateAntiForgeryToken]
        public ActionResult DeleteConfirmed(int id)
        {
            BatchMaster batchMaster = db.BatchMasters.Find(id);
            db.BatchMasters.Remove(batchMaster);
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

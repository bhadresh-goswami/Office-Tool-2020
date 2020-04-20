using System;
using System.Collections.Generic;
using System.Data;
using System.Data.Entity;
using System.Linq;
using System.Net;
using System.Web;
using System.Web.Mvc;
using DashReportingTool.Models;

namespace DashReportingTool.Areas.TechnicalExperts.Controllers
{
    [AuthenticateFilter(Roles = "technical expert,team lead")]

    public class SessionMastersController : Controller
    {
        private dbReportingEntities db = new dbReportingEntities();

        // GET: TechnicalExperts/SessionMasters
        public ActionResult Index(int? id)
        {

            if (id == null)
            {
                var sessionMasters1 = db.SessionMasters.Include(s => s.BatchMaster);
                return View(sessionMasters1.ToList());
            }

            var sessionMasters = db.SessionMasters.Include(s => s.BatchMaster).Where(a => a.BatchMaster.BatchId == id);
            return View(sessionMasters.ToList());
        }

        // GET: TechnicalExperts/SessionMasters/Details/5
        public ActionResult Details(int? id)
        {
            if (id == null)
            {
                return new HttpStatusCodeResult(HttpStatusCode.BadRequest);
            }
            SessionMaster sessionMaster = db.SessionMasters.Find(id);
            if (sessionMaster == null)
            {
                return HttpNotFound();
            }
            return View(sessionMaster);
        }

        // GET: TechnicalExperts/SessionMasters/Create
        public ActionResult Create()
        {
            ViewBag.RefBatchTitle = new SelectList(db.BatchMasters, "BatchId", "BatchName");
            return View();
        }

        // POST: TechnicalExperts/SessionMasters/Create
        // To protect from overposting attacks, please enable the specific properties you want to bind to, for 
        // more details see https://go.microsoft.com/fwlink/?LinkId=317598.
        [HttpPost]
        [ValidateAntiForgeryToken]
        public ActionResult Create([Bind(Include = "SessionId,SessionTitle,SessionTopic,SessionDate,RefBatchTitle,PresentCandidates")] SessionMaster sessionMaster, string[] list)
        {
            //if (ModelState.IsValid)
            //{
            string slist = "";
            for (int i = 0; i < list.Length; i++)
            {
                slist += list[i]+",";
            }
            slist = slist.Remove(slist.Length - 1, 1);

            sessionMaster.PresentCandidates = slist;
            db.SessionMasters.Add(sessionMaster);
            db.SaveChanges();
            return RedirectToAction("Index", "SessionMasters", new { @id = sessionMaster.RefBatchTitle });
            //}

            //ViewBag.RefBatchTitle = new SelectList(db.BatchMasters, "BatchId", "BatchName", sessionMaster.RefBatchTitle);
            //return View(sessionMaster);
        }

        // GET: TechnicalExperts/SessionMasters/Edit/5
        public ActionResult Edit(int? id)
        {
            if (id == null)
            {
                return new HttpStatusCodeResult(HttpStatusCode.BadRequest);
            }
            SessionMaster sessionMaster = db.SessionMasters.Find(id);
            if (sessionMaster == null)
            {
                return HttpNotFound();
            }
            ViewBag.RefBatchTitle = new SelectList(db.BatchMasters, "BatchId", "BatchName", sessionMaster.RefBatchTitle);
            return View(sessionMaster);
        }

        // POST: TechnicalExperts/SessionMasters/Edit/5
        // To protect from overposting attacks, please enable the specific properties you want to bind to, for 
        // more details see https://go.microsoft.com/fwlink/?LinkId=317598.
        [HttpPost]
        [ValidateAntiForgeryToken]
        public ActionResult Edit([Bind(Include = "SessionId,SessionTitle,SessionTopic,SessionDate,RefBatchTitle,PresentCandidates")] SessionMaster sessionMaster)
        {
            if (ModelState.IsValid)
            {
                db.Entry(sessionMaster).State = EntityState.Modified;
                db.SaveChanges();
                return RedirectToAction("Index");
            }
            ViewBag.RefBatchTitle = new SelectList(db.BatchMasters, "BatchId", "BatchName", sessionMaster.RefBatchTitle);
            return View(sessionMaster);
        }

        // GET: TechnicalExperts/SessionMasters/Delete/5
        public ActionResult Delete(int? id)
        {
            if (id == null)
            {
                return new HttpStatusCodeResult(HttpStatusCode.BadRequest);
            }
            SessionMaster sessionMaster = db.SessionMasters.Find(id);
            if (sessionMaster == null)
            {
                return HttpNotFound();
            }
            return View(sessionMaster);
        }

        // POST: TechnicalExperts/SessionMasters/Delete/5
        [HttpPost, ActionName("Delete")]
        [ValidateAntiForgeryToken]
        public ActionResult DeleteConfirmed(int id)
        {
            SessionMaster sessionMaster = db.SessionMasters.Find(id);
            db.SessionMasters.Remove(sessionMaster);
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

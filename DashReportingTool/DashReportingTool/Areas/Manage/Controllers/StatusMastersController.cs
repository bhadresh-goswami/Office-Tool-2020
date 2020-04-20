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
    public class StatusMastersController : Controller
    {
        private dbReportingEntities db = new dbReportingEntities();

        // GET: Manage/StatusMasters
        public ActionResult Index()
        {
            return View(db.StatusMasters.ToList());
        }

        // GET: Manage/StatusMasters/Details/5
        public ActionResult Details(int? id)
        {
            if (id == null)
            {
                return new HttpStatusCodeResult(HttpStatusCode.BadRequest);
            }
            StatusMaster statusMaster = db.StatusMasters.Find(id);
            if (statusMaster == null)
            {
                return HttpNotFound();
            }
            return View(statusMaster);
        }

        // GET: Manage/StatusMasters/Create
        public ActionResult Create()
        {
            return View();
        }

        // POST: Manage/StatusMasters/Create
        // To protect from overposting attacks, please enable the specific properties you want to bind to, for 
        // more details see https://go.microsoft.com/fwlink/?LinkId=317598.
        [HttpPost]
        [ValidateAntiForgeryToken]
        public ActionResult Create([Bind(Include = "StatusId,StatusTitle")] StatusMaster statusMaster)
        {
            if (ModelState.IsValid)
            {
                db.StatusMasters.Add(statusMaster);
                db.SaveChanges();
                return RedirectToAction("Index");
            }

            return View(statusMaster);
        }

        // GET: Manage/StatusMasters/Edit/5
        public ActionResult Edit(int? id)
        {
            if (id == null)
            {
                return new HttpStatusCodeResult(HttpStatusCode.BadRequest);
            }
            StatusMaster statusMaster = db.StatusMasters.Find(id);
            if (statusMaster == null)
            {
                return HttpNotFound();
            }
            return View(statusMaster);
        }

        // POST: Manage/StatusMasters/Edit/5
        // To protect from overposting attacks, please enable the specific properties you want to bind to, for 
        // more details see https://go.microsoft.com/fwlink/?LinkId=317598.
        [HttpPost]
        [ValidateAntiForgeryToken]
        public ActionResult Edit([Bind(Include = "StatusId,StatusTitle")] StatusMaster statusMaster)
        {
            if (ModelState.IsValid)
            {
                db.Entry(statusMaster).State = EntityState.Modified;
                db.SaveChanges();
                return RedirectToAction("Index");
            }
            return View(statusMaster);
        }

        // GET: Manage/StatusMasters/Delete/5
        public ActionResult Delete(int? id)
        {
            if (id == null)
            {
                return new HttpStatusCodeResult(HttpStatusCode.BadRequest);
            }
            StatusMaster statusMaster = db.StatusMasters.Find(id);
            if (statusMaster == null)
            {
                return HttpNotFound();
            }
            return View(statusMaster);
        }

        // POST: Manage/StatusMasters/Delete/5
        [HttpPost, ActionName("Delete")]
        [ValidateAntiForgeryToken]
        public ActionResult DeleteConfirmed(int id)
        {
            StatusMaster statusMaster = db.StatusMasters.Find(id);
            db.StatusMasters.Remove(statusMaster);
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

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

    public class TechMastersController : Controller
    {
        private dbReportingEntities db = new dbReportingEntities();

        // GET: Manage/TechMasters
        public ActionResult Index()
        {
            return View(db.TechMasters.ToList());
        }

        // GET: Manage/TechMasters/Details/5
        public ActionResult Details(int? id)
        {
            if (id == null)
            {
                return new HttpStatusCodeResult(HttpStatusCode.BadRequest);
            }
            TechMaster techMaster = db.TechMasters.Find(id);
            if (techMaster == null)
            {
                return HttpNotFound();
            }
            return View(techMaster);
        }

        // GET: Manage/TechMasters/Create
        public ActionResult Create()
        {
            return View();
        }

        // POST: Manage/TechMasters/Create
        // To protect from overposting attacks, please enable the specific properties you want to bind to, for 
        // more details see https://go.microsoft.com/fwlink/?LinkId=317598.
        [HttpPost]
        [ValidateAntiForgeryToken]
        public ActionResult Create([Bind(Include = "TechId,TechName")] TechMaster techMaster)
        {
            if (ModelState.IsValid)
            {
                db.TechMasters.Add(techMaster);
                db.SaveChanges();
                return RedirectToAction("Index");
            }

            return View(techMaster);
        }

        // GET: Manage/TechMasters/Edit/5
        public ActionResult Edit(int? id)
        {
            if (id == null)
            {
                return new HttpStatusCodeResult(HttpStatusCode.BadRequest);
            }
            TechMaster techMaster = db.TechMasters.Find(id);
            if (techMaster == null)
            {
                return HttpNotFound();
            }
            return View(techMaster);
        }

        // POST: Manage/TechMasters/Edit/5
        // To protect from overposting attacks, please enable the specific properties you want to bind to, for 
        // more details see https://go.microsoft.com/fwlink/?LinkId=317598.
        [HttpPost]
        [ValidateAntiForgeryToken]
        public ActionResult Edit([Bind(Include = "TechId,TechName")] TechMaster techMaster)
        {
            if (ModelState.IsValid)
            {
                db.Entry(techMaster).State = EntityState.Modified;
                db.SaveChanges();
                return RedirectToAction("Index");
            }
            return View(techMaster);
        }

        // GET: Manage/TechMasters/Delete/5
        public ActionResult Delete(int? id)
        {
            if (id == null)
            {
                return new HttpStatusCodeResult(HttpStatusCode.BadRequest);
            }
            TechMaster techMaster = db.TechMasters.Find(id);
            if (techMaster == null)
            {
                return HttpNotFound();
            }
            return View(techMaster);
        }

        // POST: Manage/TechMasters/Delete/5
        [HttpPost, ActionName("Delete")]
        [ValidateAntiForgeryToken]
        public ActionResult DeleteConfirmed(int id)
        {
            TechMaster techMaster = db.TechMasters.Find(id);
            db.TechMasters.Remove(techMaster);
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

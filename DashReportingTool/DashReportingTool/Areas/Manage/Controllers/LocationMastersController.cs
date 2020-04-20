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

    public class LocationMastersController : Controller
    {
        private dbReportingEntities db = new dbReportingEntities();

        // GET: Manage/LocationMasters
        public ActionResult Index()
        {
            return View(db.LocationMasters.ToList());
        }

        // GET: Manage/LocationMasters/Details/5
        public ActionResult Details(int? id)
        {
            if (id == null)
            {
                return new HttpStatusCodeResult(HttpStatusCode.BadRequest);
            }
            LocationMaster locationMaster = db.LocationMasters.Find(id);
            if (locationMaster == null)
            {
                return HttpNotFound();
            }
            return View(locationMaster);
        }

        // GET: Manage/LocationMasters/Create
        public ActionResult Create()
        {
            return View();
        }

        // POST: Manage/LocationMasters/Create
        // To protect from overposting attacks, please enable the specific properties you want to bind to, for 
        // more details see https://go.microsoft.com/fwlink/?LinkId=317598.
        [HttpPost]
        [ValidateAntiForgeryToken]
        public ActionResult Create([Bind(Include = "LocationId,LocationName")] LocationMaster locationMaster)
        {
            if (ModelState.IsValid)
            {
                db.LocationMasters.Add(locationMaster);
                db.SaveChanges();
                return RedirectToAction("Index");
            }

            return View(locationMaster);
        }

        // GET: Manage/LocationMasters/Edit/5
        public ActionResult Edit(int? id)
        {
            if (id == null)
            {
                return new HttpStatusCodeResult(HttpStatusCode.BadRequest);
            }
            LocationMaster locationMaster = db.LocationMasters.Find(id);
            if (locationMaster == null)
            {
                return HttpNotFound();
            }
            return View(locationMaster);
        }

        // POST: Manage/LocationMasters/Edit/5
        // To protect from overposting attacks, please enable the specific properties you want to bind to, for 
        // more details see https://go.microsoft.com/fwlink/?LinkId=317598.
        [HttpPost]
        [ValidateAntiForgeryToken]
        public ActionResult Edit([Bind(Include = "LocationId,LocationName")] LocationMaster locationMaster)
        {
            if (ModelState.IsValid)
            {
                db.Entry(locationMaster).State = EntityState.Modified;
                db.SaveChanges();
                return RedirectToAction("Index");
            }
            return View(locationMaster);
        }

        // GET: Manage/LocationMasters/Delete/5
        public ActionResult Delete(int? id)
        {
            if (id == null)
            {
                return new HttpStatusCodeResult(HttpStatusCode.BadRequest);
            }
            LocationMaster locationMaster = db.LocationMasters.Find(id);
            if (locationMaster == null)
            {
                return HttpNotFound();
            }
            return View(locationMaster);
        }

        // POST: Manage/LocationMasters/Delete/5
        [HttpPost, ActionName("Delete")]
        [ValidateAntiForgeryToken]
        public ActionResult DeleteConfirmed(int id)
        {
            LocationMaster locationMaster = db.LocationMasters.Find(id);
            db.LocationMasters.Remove(locationMaster);
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

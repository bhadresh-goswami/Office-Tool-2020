﻿using System;
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

    public class ExpertMastersController : Controller
    {
        private dbReportingEntities db = new dbReportingEntities();

        // GET: Manage/ExpertMasters
        public ActionResult Index()
        {
            var expertMasters = db.ExpertMasters.Include(e => e.LocationMaster);
            return View(expertMasters.ToList());
        }

        // GET: Manage/ExpertMasters/Details/5
        public ActionResult Details(int? id)
        {
            if (id == null)
            {
                return new HttpStatusCodeResult(HttpStatusCode.BadRequest);
            }
            ExpertMaster expertMaster = db.ExpertMasters.Find(id);
            if (expertMaster == null)
            {
                return HttpNotFound();
            }
            return View(expertMaster);
        }

        // GET: Manage/ExpertMasters/Create
        public ActionResult Create()
        {
            ViewBag.RefLocationId = new SelectList(db.LocationMasters, "LocationId", "LocationName");
            return View();
        }

        // POST: Manage/ExpertMasters/Create
        // To protect from overposting attacks, please enable the specific properties you want to bind to, for 
        // more details see https://go.microsoft.com/fwlink/?LinkId=317598.
        [HttpPost]
        [ValidateAntiForgeryToken]
        public ActionResult Create([Bind(Include = "ExpertId,ExpertName,ExpertPassword,ExpertEmailid,ExpertMobile,Designation,RefLocationId")] ExpertMaster expertMaster)
        {
            try
            {
                if (ModelState.IsValid)
                {
                    db.ExpertMasters.Add(expertMaster);
                    db.SaveChanges();
                    return RedirectToAction("Index");
                }

                ViewBag.RefLocationId = new SelectList(db.LocationMasters, "LocationId", "LocationName", expertMaster.RefLocationId);
                return View(expertMaster);
            }
            catch (Exception ex)
            {
                TempData["err"] = ex.Message;
                return RedirectToAction("Index");
            }
         
        }

        // GET: Manage/ExpertMasters/Edit/5
        public ActionResult Edit(int? id)
        {
            if (id == null)
            {
                return new HttpStatusCodeResult(HttpStatusCode.BadRequest);
            }
            ExpertMaster expertMaster = db.ExpertMasters.Find(id);
            if (expertMaster == null)
            {
                return HttpNotFound();
            }
            ViewBag.RefLocationId = new SelectList(db.LocationMasters, "LocationId", "LocationName", expertMaster.RefLocationId);
            return View(expertMaster);
        }

        // POST: Manage/ExpertMasters/Edit/5
        // To protect from overposting attacks, please enable the specific properties you want to bind to, for 
        // more details see https://go.microsoft.com/fwlink/?LinkId=317598.
        [HttpPost]
        [ValidateAntiForgeryToken]
        public ActionResult Edit([Bind(Include = "ExpertId,ExpertName,ExpertPassword,ExpertEmailid,ExpertMobile,Designation,RefLocationId")] ExpertMaster expertMaster)
        {
            try
            {
                if (ModelState.IsValid)
                {
                    db.Entry(expertMaster).State = EntityState.Modified;
                    db.SaveChanges();
                    return RedirectToAction("Index");
                }
                ViewBag.RefLocationId = new SelectList(db.LocationMasters, "LocationId", "LocationName", expertMaster.RefLocationId);
                return View(expertMaster);
            }
            catch (Exception ex)
            {
                TempData["err"] = ex.Message;
                return RedirectToAction("Index");
            }
            
        }

        // GET: Manage/ExpertMasters/Delete/5
        public ActionResult Delete(int? id)
        {
            if (id == null)
            {
                return new HttpStatusCodeResult(HttpStatusCode.BadRequest);
            }
            ExpertMaster expertMaster = db.ExpertMasters.Find(id);
            if (expertMaster == null)
            {
                return HttpNotFound();
            }
            return View(expertMaster);
        }

        // POST: Manage/ExpertMasters/Delete/5
        [HttpPost, ActionName("Delete")]
        [ValidateAntiForgeryToken]
        public ActionResult DeleteConfirmed(int id)
        {
            ExpertMaster expertMaster = db.ExpertMasters.Find(id);
            db.ExpertMasters.Remove(expertMaster);
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

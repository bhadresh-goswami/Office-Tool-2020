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

    public class TechExpertMastersController : Controller
    {
        private dbReportingEntities db = new dbReportingEntities();

        // GET: Manage/TechExpertMasters
        public ActionResult Index()
        {
            var techExpertMasters = db.TechExpertMasters.Include(t => t.ExpertMaster).Include(t => t.TechMaster);
            return View(techExpertMasters.ToList());
        }

        // GET: Manage/TechExpertMasters/Details/5
        public ActionResult Details(int? id)
        {
            if (id == null)
            {
                return new HttpStatusCodeResult(HttpStatusCode.BadRequest);
            }
            TechExpertMaster techExpertMaster = db.TechExpertMasters.Find(id);
            if (techExpertMaster == null)
            {
                return HttpNotFound();
            }
            return View(techExpertMaster);
        }

        // GET: Manage/TechExpertMasters/Create
        public ActionResult Create()
        {
            ViewBag.RefExpertId = new SelectList(db.ExpertMasters, "ExpertId", "ExpertName");
            ViewBag.RefTechId = new SelectList(db.TechMasters, "TechId", "TechName");
            return View();
        }

        // POST: Manage/TechExpertMasters/Create
        // To protect from overposting attacks, please enable the specific properties you want to bind to, for 
        // more details see https://go.microsoft.com/fwlink/?LinkId=317598.
        [HttpPost]
        [ValidateAntiForgeryToken]
        public ActionResult Create([Bind(Include = "TechExpertId,RefTechId,RefExpertId,TechExpertIsEnabled")] TechExpertMaster techExpertMaster)
        {
            if (ModelState.IsValid)
            {
                db.TechExpertMasters.Add(techExpertMaster);
                db.SaveChanges();
                return RedirectToAction("Index");
            }

            ViewBag.RefExpertId = new SelectList(db.ExpertMasters, "ExpertId", "ExpertName", techExpertMaster.RefExpertId);
            ViewBag.RefTechId = new SelectList(db.TechMasters, "TechId", "TechName", techExpertMaster.RefTechId);
            return View(techExpertMaster);
        }

        // GET: Manage/TechExpertMasters/Edit/5
        public ActionResult Edit(int? id)
        {
            if (id == null)
            {
                return new HttpStatusCodeResult(HttpStatusCode.BadRequest);
            }
            TechExpertMaster techExpertMaster = db.TechExpertMasters.Find(id);
            if (techExpertMaster == null)
            {
                return HttpNotFound();
            }
            ViewBag.RefExpertId = new SelectList(db.ExpertMasters, "ExpertId", "ExpertName", techExpertMaster.RefExpertId);
            ViewBag.RefTechId = new SelectList(db.TechMasters, "TechId", "TechName", techExpertMaster.RefTechId);
            return View(techExpertMaster);
        }

        // POST: Manage/TechExpertMasters/Edit/5
        // To protect from overposting attacks, please enable the specific properties you want to bind to, for 
        // more details see https://go.microsoft.com/fwlink/?LinkId=317598.
        [HttpPost]
        [ValidateAntiForgeryToken]
        public ActionResult Edit([Bind(Include = "TechExpertId,RefTechId,RefExpertId,TechExpertIsEnabled")] TechExpertMaster techExpertMaster)
        {
            if (ModelState.IsValid)
            {
                db.Entry(techExpertMaster).State = EntityState.Modified;
                db.SaveChanges();
                return RedirectToAction("Index");
            }
            ViewBag.RefExpertId = new SelectList(db.ExpertMasters, "ExpertId", "ExpertName", techExpertMaster.RefExpertId);
            ViewBag.RefTechId = new SelectList(db.TechMasters, "TechId", "TechName", techExpertMaster.RefTechId);
            return View(techExpertMaster);
        }

        // GET: Manage/TechExpertMasters/Delete/5
        public ActionResult Delete(int? id)
        {
            if (id == null)
            {
                return new HttpStatusCodeResult(HttpStatusCode.BadRequest);
            }
            TechExpertMaster techExpertMaster = db.TechExpertMasters.Find(id);
            if (techExpertMaster == null)
            {
                return HttpNotFound();
            }
            return View(techExpertMaster);
        }

        // POST: Manage/TechExpertMasters/Delete/5
        [HttpPost, ActionName("Delete")]
        [ValidateAntiForgeryToken]
        public ActionResult DeleteConfirmed(int id)
        {
            TechExpertMaster techExpertMaster = db.TechExpertMasters.Find(id);
            db.TechExpertMasters.Remove(techExpertMaster);
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

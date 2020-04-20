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

    public class CandidateMastersController : Controller
    {
        private dbReportingEntities db = new dbReportingEntities();

        // GET: Manage/CandidateMasters
        public ActionResult Index()
        {
            var candidateMasters = db.CandidateMasters.Include(c => c.StatusMaster);
            return View(candidateMasters.ToList());
        }

        // GET: Manage/CandidateMasters/Details/5
        public ActionResult Details(int? id)
        {
            if (id == null)
            {
                return new HttpStatusCodeResult(HttpStatusCode.BadRequest);
            }
            CandidateMaster candidateMaster = db.CandidateMasters.Find(id);
            if (candidateMaster == null)
            {
                return HttpNotFound();
            }
            return View(candidateMaster);
        }

        // GET: Manage/CandidateMasters/Create
        public ActionResult Create()
        {
            ViewBag.RefCandidateStatusTitle = new SelectList(db.StatusMasters, "StatusId", "StatusTitle");
            return View();
        }

        // POST: Manage/CandidateMasters/Create
        // To protect from overposting attacks, please enable the specific properties you want to bind to, for 
        // more details see https://go.microsoft.com/fwlink/?LinkId=317598.
        [HttpPost]
        [ValidateAntiForgeryToken]
        public ActionResult Create([Bind(Include = "CandidateId,CandidateName,CandidateEmailId,CandidateMobile,RefCandidateStatusTitle")] CandidateMaster candidateMaster)
        {
            try
            {

                if (ModelState.IsValid)
                {
                    db.CandidateMasters.Add(candidateMaster);
                    db.SaveChanges();
                    return RedirectToAction("Index");
                }

                ViewBag.RefCandidateStatusTitle = new SelectList(db.StatusMasters, "StatusId", "StatusTitle", candidateMaster.RefCandidateStatusTitle);
                return View(candidateMaster);
            }
            catch (Exception ex)
            {
                TempData["err"] = ex.Message;
                return RedirectToAction("Index");
            }
        }

        // GET: Manage/CandidateMasters/Edit/5
        public ActionResult Edit(int? id)
        {
            if (id == null)
            {
                return new HttpStatusCodeResult(HttpStatusCode.BadRequest);
            }
            CandidateMaster candidateMaster = db.CandidateMasters.Find(id);
            if (candidateMaster == null)
            {
                return HttpNotFound();
            }
            ViewBag.RefCandidateStatusTitle = new SelectList(db.StatusMasters, "StatusId", "StatusTitle", candidateMaster.RefCandidateStatusTitle);
            return View(candidateMaster);
        }

        // POST: Manage/CandidateMasters/Edit/5
        // To protect from overposting attacks, please enable the specific properties you want to bind to, for 
        // more details see https://go.microsoft.com/fwlink/?LinkId=317598.
        [HttpPost]
        [ValidateAntiForgeryToken]
        public ActionResult Edit([Bind(Include = "CandidateId,CandidateName,CandidateEmailId,CandidateMobile,RefCandidateStatusTitle")] CandidateMaster candidateMaster)
        {
            try
            {
                if (ModelState.IsValid)
                {
                    db.Entry(candidateMaster).State = EntityState.Modified;
                    db.SaveChanges();
                    return RedirectToAction("Index");
                }
                ViewBag.RefCandidateStatusTitle = new SelectList(db.StatusMasters, "StatusId", "StatusTitle", candidateMaster.RefCandidateStatusTitle);
                return View(candidateMaster);
            }
            catch (Exception ex)
            {
                TempData["err"] = ex.Message;
                return RedirectToAction("Index");
            }
           
        }

        // GET: Manage/CandidateMasters/Delete/5
        public ActionResult Delete(int? id)
        {
            if (id == null)
            {
                return new HttpStatusCodeResult(HttpStatusCode.BadRequest);
            }
            CandidateMaster candidateMaster = db.CandidateMasters.Find(id);
            if (candidateMaster == null)
            {
                return HttpNotFound();
            }
            return View(candidateMaster);
        }

        // POST: Manage/CandidateMasters/Delete/5
        [HttpPost, ActionName("Delete")]
        [ValidateAntiForgeryToken]
        public ActionResult DeleteConfirmed(int id)
        {
            CandidateMaster candidateMaster = db.CandidateMasters.Find(id);
            db.CandidateMasters.Remove(candidateMaster);
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

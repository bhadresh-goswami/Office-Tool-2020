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
    [AuthenticateFilter(Roles = "technical expert,admin,team lead")]

    public class TaskTitleMastersController : Controller
    {
        private dbReportingEntities db = new dbReportingEntities();

        // GET: TechnicalExperts/TaskTitleMasters
        public ActionResult Index()
        {
            return View(db.TaskTitleMasters.ToList());
        }

        // GET: TechnicalExperts/TaskTitleMasters/Details/5
        public ActionResult Details(int? id)
        {
            if (id == null)
            {
                return new HttpStatusCodeResult(HttpStatusCode.BadRequest);
            }
            TaskTitleMaster taskTitleMaster = db.TaskTitleMasters.Find(id);
            if (taskTitleMaster == null)
            {
                return HttpNotFound();
            }
            return View(taskTitleMaster);
        }

        // GET: TechnicalExperts/TaskTitleMasters/Create
        public ActionResult Create()
        {
            return View();
        }

        // POST: TechnicalExperts/TaskTitleMasters/Create
        // To protect from overposting attacks, please enable the specific properties you want to bind to, for 
        // more details see https://go.microsoft.com/fwlink/?LinkId=317598.
        [HttpPost]
        [ValidateAntiForgeryToken]
        public ActionResult Create([Bind(Include = "TaskTitleId,TaskTitle")] TaskTitleMaster taskTitleMaster)
        {
            if (ModelState.IsValid)
            {
                db.TaskTitleMasters.Add(taskTitleMaster);
                db.SaveChanges();
                return RedirectToAction("Index");
            }

            return View(taskTitleMaster);
        }

        // GET: TechnicalExperts/TaskTitleMasters/Edit/5
        public ActionResult Edit(int? id)
        {
            if (id == null)
            {
                return new HttpStatusCodeResult(HttpStatusCode.BadRequest);
            }
            TaskTitleMaster taskTitleMaster = db.TaskTitleMasters.Find(id);
            if (taskTitleMaster == null)
            {
                return HttpNotFound();
            }
            return View(taskTitleMaster);
        }

        // POST: TechnicalExperts/TaskTitleMasters/Edit/5
        // To protect from overposting attacks, please enable the specific properties you want to bind to, for 
        // more details see https://go.microsoft.com/fwlink/?LinkId=317598.
        [HttpPost]
        [ValidateAntiForgeryToken]
        public ActionResult Edit([Bind(Include = "TaskTitleId,TaskTitle")] TaskTitleMaster taskTitleMaster)
        {
            if (ModelState.IsValid)
            {
                db.Entry(taskTitleMaster).State = EntityState.Modified;
                db.SaveChanges();
                return RedirectToAction("Index");
            }
            return View(taskTitleMaster);
        }

        // GET: TechnicalExperts/TaskTitleMasters/Delete/5
        public ActionResult Delete(int? id)
        {
            if (id == null)
            {
                return new HttpStatusCodeResult(HttpStatusCode.BadRequest);
            }
            TaskTitleMaster taskTitleMaster = db.TaskTitleMasters.Find(id);
            if (taskTitleMaster == null)
            {
                return HttpNotFound();
            }
            return View(taskTitleMaster);
        }

        // POST: TechnicalExperts/TaskTitleMasters/Delete/5
        [HttpPost, ActionName("Delete")]
        [ValidateAntiForgeryToken]
        public ActionResult DeleteConfirmed(int id)
        {
            TaskTitleMaster taskTitleMaster = db.TaskTitleMasters.Find(id);
            db.TaskTitleMasters.Remove(taskTitleMaster);
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

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

    public class TaskMastersController : Controller
    {
        private dbReportingEntities db = new dbReportingEntities();

        // GET: TechnicalExperts/TaskMasters
        public ActionResult Index()
        {
            var taskMasters = db.TaskMasters.Include(t => t.ExpertMaster).Include(t => t.TaskTitleMaster);
            return View(taskMasters.ToList());
        }

        // GET: TechnicalExperts/TaskMasters/Details/5
        public ActionResult Details(int? id)
        {
            if (id == null)
            {
                return new HttpStatusCodeResult(HttpStatusCode.BadRequest);
            }
            TaskMaster taskMaster = db.TaskMasters.Find(id);
            if (taskMaster == null)
            {
                return HttpNotFound();
            }
            return View(taskMaster);
        }

        // GET: TechnicalExperts/TaskMasters/Create
        public ActionResult Create()
        {
            int id = int.Parse(Session["UserId"].ToString());
            var isanyTask = db.TaskMasters.Where(a => a.RefExpertName == id && !a.TaskDone);
            if (isanyTask.ToList().Count>0)
            {
                ViewBag.info = "Current Task is Pending, Please Complete it First!";
                return RedirectToAction("Index");
            }
            ViewBag.RefExpertName = new SelectList(db.ExpertMasters, "ExpertId", "ExpertName");
            ViewBag.RefTaskTitle = new SelectList(db.TaskTitleMasters, "TaskTitleId", "TaskTitle");
            return View();
        }

        // POST: TechnicalExperts/TaskMasters/Create
        // To protect from overposting attacks, please enable the specific properties you want to bind to, for 
        // more details see https://go.microsoft.com/fwlink/?LinkId=317598.
        [HttpPost]
        [ValidateAntiForgeryToken]
        public ActionResult Create([Bind(Include = "TaskId,TaskDate,TaskDone,RefExpertName,TaskDetails,RefTaskTitle,EstimateTime,TaskStartTime,TaskEndTime,TimeTaken")] TaskMaster taskMaster)
        {
            //if (ModelState.IsValid)
            //{
            
            taskMaster.RefAssignedBy = int.Parse(Session["UserId"].ToString());
            taskMaster.AssignedTask = false;
            taskMaster.IsStartedTask = true;
            taskMaster.DeadLine = DateTime.Now;

            taskMaster.RefExpertName = int.Parse(Session["UserId"].ToString());
            taskMaster.TaskStartTime = DateTime.Now.TimeOfDay;
            taskMaster.TaskEndTime = DateTime.Now.AddMinutes(taskMaster.EstimateTime).TimeOfDay;
            taskMaster.TimeTaken = 0;
            db.TaskMasters.Add(taskMaster);
            db.SaveChanges();
            return RedirectToAction("Index");
            //}

            //ViewBag.RefExpertName = new SelectList(db.ExpertMasters, "ExpertId", "ExpertName", taskMaster.RefExpertName);
            //ViewBag.RefTaskTitle = new SelectList(db.TaskTitleMasters, "TaskTitleId", "TaskTitle", taskMaster.RefTaskTitle);
            //return View(taskMaster);
        }

        // GET: TechnicalExperts/TaskMasters/Edit/5
        public ActionResult Edit(int? id)
        {
            if (id == null)
            {
                return new HttpStatusCodeResult(HttpStatusCode.BadRequest);
            }
            TaskMaster taskMaster = db.TaskMasters.Find(id);
            if (taskMaster == null)
            {
                return HttpNotFound();
            }
            ViewBag.RefExpertName = new SelectList(db.ExpertMasters, "ExpertId", "ExpertName", taskMaster.RefExpertName);
            ViewBag.RefTaskTitle = new SelectList(db.TaskTitleMasters, "TaskTitleId", "TaskTitle", taskMaster.RefTaskTitle);
            return View(taskMaster);
        }

        // POST: TechnicalExperts/TaskMasters/Edit/5
        // To protect from overposting attacks, please enable the specific properties you want to bind to, for 
        // more details see https://go.microsoft.com/fwlink/?LinkId=317598.
        [HttpPost]
        [ValidateAntiForgeryToken]
        public ActionResult Edit([Bind(Include = "TaskId,TaskDate,TaskDone,RefExpertName,TaskDetails,RefTaskTitle,EstimateTime,TaskStartTime,TaskEndTime,TimeTaken")] TaskMaster taskMaster)
        {
            if (ModelState.IsValid)
            {
                db.Entry(taskMaster).State = EntityState.Modified;
                db.SaveChanges();
                return RedirectToAction("Index");
            }
            ViewBag.RefExpertName = new SelectList(db.ExpertMasters, "ExpertId", "ExpertName", taskMaster.RefExpertName);
            ViewBag.RefTaskTitle = new SelectList(db.TaskTitleMasters, "TaskTitleId", "TaskTitle", taskMaster.RefTaskTitle);
            return View(taskMaster);
        }

        // GET: TechnicalExperts/TaskMasters/Delete/5
        public ActionResult Delete(int? id)
        {
            if (id == null)
            {
                return new HttpStatusCodeResult(HttpStatusCode.BadRequest);
            }
            TaskMaster taskMaster = db.TaskMasters.Find(id);
            if (taskMaster == null)
            {
                return HttpNotFound();
            }
            return View(taskMaster);
        }

        // POST: TechnicalExperts/TaskMasters/Delete/5
        [HttpPost, ActionName("Delete")]
        [ValidateAntiForgeryToken]
        public ActionResult DeleteConfirmed(int id)
        {
            TaskMaster taskMaster = db.TaskMasters.Find(id);
            db.TaskMasters.Remove(taskMaster);
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

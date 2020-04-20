using System;
using System.Collections.Generic;
using System.Data;
using System.Data.Entity;
using System.Linq;
using System.Net;
using System.Web;
using System.Web.Mvc;
using DashReportingTool.Models;

namespace DashReportingTool.Areas.TeamLead.Controllers
{
    [AuthenticateFilter(Roles = "admin,team lead,manager")]
    public class AssignTaskMastersController : Controller
    {
        private dbReportingEntities db = new dbReportingEntities();

        // GET: TeamLead/AssignTaskMasters
        public ActionResult Index(int? SelectedExpert)
        {
            ViewBag.userType = Session["UserType"].ToString();
            if (SelectedExpert != null) 
            {
                var taskMasters1 = db.TaskMasters.Include(t => t.ExpertMaster).Include(t => t.ExpertMaster1).Include(t => t.TaskTitleMaster).Where(a=>a.RefExpertName==SelectedExpert);
                return View(taskMasters1.Where(a=>a.AssignedTask==true).ToList());
            }
            var taskMasters = db.TaskMasters.Include(t => t.ExpertMaster).Include(t => t.ExpertMaster1).Include(t => t.TaskTitleMaster);
            return View(taskMasters.Where(a => a.AssignedTask == true).ToList());
        }

        // GET: TeamLead/AssignTaskMasters/Details/5
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

        // GET: TeamLead/AssignTaskMasters/Create
        public ActionResult Create()
        {
            ViewBag.RefAssignedBy = new SelectList(db.ExpertMasters, "ExpertId", "ExpertName");
            ViewBag.RefExpertName = new SelectList(db.ExpertMasters, "ExpertId", "ExpertName");
            ViewBag.RefTaskTitle = new SelectList(db.TaskTitleMasters, "TaskTitleId", "TaskTitle");
            return View();
        }

        // POST: TeamLead/AssignTaskMasters/Create
        // To protect from overposting attacks, please enable the specific properties you want to bind to, for 
        // more details see https://go.microsoft.com/fwlink/?LinkId=317598.
        [HttpPost]
        [ValidateAntiForgeryToken]
        public ActionResult Create([Bind(Include = "TaskId,TaskDate,TaskDone,RefExpertName,TaskDetails,RefTaskTitle,EstimateTime,TaskStartTime,TaskEndTime,TimeTaken,RefAssignedBy,AssignedTask,IsStartedTask,DeadLine")] TaskMaster taskMaster)
        {
            try
            {
                if (ModelState.IsValid)
                {
                    taskMaster.IsStartedTask = false;
                    taskMaster.RefAssignedBy = int.Parse(Session["UserId"].ToString());
                    taskMaster.TaskDone = false;
                    taskMaster.RefStatusTitle = 6;
                    taskMaster.TaskEndTime = DateTime.Now.TimeOfDay;
                    taskMaster.TaskStartTime = DateTime.Now.TimeOfDay;
                    taskMaster.TimeTaken = 0;
                    taskMaster.AssignedTask = true;

                    db.TaskMasters.Add(taskMaster);
                    db.SaveChanges();
                    return RedirectToAction("Index");
                }

                ViewBag.RefAssignedBy = new SelectList(db.ExpertMasters, "ExpertId", "ExpertName", taskMaster.RefAssignedBy);
                ViewBag.RefExpertName = new SelectList(db.ExpertMasters, "ExpertId", "ExpertName", taskMaster.RefExpertName);
                ViewBag.RefTaskTitle = new SelectList(db.TaskTitleMasters, "TaskTitleId", "TaskTitle", taskMaster.RefTaskTitle);
                return View(taskMaster);
            }
            catch (Exception ex)
            {

                TempData["err"] = ex.Message;
                return RedirectToAction("Index");
            }

           
        }

        // GET: TeamLead/AssignTaskMasters/Edit/5
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
            ViewBag.RefAssignedBy = new SelectList(db.ExpertMasters, "ExpertId", "ExpertName", taskMaster.RefAssignedBy);
            ViewBag.RefExpertName = new SelectList(db.ExpertMasters, "ExpertId", "ExpertName", taskMaster.RefExpertName);
            ViewBag.RefTaskTitle = new SelectList(db.TaskTitleMasters, "TaskTitleId", "TaskTitle", taskMaster.RefTaskTitle);
            return View(taskMaster);
        }

        // POST: TeamLead/AssignTaskMasters/Edit/5
        // To protect from overposting attacks, please enable the specific properties you want to bind to, for 
        // more details see https://go.microsoft.com/fwlink/?LinkId=317598.
        [HttpPost]
        [ValidateAntiForgeryToken]
        public ActionResult Edit([Bind(Include = "TaskId,TaskDate,TaskDone,RefExpertName,TaskDetails,RefTaskTitle,EstimateTime,TaskStartTime,TaskEndTime,TimeTaken,RefAssignedBy,AssignedTask,IsStartedTask,DeadLine")] TaskMaster taskMaster)
        {
            try
            {
                if (ModelState.IsValid)
                {
                    db.Entry(taskMaster).State = EntityState.Modified;
                    db.SaveChanges();
                    return RedirectToAction("Index");
                }
                ViewBag.RefAssignedBy = new SelectList(db.ExpertMasters, "ExpertId", "ExpertName", taskMaster.RefAssignedBy);
                ViewBag.RefExpertName = new SelectList(db.ExpertMasters, "ExpertId", "ExpertName", taskMaster.RefExpertName);
                ViewBag.RefTaskTitle = new SelectList(db.TaskTitleMasters, "TaskTitleId", "TaskTitle", taskMaster.RefTaskTitle);
                return View(taskMaster);
            }
            catch (Exception ex)
            {

                TempData["err"] = ex.Message;
                return RedirectToAction("Index");
            }

          
        }

        // GET: TeamLead/AssignTaskMasters/Delete/5
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

        // POST: TeamLead/AssignTaskMasters/Delete/5
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

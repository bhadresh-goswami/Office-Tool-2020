﻿using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.Mvc;

using DashReportingTool.Models;

namespace DashReportingTool.Areas.User.Controllers
{
    [AuthenticateFilter(Roles = "admin,manager,team lead,technical expert")]

    public class DashboardController : Controller
    {
        dbReportingEntities db = new dbReportingEntities();

        // GET: User/Dashboard
        public ActionResult Index()
        {
            if (Session["UserType"].ToString().Contains("Admin"))
            {
                return RedirectToAction("adminView");
            }
            if (Session["UserType"].ToString().Contains("Team Lead"))
            {
                return RedirectToAction("TeamLeadIndex");
            }

            int id = int.Parse(Session["UserId"].ToString());

            var tasks = db.TaskMasters.Where(a => a.RefExpertName == id).OrderByDescending(a => a.TaskDate).OrderByDescending(a => a.TaskStartTime).ToList();
            var pendingTask = tasks.Where(a => a.RefStatusTitle == 4 || a.RefStatusTitle == 2);
            var d = db.TaskMasters.Where(a => a.RefExpertName == id);
            var completedTask = d.ToList().Where(a => a.TaskDone && a.TaskDate.Date == DateTime.Now.Date);

            var assignedTask = db.TaskMasters.Where(a => a.RefExpertName == id && a.AssignedTask == true && !a.TaskDone);

            ViewBag.PT = pendingTask.ToList();
            ViewBag.CT = completedTask.ToList();
            ViewBag.APT = assignedTask.ToList();

            return View(tasks);
        }




        public ActionResult TeamLeadIndex()
        {

            int id = int.Parse(Session["UserId"].ToString());

            var tasks = db.TaskMasters.Where(a => a.RefExpertName == id && a.AssignedTask == null).OrderByDescending(a => a.TaskDate).OrderByDescending(a => a.TaskStartTime).ToList();
            var pendingTask = tasks.Where(a => !a.TaskDone);
            var completedTask = tasks.Where(a => a.TaskDone && a.TaskDate.Date == DateTime.Now.Date);

            var assignedTask = db.TaskMasters.Where(a => a.RefExpertName == id && a.AssignedTask == true && !a.TaskDone);

            ViewBag.PT = pendingTask.ToList();
            ViewBag.CT = completedTask.ToList();
            ViewBag.APT = assignedTask.ToList();

            return View(tasks);
        }
        public ActionResult adminView()
        {
            List<TaskMaster> tlist = db.TaskMasters.ToList();
            //    .Where(a =>
            //(a.StatusMaster.StatusId == 2
            //|| a.StatusMaster.StatusId == 3
            //|| a.StatusMaster.StatusId == 6)).ToList();

            Dictionary<string, List<TaskMaster>> list = new Dictionary<string, List<TaskMaster>>();

            foreach (var item in db.ExpertMasters.ToList())
            {
                var d = tlist.Where(a => a.ExpertMaster1.ExpertId == item.ExpertId).ToList();
                if(d.Count==0)
                {
                    continue;
                }
                list.Add(item.ExpertName, d);
            }

            //List<Dictionary<string, List<TaskMaster>>> list = new List<Dictionary<string, List<TaskMaster>>>();

            //var expertList = db.ExpertMasters.ToList().OrderBy(a => a.ExpertName);


            //foreach (var item in expertList)
            //{
            //    var data = db.TaskMasters.Where(a => a.RefExpertName == item.ExpertId);
            //    Dictionary<string, List<TaskMaster>> expertData = new Dictionary<string, List<TaskMaster>>();

            //    expertData[item.ExpertName] = data.ToList();
            //    list.Add(expertData);
            //}

            return View(list);
        }
    }
}
using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.Mvc;

using DashReportingTool.Models;

namespace DashReportingTool.Areas.Manage.Controllers
{
    [AuthenticateFilter(Roles = "admin,manager,team lead")]

    public class HomeManageController : Controller
    {
        dbReportingEntities db = new dbReportingEntities();
        // GET: Manage/HomeManage
        public ActionResult Index()
        {
            return View();
        }
        public ActionResult Main()
        {
            return View();
        }

        public JsonResult getExperts()
        {
            var data = db.ExpertMasters.Where(a => a.Designation.ToLower() != "admin").ToList();
            return Json(data.Select(a => new { a.ExpertName, a.ExpertEmailid, a.ExpertMobile, TotalBatch = a.BatchMasters.ToList().Count }),JsonRequestBehavior.AllowGet);
        }
    }
}
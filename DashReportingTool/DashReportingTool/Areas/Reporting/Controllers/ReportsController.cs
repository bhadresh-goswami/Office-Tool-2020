using System;
using System.Collections.Generic;
using System.Data;
using System.Data.Entity;
using System.Linq;
using System.Net;
using System.Web;
using System.Web.Mvc;
using DashReportingTool.Models;

namespace DashReportingTool.Areas.Reporting.Controllers
{
    [AuthenticateFilter(Roles = "admin,manager")]
    public class ReportsController : Controller
    {
        dbReportingEntities db = new dbReportingEntities();
        // GET: Reporting/Reports
        public ActionResult Index(int? id, DateTime? startDate, DateTime? endDate)
        {
            if (id != null)
            {
                var data = db.TaskMasters.Include(a => a.ExpertMaster).Include(a => a.StatusMaster).Where(a => a.RefExpertName == id).ToList();
                if (startDate != null && endDate != null)
                {
                    DateTime sd = (DateTime)startDate;
                    DateTime ed = (DateTime)endDate;
                    var details = data.Where(a => a.TaskDate.Date >= sd.Date && a.TaskDate <= ed.Date);
                    var dcount = details.ToList().Count;
                    return View(details);
                }

                return View(data);
            }
            return View(db.TaskMasters.Include(a => a.ExpertMaster).Include(a => a.StatusMaster));
        }

        public ActionResult LoginDetails(int? id, DateTime? startDate, DateTime? endDate)
        {
            if (id != null)
            {
                var data = db.LoginLogMasters.Include(a => a.ExpertMaster).Where(a => a.expertId == id).ToList().OrderByDescending(a=>a.logid);
                if (startDate != null && endDate != null)
                {
                    DateTime sd = (DateTime)startDate;
                    DateTime ed = (DateTime)endDate;
                    var details = data.Where(a => a.logdate >= sd.Date && a.logdate <= ed.Date);
                    var dcount = details.ToList().Count;
                    return View(details.OrderByDescending(a => a.logid));
                }

                return View(data.OrderByDescending(a => a.logid));
            }
            return View(db.LoginLogMasters.Include(a => a.ExpertMaster).OrderByDescending(a => a.logid));
        }
    }
}
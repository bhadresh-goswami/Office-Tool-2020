using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.Mvc;

using DashReportingTool.Models;

namespace DashReportingTool.Areas.TechnicalExperts.Controllers
{
    [AuthenticateFilter(Roles = "technical expert,team lead")]
    public class BatchesController : Controller
    {
        dbReportingEntities db = new dbReportingEntities();
        int id = 0;
        public BatchesController()
        {
            //id = int.Parse(Session["UserId"].ToString());
        }
        // GET: TechnicalExperts/Batches
        public ActionResult Index()
        {
            List<SelectListItem> lst = new List<SelectListItem>();
            foreach (var item in db.BatchMasters)
            {
                lst.Add(new SelectListItem() { Text = item.BatchName, Value = item.BatchId.ToString()});
            }

            List<SelectListItem> chk = new List<SelectListItem>();
            

            ViewBag.RefBatchTitle = lst;
            id = int.Parse(Session["UserId"].ToString());
            return View(db.BatchMasters.Where(a => a.RefExpertName == id));
        }
    }
}
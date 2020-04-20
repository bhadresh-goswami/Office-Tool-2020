using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.Mvc;

using DashReportingTool.Models;
namespace DashReportingTool.Areas.Manage.Controllers
{
    public class jsonApiController : Controller
    {
        dbReportingEntities db = new dbReportingEntities();
        // GET: Manage/jsonApi
        public JsonResult getTechs()
        {
            return Json(db.TechMasters.Select(a => new { a.TechName }), JsonRequestBehavior.AllowGet);
        }

        public JsonResult getCandidateList(int id)
        {
            var data = from a in db.CandidateMasters
                       join s in db.CandidateCourseMasters
                       on a.CandidateId equals s.RefCandidateTitle
                       join
                       c in db.CandidateBatchMasters on
                       s.CCId equals c.RefCandidateCourseId
                       where c.BatchMaster.BatchId == id
                       select new
                       {
                           a.CandidateName,
                           c.BatchMaster.Technology,
                           c.CandidateCourseMaster.RefCandidateTitle
                       };
            return Json(data.Select(a => new { a.CandidateName, a.Technology, CandidateId = a.RefCandidateTitle }), JsonRequestBehavior.AllowGet);
        }

        public JsonResult getSessionList(int id)
        {
            var data = from b in db.BatchMasters
                       join s in db.SessionMasters
on b.BatchId equals s.RefBatchTitle
                       where b.BatchId == id
                       select new
                       {
                           b.BatchId,
                           b.BatchName,
                           b.BatchStatus,
                           s.SessionId,
                           s.PresentCandidates,
                           s.RefBatchTitle,
                           s.SessionDate,
                           s.SessionTitle,
                           s.SessionTopic
                       };

            return Json(data, JsonRequestBehavior.AllowGet);
        }

        public string StatusChanged(int id)
        {
            try
            {
                var data = db.TaskMasters.SingleOrDefault(a => a.TaskId == id);
                TimeSpan t = (TimeSpan)data.TaskStartTime;
                DateTime d = DateTime.Now;
                DateTime date1 = new DateTime(d.Year, d.Month, d.Day, t.Hours, t.Minutes, t.Seconds);
                DateTime date2 = DateTime.Now;
                TimeSpan ts = date2 - date1;

                data.TimeTaken = ts.Minutes;
                data.TaskEndTime = DateTime.Now.TimeOfDay;
                data.TaskDone = true;
                db.SaveChanges();
            }
            catch (Exception ex)
            {
                return ex.Message;
            }
            return "";
        }

        public JsonResult getPendingTaskFeeds(int id)
        {
          
            var PT = db.TaskMasters.Where(a => a.RefExpertName == id && !a.TaskDone && (a.AssignedTask == null || a.AssignedTask==false)).OrderByDescending(a => a.TaskDate).OrderByDescending(a => a.TaskStartTime).ToList();
          
            return Json(PT.Select(a => new
            {
                a.AssignedTask,
                a.EstimateTime,
                //a.ExpertMaster.ExpertName,
                a.RefExpertName,
                a.RefTaskTitle,
                TaskDate = new DateTime(a.TaskDate.Ticks).ToString("MM/dd/yyyy"),
                a.TaskDetails,
                a.TaskDone,
                TaskEndTime = new DateTime(a.TaskEndTime.Value.Ticks).ToString("hh:mm tt"),
                a.TaskId,
                TaskStartTime = new DateTime(a.TaskStartTime.Value.Ticks).ToString("hh:mm tt"),
                a.TaskTitleMaster.TaskTitle,
                a.TimeTaken
            }), JsonRequestBehavior.AllowGet);

        }


        public JsonResult getCompletedTaskFeeds(int id)
        {
            var tasks = db.TaskMasters.Where(a => a.RefExpertName == id).OrderByDescending(a => a.TaskDate).OrderByDescending(a => a.TaskStartTime).ToList();
            var completedTask = tasks.Where(a => a.TaskDone && a.TaskDate.Date == DateTime.Now.Date);
            var CT = completedTask.ToList();//db.TaskMasters.Where(a => a.RefExpertName == id && !a.TaskDone).OrderByDescending(a => a.TaskDate).OrderByDescending(a => a.TaskStartTime).ToList();

            return Json(CT.Select(a => new
            {
                a.AssignedTask,
                a.EstimateTime,
                //a.ExpertMaster.ExpertName,
                a.RefExpertName,
                a.RefTaskTitle,
                TaskDate = new DateTime(a.TaskDate.Ticks).ToString("MM/dd/yyyy"),
                a.TaskDetails,
                a.TaskDone,
                TaskEndTime = new DateTime(a.TaskEndTime.Value.Ticks).ToString("hh:mm tt"),
                a.TaskId,
                TaskStartTime = new DateTime(a.TaskStartTime.Value.Ticks).ToString("hh:mm tt"),
                a.TaskTitleMaster.TaskTitle,
                a.TimeTaken
            }), JsonRequestBehavior.AllowGet);

        }


        public JsonResult getAssignedTaskFeeds(int id)
        {
            //int id = int.Parse(Session["UserId"].ToString());

            var tasks = db.TaskMasters.Where(a => a.RefExpertName == id).OrderByDescending(a => a.TaskDate).OrderByDescending(a => a.TaskStartTime).ToList();
            //var pendingTask = tasks.Where(a => !a.TaskDone).ToList();
            //var completedTask = tasks.Where(a => a.TaskDone && a.TaskDate.Date == DateTime.Now.Date);

            var assignedTask = db.TaskMasters.Include("ExpertMaster").Where(a => a.RefExpertName == id && a.AssignedTask == true && !a.TaskDone);

            /*ViewBag.PT = pendingTask.ToList();
            ViewBag.CT = completedTask.ToList();
            ViewBag.APT = assignedTask.ToList();
            */
            var AT = assignedTask.ToList();//db.TaskMasters.Where(a => a.RefExpertName == id && !a.TaskDone).OrderByDescending(a => a.TaskDate).OrderByDescending(a => a.TaskStartTime).ToList();
            //pendingTask;
            //;
            // returnData["CT"] = completedTask.ToList();
            // returnData["APT"] = assignedTask.ToList();
            return Json(AT.Select(a => new
            {
                a.AssignedTask,
                a.EstimateTime,
                a.ExpertMaster.ExpertName,
                a.RefExpertName,
                a.RefTaskTitle,
                TaskDate = new DateTime(a.TaskDate.Ticks).ToString("MM/dd/yyyy"),
                a.TaskDetails,
                a.TaskDone,
                TaskEndTime = new DateTime(a.TaskEndTime.Value.Ticks).ToString("hh:mm tt"),
                a.TaskId,
                TaskStartTime = new DateTime(a.TaskStartTime.Value.Ticks).ToString("hh:mm tt"),
                a.TaskTitleMaster.TaskTitle,
                a.TimeTaken
            }), JsonRequestBehavior.AllowGet);

        }

        public JsonResult showTask(int id)
        {
            try
            {

                var task = db.TaskMasters.SingleOrDefault(a => a.TaskId == id);

                var dict = new Dictionary<string, object>();
                dict["TaskTitle"] = task.TaskTitleMaster.TaskTitle;
                dict["TaskDetails"] = task.TaskDetails;
                return Json(dict, JsonRequestBehavior.AllowGet);
            }
            catch (Exception ex)
            {
                var data =new Dictionary<string, string>();
                data["TaskTitle"] = "Server Side Error";

                data["TaskDetails"] = "Server Not Responding Please Try after sometime!";
                return Json(data,JsonRequestBehavior.AllowGet);
            }
        }

    }
}
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

            //var PT = db.TaskMasters.Where(a => a.RefExpertName == id && !a.TaskDone && (a.AssignedTask == null || a.AssignedTask == false)).OrderByDescending(a => a.TaskDate).OrderByDescending(a => a.TaskStartTime).ToList();
            var PT = db.TaskMasters.Where(a => a.RefExpertName == id && a.RefStatusTitle == 2 && 
            (a.AssignedTask == null || a.AssignedTask == false))
                .OrderByDescending(a => a.TaskDate)
                .OrderByDescending(a => a.TaskStartTime).ToList();
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
                var data = new Dictionary<string, string>();
                data["TaskTitle"] = "Server Side Error";

                data["TaskDetails"] = "Server Not Responding Please Try after sometime!";
                return Json(data, JsonRequestBehavior.AllowGet);
            }
        }

        public string StartTask(int id, int userid)
        {
            try
            {
                var anyPending = db.TaskMasters.Where(a => a.RefExpertName == userid && a.TaskDone == false && a.AssignedTask == false && a.RefStatusTitle==2).ToList().Count;
                if (anyPending > 0)
                {
                    return "Complete Pending Task First!";
                }

                var data = db.TaskMasters.SingleOrDefault(a => a.TaskId == id);
                //TimeSpan t = (TimeSpan)data.TaskStartTime;
                //DateTime d = DateTime.Now;
                //DateTime date1 = new DateTime(d.Year, d.Month, d.Day, t.Hours, t.Minutes, t.Seconds);
                //DateTime date2 = DateTime.Now;
                //TimeSpan ts = date2 - date1;

                //data.TimeTaken = ts.Minutes;
                //data.TaskEndTime = DateTime.Now.TimeOfDay;
                data.RefStatusTitle = 2;
                data.TaskStartTime = DateTime.Now.TimeOfDay;
                data.IsStartedTask = true;
                db.SaveChanges();
            }
            catch (Exception ex)
            {
                return ex.Message;
            }
            return "Task Started!";
        }

        public JsonResult UpdatePassword(string oldp, string newp, int userid)
        {
            var dict = new Dictionary<string, string>();
            try
            {
                var expert = db.ExpertMasters.SingleOrDefault(a => a.ExpertId == userid && a.ExpertPassword == oldp);
                if (expert != null)
                {
                    expert.ExpertPassword = newp;
                    db.SaveChanges();
                    dict["msg"] = "Changed Password";
                    return Json(dict, JsonRequestBehavior.AllowGet);

                }
                dict["msg"] = "User Details Not Found!";
            }
            catch (Exception ex)
            {

                dict["msg"] = ex.Message;
            }
            return Json(dict, JsonRequestBehavior.AllowGet);
        }


        public JsonResult UpdateTaskStatus(int id, int sid, int uid)
        {
            var dict = new Dictionary<string, string>();
            var task = db.TaskMasters.Find(id);
            
            var tasks = db.TaskMasters.Where(a => a.RefExpertName == uid && a.RefStatusTitle == 2);
            int reny = tasks.ToList().Count;
            try
            {
                var st = db.StatusMasters.Find(sid);
                if (sid == 5)
                {
                    task.RefStatusTitle = sid;
                    task.IsStartedTask = false;
                    task.TaskDone = false;
                    task.TimeTaken = 0;
                    db.SaveChanges();
                    //reassigned
                }
                else if (sid == 2)
                {
                    if (reny > 0)
                    {
                        dict["msg"] = "Some task is running you can not complete this task now!";
                        return Json(dict, JsonRequestBehavior.AllowGet);

                    }
                    task.RefStatusTitle = sid;
                    task.IsStartedTask = true;
                    task.TaskDone = false;
                    task.TimeTaken = 0;
                    db.SaveChanges();
                    //reassigned
                }
                else if (sid == 3)
                {

                    task.RefStatusTitle = sid;
                    task.IsStartedTask = false;
                    task.TaskDone = false;
                    TimeSpan stime = (TimeSpan)task.TaskStartTime;
                    TimeSpan etime = DateTime.Now.TimeOfDay;
                    TimeSpan t = etime.Subtract(stime);
                    int hrToM = t.Hours * 60;
                    int tmin = t.Minutes + hrToM;
                    task.TimeTaken = tmin;
                    task.TaskEndTime = DateTime.Now.TimeOfDay;
                    task.RefStatusTitle = sid;
                    db.SaveChanges();
                    //hold
                }
                else if (sid == 1)
                {
                   if(task.RefStatusTitle == 3 || task.RefStatusTitle == 2)
                    {
                        
                        task.IsStartedTask = true;
                        task.TaskDone = true;
                        TimeSpan stime = (TimeSpan)task.TaskStartTime;
                        TimeSpan etime = DateTime.Now.TimeOfDay;
                        TimeSpan t = etime.Subtract(stime);
                        
                        int hrToM = t.Hours * 60;
                        int tmin = t.Minutes + hrToM;
                        if (task.RefStatusTitle == 3)
                        {
                            tmin -= (int)task.TimeTaken;
                        }
                        task.RefStatusTitle = sid;
                        task.TimeTaken = tmin;
                        task.RefStatusTitle = sid;
                        task.TaskEndTime = DateTime.Now.TimeOfDay;

                        db.SaveChanges();
                        dict["msg"] = "Status Changed to " + st.StatusTitle;
                        return Json(dict, JsonRequestBehavior.AllowGet);

                    }
                    if (reny > 0)
                        {
                            dict["msg"] = "Some task is running you can not complete this task now!";
                            return Json(dict, JsonRequestBehavior.AllowGet);

                        }
                   
                   
                    //completed
                }
                dict["msg"] = "Status Changed to " + st.StatusTitle;
            }
            catch (Exception ex)
            {
                dict["msg"] = ex.Message;
                return Json(dict, JsonRequestBehavior.AllowGet);


            }
            return Json(dict, JsonRequestBehavior.AllowGet);

        }

        public JsonResult getTaskList(int id, int uid)
        {
            var list = db.TaskMasters.Include(a => a.ExpertMaster).Include(a=>a.StatusMaster).Where(a=>a.RefExpertName == uid && a.RefStatusTitle==id).OrderByDescending(a=>a.TaskDate).ToList();
            return Json(list.Select(a=>new { 
                a.AssignedTask,
                DeadLine = new DateTime(((DateTime)a.DeadLine).Ticks).ToString("MM/dd/yyyy"),
                a.EstimateTime,
                TaskBy= a.ExpertMaster.ExpertName,
                a.ExpertMaster1.ExpertName,
                a.IsStartedTask,
                a.StatusMaster.StatusTitle,
                TaskDate = new DateTime(a.TaskDate.Ticks).ToString("MM/dd/yyyy"),
                a.TaskDetails,
                a.TaskDone,
                TaskEndTime = new DateTime(a.TaskEndTime.Value.Ticks).ToString("hh:mm tt"),
                a.TaskId,
                TaskStartTime = new DateTime(a.TaskStartTime.Value.Ticks).ToString("hh:mm tt"),
                a.TaskTitleMaster.TaskTitle,
                a.TimeTaken
            
            }),JsonRequestBehavior.AllowGet);
        }


    }
}
using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.Mvc;
using DashReportingTool.Models;

namespace DashReportingTool.Controllers
{
    public class AuthenticationController : Controller
    {
        dbReportingEntities db = new dbReportingEntities();
        // GET: Authentication
        public ActionResult Login()
        {
           // db.Database.Create();
            LoginModel login = new LoginModel();
            if(Request.Cookies["login"]!=null && Session["UserId"] == null)
            {
                HttpCookie cookie = Request.Cookies["login"];
                login.EmailId = cookie["emailid"].ToString();
                int id = login._GetId;
                Session["UserId"] = id;
                Session["UserType"] = login.Role;
                ViewBag.msg = "You are already Login!";
                var log = new LoginLogMaster();
                log.expertId = id;
                log.logdate = DateTime.Now.Date;
                log.login = DateTime.Now.TimeOfDay;
                log.status = "Auto Login because of not active for 10 min";
                db.LoginLogMasters.Add(log);
                db.SaveChanges();
                return RedirectToAction("Index", "Dashboard", new { @area = "User" });

            }
            return View();
        }

        [HttpPost]
        public ActionResult Login(LoginModel login)
        {
            try
            {
                ExpertMaster expert = db.ExpertMasters.SingleOrDefault(
                    a =>
                    a.ExpertEmailid == login.EmailId
                    &&
                    a.ExpertPassword == login.Password);
                if (expert != null)
                {
                    HttpCookie cookie = new HttpCookie("login");
                    cookie.Values.Add("emailid", login.EmailId);
                    cookie.Expires = DateTime.Now.AddDays(1);
                    Response.Cookies.Add(cookie);

                    Session["UserId"] = expert.ExpertId;
                    Session["UserType"] = expert.Designation;
                    ViewBag.msg = "Login Successfuly Done!";
                    var log = new LoginLogMaster();
                    log.expertId = expert.ExpertId;
                    log.logdate = DateTime.Now.Date;
                    log.login = DateTime.Now.TimeOfDay;
                    log.status = "Login";
                    db.LoginLogMasters.Add(log);
                    db.SaveChanges();
                    Session.Timeout = 10;
                    return RedirectToAction("Index", "Dashboard", new { @area = "User" });
                }

            }
            catch (Exception ex)
            {
                ViewBag.err = "Error:" + ex.Message;
            }
            ViewBag.err = "Email Id or Password is Wrong, Try Again with valid Details!";
            return View();
        }

        public ActionResult LogOut()
        {
            int id = int.Parse(Session["UserId"].ToString());
            Response.ClearHeaders();
            Response.AddHeader("Cache-Control", "no-cache, no-store, max-age=0, must-revalidate");
            Response.AddHeader("Pragma", "no-cache");
            //Request.Cookies.Remove("login");
            if (Request.Cookies["login"] != null)
            {
                HttpCookie cookie = Request.Cookies["login"];
                cookie.Expires = DateTime.Now.AddMilliseconds(1);
                Response.Cookies.Add(cookie);
            }
            Request.Cookies.Remove("login");
            Session.Abandon();

            var log = new LoginLogMaster();
            log.expertId = id;
            log.logdate = DateTime.Now.Date;
            log.logout = DateTime.Now.TimeOfDay;
            log.status = "LogOut";
            db.LoginLogMasters.Add(log);
            db.SaveChanges();

            return RedirectToAction("Login");
        }
    }
}
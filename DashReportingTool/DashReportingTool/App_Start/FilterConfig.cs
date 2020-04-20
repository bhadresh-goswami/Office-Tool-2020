using System;
using System.Web;
using System.Web.Mvc;
using System.Web.Routing;

namespace DashReportingTool
{
    public class FilterConfig
    {
        public static void RegisterGlobalFilters(GlobalFilterCollection filters)
        {
            filters.Add(new HandleErrorAttribute());
        }
    }

    public class AuthenticateFilter : AuthorizeAttribute
    {
        protected override bool AuthorizeCore(HttpContextBase httpContext)
        {
            bool authorize = false;
            if (httpContext.Session["UserId"] == null && httpContext.Session["UserType"] == null) { return authorize; }

            var userId = Convert.ToInt32(httpContext.Session["UserId"].ToString());
            var role = httpContext.Session["UserType"].ToString().ToLower();

            bool noMatch = false;
            
            foreach (var r in Roles.Split(','))
            {
                if (role.Equals(r.ToString().ToLower()))
                {
                    noMatch = true;
                    break;
                }
            }
            return noMatch;
        }

        protected override void HandleUnauthorizedRequest(AuthorizationContext filterContext)
        {
            filterContext.Result = new RedirectToRouteResult(
               new RouteValueDictionary
               {
                    { "controller", "Authentication" },
                    { "action", "Login" },
                       { "area",""}
               });
        }

        //protected override void HandleUnauthorizedRequest(AuthorizationContext filterContext)
        //{
        //    RedirectToRouteResult routeData = null;

        //    if(HttpContext.Current.Session["UserId"]==null)
        //    {
        //        routeData = new RedirectToRouteResult
        //            (new System.Web.Routing.RouteValueDictionary
        //            (new
        //            {
        //                controller = "Authentication",
        //                action = "Login",
        //            }
        //            ));
        //    }
        //    int id = Convert.ToInt32(HttpContext.Current.Session["UserId"].ToString());
        //    string role = HttpContext.Current.Session["UserType"].ToString().ToLower();
        //    bool noMatch = true;
        //    foreach (var r in Roles.Split(','))
        //    {
        //        if(role.Equals(r.ToString().ToLower()))
        //        {
        //            noMatch = false;
        //            break;
        //        }
        //    }
        //    if(noMatch)
        //    {
        //        routeData = new RedirectToRouteResult
        //           (new System.Web.Routing.RouteValueDictionary
        //           (new
        //           {
        //               controller = "Authentication",
        //               action = "Login",
        //           }
        //           ));
        //    }
        //    //if (CurrentUser == null)
        //    //{
        //    //    routeData = new RedirectToRouteResult
        //    //        (new System.Web.Routing.RouteValueDictionary
        //    //        (new
        //    //        {
        //    //            controller = "Account",
        //    //            action = "Login",
        //    //        }
        //    //        ));
        //    //}
        //    //else
        //    //{
        //    //    routeData = new RedirectToRouteResult
        //    //    (new System.Web.Routing.RouteValueDictionary
        //    //     (new
        //    //     {
        //    //         controller = "Error",
        //    //         action = "AccessDenied"
        //    //     }
        //    //     ));
        //    //}

        //    filterContext.Result = routeData;
        //}

        //public override void OnAuthorization(AuthorizationContext filterContext)
        //{
        //    base.OnAuthorization(filterContext);
        //}
    }

}

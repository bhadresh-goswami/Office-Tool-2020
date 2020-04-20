using System.Web.Mvc;

namespace DashReportingTool.Areas.TechnicalExperts
{
    public class TechnicalExpertsAreaRegistration : AreaRegistration 
    {
        public override string AreaName 
        {
            get 
            {
                return "TechnicalExperts";
            }
        }

        public override void RegisterArea(AreaRegistrationContext context) 
        {
            context.MapRoute(
                "TechnicalExperts_default",
                "TechnicalExperts/{controller}/{action}/{id}",
                new { action = "Index", id = UrlParameter.Optional }
            );
        }
    }
}
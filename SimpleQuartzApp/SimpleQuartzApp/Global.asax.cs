using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.Mvc;
using System.Web.Optimization;
using System.Web.Routing;
using SimpleQuartzApp.Models;

namespace SimpleQuartzApp
{
    public class MvcApplication : System.Web.HttpApplication
    {
        public static Quartz.IScheduler Scheduler = Quartz.Impl.StdSchedulerFactory.GetDefaultScheduler();

        protected void Application_Start()
        {
            AreaRegistration.RegisterAllAreas();
            FilterConfig.RegisterGlobalFilters(GlobalFilters.Filters);
            RouteConfig.RegisterRoutes(RouteTable.Routes);
            BundleConfig.RegisterBundles(BundleTable.Bundles);
            new DatabaseScheduler().Start();
        }
    }
}

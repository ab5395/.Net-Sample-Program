using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using System.Web;
using System.Web.Mvc;
using Quartz;
using Quartz.Impl;
using SimpleQuartzApp.Models;
using DatabaseScheduler = SimpleQuartzApp.Models.DatabaseScheduler;

namespace SimpleQuartzApp.Controllers
{
    public class HomeController : Controller
    {
        public static int i = 0;
        public ActionResult Index()
        {
            return View();
        }

        public ActionResult About()
        {
            ViewBag.Message = "Your application description page.";
            return View();
        }

        [HttpPost]
        public ActionResult About(FormCollection fc)
        {
            ViewBag.Message = "Your application description page.";
            if (fc["btnSubmit"]=="TestStart")
            {
                CheckScheduler();

                DatabaseScanContext obj = new DatabaseScanContext();
                i = i + 1;
                obj.JobName = "Job" + i;
                new DatabaseScheduler().AddTaskJob(obj); 
            }
            if (fc["btnStop"] == "TestStop")
            {
                DatabaseScanContext obj = new DatabaseScanContext();
                i = i + 1;
                obj.JobName = "Job" + i;
                new DatabaseScheduler().Stop();
            }

            return RedirectToAction("About");
        }

        public ActionResult Contact()
        {
            ViewBag.Message = "Your contact page.";

            return View();
        }

        public void CheckScheduler()
        {
            ISchedulerFactory schedFact = new StdSchedulerFactory();
            foreach (IScheduler scheduler in schedFact.AllSchedulers)
            {
                var scheduler1 = scheduler;
               
            }
        }
    }
}
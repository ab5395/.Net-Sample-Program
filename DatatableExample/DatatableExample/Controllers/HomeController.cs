using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.Mvc;
using System.Web.Script.Serialization;
using DatatableExample.Models;

namespace DatatableExample.Controllers
{
    public class HomeController : Controller
    {
        public ActionResult Index()
        {
            return View();
        }

        public ActionResult About()
        {
            ViewBag.Message = "Your application description page.";

            return View();
        }

        public ActionResult Contact()
        {
            ViewBag.Message = "Your contact page.";

            return View();
        }

        public ActionResult AjaxDataProvider1(JQueryDataTableParamModel param)
        {
            List<SelectData> people = new List<SelectData>{
                new SelectData("Data1"),
                new SelectData("Data2"),
                new SelectData("Data3"),
                new SelectData("Data4"),
            };
            JavaScriptSerializer serializer = new JavaScriptSerializer();
            var pp = serializer.Serialize(people);
            return this.Json(pp, JsonRequestBehavior.AllowGet);
        }

        public ActionResult DataList(JQueryDataTableParamModel param)
        {
            var selectList = new List<SelectData>
            {
                new SelectData("Data1"),
                new SelectData("Data2"),
                new SelectData("Data3"),
                new SelectData("Data4"),
            };
            var list = new List<Data>
           {
               new Data(1,"Test",selectList[0]),
               new Data(2,"Test",selectList[1]),
               new Data(3,"Test",selectList[2]),
               new Data(4,"Test",selectList[0]),
               new Data(5,"Test",selectList[2]),
               new Data(6,"Test",selectList[0]),
           };
            return Json(new
            {
                sEcho = param.sEcho,
                iTotalRecords = 6,
                iTotalDisplayRecords = 10,
                aaData = list
            }, JsonRequestBehavior.AllowGet);
        }

        public ActionResult DynamicDivTest()
        {
            return View();
        }
    }

    public class Data
    {
        public int VideoId { get; set; }
        public string CategoryName { get; set; }
        public SelectData VideoName { get; set; }
        public Data(int id, string name, SelectData name1)
        {
            VideoId = id;
            CategoryName = name;
            VideoName = name1;
        }
    }

    public class SelectData
    {
        public string selectItem { get; set; }

        public SelectData(string name)
        {
            selectItem = name;
        }
    }
}
using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.Mvc;
using RestSharp;

namespace EmpSolution.Controllers
{
    public class DataTableController : Controller
    {
        public static List<Student> StudentList = new List<Student>();

        // GET: DataTable
        public ActionResult Index()
        {
            return View();
        }

        public JsonResult GetStudent()
        {
            for (int i = 1; i <= 5; i++)
            {
                Student st = new Student()
                {
                    Rno = i,
                    Name = new Guid().ToString(),
                    Date = System.DateTime.Now
                };
                StudentList.Add(st);
            }
            return Json(StudentList, JsonRequestBehavior.AllowGet);
        }



    }

    public class Student
    {
        public int Rno { get; set; }
        public string Name { get; set; }
        public DateTime Date { get; set; }
    }
}
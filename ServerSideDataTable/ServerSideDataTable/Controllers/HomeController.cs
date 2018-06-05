using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.Mvc;
using ServerSideDataTable.Models;

namespace ServerSideDataTable.Controllers
{
    public class HomeController : Controller
    {
        public ActionResult Index()
        {
            return View();
        }

        public ActionResult DatatableTest()
        {
            return View();
        }

        [HttpGet]
        public ActionResult Getdatatables(JQueryDataTableParamModel param)
        {
            EmployeeDbEntities _dbcontext = new EmployeeDbEntities();
            using (_dbcontext)
            {
                var result = _dbcontext.Tests;
                IEnumerable<Test> filteredTests = result;
                //Searching
                if (!string.IsNullOrEmpty(param.sSearch))
                {
                    //filteredTests = result
                    //         .Where(c => c.EmployeeName.ToLower().Contains(param.sSearch.ToLower()) ||
                    //                     c.EmployeeType.ToLower().Contains(param.sSearch));
                    filteredTests = result.Where(x => x.Name.ToLower().Contains(param.sSearch.ToLower()));
                }
                //sorting
                var sortColumnIndex = Convert.ToInt32(HttpContext.Request.QueryString["iSortCol_0"]);
                //Func<Test, string> orderingFunction = (c => sortColumnIndex == 0 ? c.Rno.ToString() : sortColumnIndex == 1 ? c.Name.ToString() : sortColumnIndex == 2 ? c.EmployeeName : sortColumnIndex == 3 ? c.EmployeeType : c.EmployeeDesignation);
                //Func<Test, int> orderingFunction = (c => sortColumnIndex == 0 ? c.Rno : 0);  for int sorting

                Func<Test, string> orderingFunction = c => sortColumnIndex == 0 ? c.Rno.ToString() : c.Name.ToString();
                var sortDirection = HttpContext.Request.QueryString["sSortDir_0"]; // asc or desc
                filteredTests = sortDirection == "asc" ? filteredTests.OrderBy(orderingFunction) : filteredTests.OrderByDescending(orderingFunction);

                //Pagination
                var displayedTest = filteredTests.Skip(param.iDisplayStart)
                       .Take(param.iDisplayLength);
                var totalRecords = filteredTests.Count();
                return Json(new
                {
                    sEcho = param.sEcho,
                    iTotalRecords = totalRecords,
                    iTotalDisplayRecords = totalRecords,
                    aaData = displayedTest.ToList()
                }, JsonRequestBehavior.AllowGet);
            }
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
    }
}
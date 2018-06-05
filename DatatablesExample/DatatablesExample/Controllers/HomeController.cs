using System;
using System.Collections.Generic;
using System.Linq;
using System.Web.Mvc;
using DatatablesExample.Models;

namespace DatatablesExample.Controllers
{
    public class HomeController : Controller
    {
        OrgEntities _dbcontext=new OrgEntities();
        public ActionResult Index()
        {

            return View();
        }

        public ActionResult About()
        {
            ViewBag.Message = "Your application description page.";

            return View();
        }


        public ActionResult Employee()
        {
            return View();
        }

        [HttpGet]
        public ActionResult Getdatatables(JQueryDataTableParamModel param)

        {
            using (_dbcontext)
            {
                var result = _dbcontext.Employees;
                IEnumerable<Employee> filteredEmployee = result;
                //Searching
                if (!string.IsNullOrEmpty(param.sSearch))
                {
                    filteredEmployee = result
                             .Where(c => c.EmployeeName.ToLower().Contains(param.sSearch.ToLower()) ||
                                         c.EmployeeType.ToLower().Contains(param.sSearch));
                }
                //sorting
                var sortColumnIndex = Convert.ToInt32(HttpContext.Request.QueryString["iSortCol_0"]);
                Func<Employee,string> orderingFunction = (c => sortColumnIndex == 0 ? c.Id.ToString():sortColumnIndex == 1 ? c.Date.ToString() : sortColumnIndex == 2 ? c.EmployeeName : sortColumnIndex ==3 ? c.EmployeeType:  c.EmployeeDesignation );
               
                


                var sortDirection = HttpContext.Request.QueryString["sSortDir_0"]; // asc or desc
                filteredEmployee = sortDirection == "asc" ? filteredEmployee.OrderBy(orderingFunction) : filteredEmployee.OrderByDescending(orderingFunction);

                //Pagination
                var displayedEmployee = filteredEmployee.Skip(param.iDisplayStart)
                       .Take(param.iDisplayLength);
                var totalRecords = filteredEmployee.Count();
                return Json(new
                {
                    sEcho = param.sEcho,
                    iTotalRecords = totalRecords,
                    iTotalDisplayRecords = totalRecords,
                    aaData = displayedEmployee.ToList()
                },JsonRequestBehavior.AllowGet);
            }
        }

        public ActionResult Contact()
        {
            ViewBag.Message = "Your contact page.";

            return View();
        }
    }
}
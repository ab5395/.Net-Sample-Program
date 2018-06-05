using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.Mvc;
using RestSharp;

namespace EmpSolution.Controllers
{
    public class HomeController : Controller
    {
        [HttpGet]
        public ActionResult Index()
        {
            return View();
        }

        public JsonResult BindEmployeeById(string eid)
        {
            int i = int.Parse(eid);
            var client = new RestClient("http://localhost:52478/Employee/GetEmployeeById?id=" + i);
            var request = new RestRequest(Method.GET);
            IRestResponse response = client.Execute(request);
            return Json(new { success = true, data = response.Content }, JsonRequestBehavior.AllowGet);
        }

        public JsonResult BindCountry()
        {
            var client = new RestClient("http://localhost:52478/Common/GetCountry");
            var request = new RestRequest(Method.GET);
            IRestResponse response = client.Execute(request);
            return Json(new { success = true, data = response.Content }, JsonRequestBehavior.AllowGet);
        }

        public JsonResult BindState(string cid)
        {
            int i = int.Parse(cid);
            var client = new RestClient("http://localhost:52478/Common/GetStateByCountryId?cid=" + i);
            var request = new RestRequest(Method.GET);
            IRestResponse response = client.Execute(request);
            return Json(new { success = true, data = response.Content }, JsonRequestBehavior.AllowGet);
        }

        public JsonResult BindCity(string sid)
        {
            int i = int.Parse(sid);
            var client = new RestClient("http://localhost:52478/Common/GetCityByStateId?sid=" + i);
            var request = new RestRequest(Method.GET);
            IRestResponse response = client.Execute(request);
            return Json(new { success = true, data = response.Content }, JsonRequestBehavior.AllowGet);
        }

        public JsonResult GetEmpList()
        {
            var client = new RestClient("http://localhost:52478/Employee/GetEmployee");
            var request = new RestRequest(Method.GET);
            IRestResponse response = client.Execute(request);
            return Json(new { success = true, data = response.Content }, JsonRequestBehavior.AllowGet);
        }

        public JsonResult AddEmployee(string city, string name, string department, string mobile)
        {
            var client = new RestClient("http://localhost:52478/Employee/AddEmployee");
            var request = new RestRequest(Method.POST);
            request.AddParameter("application/json", "{\n\t\"Name\":\"" + name + "\",\n\t\"Department\":\"" + department + "\",\n\t\"CityId\":\"" + city
                + "\",\n\t\"Mobile\":\"" + mobile + "\"\n}", ParameterType.RequestBody);
            IRestResponse response = client.Execute(request);
            return Json(new { success = response.Content }, JsonRequestBehavior.AllowGet);
        }

        public JsonResult UpdateEmployee(string eid,string city, string name, string department, string mobile)
        {
            var client = new RestClient("http://localhost:52478/Employee/EditEmployee");
            var request = new RestRequest(Method.POST);
            request.AddParameter("application/json", "{\n\t\"EmpId\":\""+eid+"\",\n\t\"Name\":\""+name+"\",\n\t\"Department\":\""+department+"\",\n\t\"CityId\":\""+city+"\",\n\t\"Mobile\":\""+mobile+"\",\n}", ParameterType.RequestBody);
            IRestResponse response = client.Execute(request);
            return Json(new { success = response.Content }, JsonRequestBehavior.AllowGet);
        }

        public JsonResult DeleteEmployee(string eid)
        {
            var client = new RestClient("http://localhost:52478/Employee/DeleteEmployee");
            var request = new RestRequest(Method.POST);
            request.AddParameter("application/json", "{\n\t\"EmpId\":\"" + eid + "\"\n}", ParameterType.RequestBody);
            IRestResponse response = client.Execute(request);
            return Json(new { success = response.Content }, JsonRequestBehavior.AllowGet);
        }
    }
}
using System;
using System.Collections.Generic;
using System.Linq;
using System.Net;
using System.Net.Http;
using System.Web.Http;
using Business.API;
using Data.API;

namespace WebAPIApp.Controllers
{
    [RoutePrefix("Employee")]
    public class EmployeeApiController : ApiController
    {
        public EmployeeDomain Ed = new EmployeeDomain();

        [Route("GetEmployee")]
        [HttpGet]
        public IEnumerable<Employee> GetEmployeeList()
        {
            return Ed.GetEmployeeList();
        }

        [Route("GetEmployeeById")]
        [HttpGet]
        public IEnumerable<Employee> GetEmployeeListByEmployeeId(int id)
        {
            return Ed.GetEmployeeList().Where(x => x.EmpId == id).ToList();
        }

        [Route("AddEmployee")]
        [HttpPost]
        public bool AddEmployees(Employee employee)
        {
            Ed.AddEmployee(employee);
            return true;
        }

        [Route("EditEmployee")]
        [HttpPost]
        public bool UpdateEmployee(Employee employee)
        {
            Ed.UpdateEmployee(employee);
            return true;
        }

        [Route("DeleteEmployee")]
        [HttpPost]
        public bool DeleteEmployee(Employee employee)
        {
            Ed.DeleteEmployee(employee);
            return true;
        }

    }
}

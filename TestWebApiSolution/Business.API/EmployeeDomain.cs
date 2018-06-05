using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Data.API;

namespace Business.API
{
    public class EmployeeDomain
    {
        public EmployeeDbEntities Ee = new EmployeeDbEntities();

        //Employee
        public void AddEmployee(Employee employee)
        {
            Ee.Employees.Add(employee);
            Ee.SaveChanges();
        }

        public void DeleteEmployee(Employee employee)
        {
            Employee data = Ee.Employees.FirstOrDefault(x => x.EmpId == employee.EmpId);
            Ee.Employees.Remove(data);
            Ee.SaveChanges();
        }

        public void UpdateEmployee(Employee employee)
        {
            Employee data = Ee.Employees.FirstOrDefault(x => x.EmpId == employee.EmpId);
            if (data != null)
            {
                data.Name = employee.Name;
                data.Department = employee.Department;
                data.CityId = employee.CityId;
                data.Mobile = employee.Mobile;
            }
            Ee.SaveChanges();
        }

        public IEnumerable<Employee> GetEmployeeList()
        {
            return Ee.Employees.ToList();
        }

        //Image
        public void AddImage(Image image)
        {
            Ee.Images.Add(image);
            Ee.SaveChanges();
        }

        public void DeleteImage(Image image)
        {
            Image data = Ee.Images.FirstOrDefault(x => x.ImageId == image.ImageId);
            Ee.Images.Remove(data);
            Ee.SaveChanges();
        }

        public IEnumerable<Image> GetImageList()
        {
            return Ee.Images.ToList();
        }

        public IEnumerable<Image> GetImageListByEmpId(Employee employee)
        {
            return Ee.Images.Where(x => x.EmployeeId == employee.EmpId).ToList();
        }
    }
}

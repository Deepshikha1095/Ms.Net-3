using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace DalLib
{
    public class FakeDal : IDal
    {
        static List<Employee> empList = new List<Employee>();
        public FakeDal()
        {
            empList.Add(new Employee { Id = 101, Name = "Sita", Dept = 501 });
            empList.Add(new Employee { Id = 102, Name = "gita", Dept = 502 });
            empList.Add(new Employee { Id = 103, Name = "sunita", Dept = 501 });
            empList.Add(new Employee { Id = 106, Name = "Rita", Dept = 504 });
            empList.Add(new Employee { Id = 105, Name = "Mita", Dept = 503 });
        }
        public bool AddEmployee(Employee emp)
        {
            empList.Add(emp);

            return true;
        }

        public List<Employee> GetAllEmployee()
        {
            return empList;
        }

        public Employee GetEmployeeById(int id)
        {
            var emp= empList.Where(x => x.Id == id).FirstOrDefault();
            return emp;
        }

        public bool ModifyEmployee(Employee emp)
        {
            var emp1 = empList.Where(x => x.Id == emp.Id).FirstOrDefault();
            emp1.Name = emp.Name;
            emp1.Dept = emp.Dept;
            return true;
        }

        public bool RemoveEmployee(int id)
        {
            var emp = empList.Where(x => x.Id == id).FirstOrDefault();
            return empList.Remove(emp);
        }
    }
}

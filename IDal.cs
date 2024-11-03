using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace DalLib
{
    public interface IDal
    {
        List<Employee> GetAllEmployee();
        Employee GetEmployeeById(int id);
        bool AddEmployee(Employee emp);
        bool ModifyEmployee(Employee emp);
        bool RemoveEmployee(int id);

    }
}

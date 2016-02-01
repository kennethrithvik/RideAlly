using Rideally.Entities;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Rideally.Business.Contract
{
    public interface IEmployeeManager
    {
        List<Employee> GetAllEmployees();
        bool AddEmployee(Employee employee);
        Employee GetEmployeeByID(int id);

        bool UpdateEmployee(Employee employee);

        bool DeleteEmployee(Employee employee);

    }
}

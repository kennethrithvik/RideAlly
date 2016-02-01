using Rideally.Business.Contract;
using Rideally.Data.Repository;
using Rideally.Entities;
using Rideally.Exceptions;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Rideally.Business.Impementation
{
    public class EmployeeManager : IEmployeeManager
    {
        IGenericRepository<Employee> EmployeeRepo = null;
        public EmployeeManager(IUnitOfWork uow)
        {
            EmployeeRepo = uow.GetGenericRepository<Employee>();
        }

        public List<Entities.Employee> GetAllEmployees()
        {
            List<Employee> Employee = new List<Employee>();
            try
            {
                Employee = EmployeeRepo.GetAll().ToList();
            }
            catch (Exception)
            {
                throw new NullReferenceException("No Address Found");
            }

            return Employee;
        }

        public bool AddEmployee(Entities.Employee employee)
        {
            bool IsAdded = false;
            if (employee == null)
                throw new NullReferenceException("Cannot insert Null value");
            //Employee emp = new Employee();
            try
            {
                EmployeeRepo.Create(employee);
                IsAdded = true;
            }
            catch (Exception)
            {
                throw new EmployeeNotAddedException("Cannot Add Employee");
            }
            return IsAdded;
        }

        public Entities.Employee GetEmployeeByID(int id)
        {
            if (id == 0)
                throw new NullReferenceException("No Employee Found With id" + id);

            Employee emp = null;
            try
            {
               emp = EmployeeRepo.GetAll().FirstOrDefault(x => x.EmployeeID == id);
            }
            catch (Exception)
            {
                throw new NoEmployeeFoundException("No Employee Found");
            }
            return emp;
        }

        public bool UpdateEmployee(Employee employee)
        {
            bool IsUpdated = false;
            if (employee == null)
                throw new NullReferenceException("Cannot update Null value");
            //Employee emp = new Employee();
            try
            {
                //EmployeeRepo.Detach();
                //EmployeeRepo.Detach(employee.OfficeAddress);
                EmployeeRepo.Update(employee);
                IsUpdated = true;
            }
            catch (Exception ex)
            {
                throw new EmployeeNotAddedException("Cannot update Employee" + ex);
            }
            return IsUpdated;
        }

        public bool DeleteEmployee(Employee employee)
        {
            bool IsDeleted = false;
            if (employee == null)
                throw new NullReferenceException("Cannot delete Null value");
            try
            {
                EmployeeRepo.Delete(employee);
                IsDeleted = true;
            }
            catch (Exception)
            {
                throw new EmployeeNotAddedException("Cannot Delete Employee");
            }
            return IsDeleted;
        }

    }
}

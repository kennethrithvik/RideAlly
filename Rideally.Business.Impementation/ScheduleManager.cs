using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using Rideally.Exceptions;
using System.Threading.Tasks;
using Rideally.Business.Contract;
using Rideally.Data.Repository;
using Rideally.Entities;


namespace Rideally.Business.Impementation
{
   public class ScheduleManager:IScheduleManager
    {
        IGenericRepository<Schedule> ScheduleRepo = null;
        IGenericRepository<Employee> EmployeeRepo = null;
        IGenericRepository<Address> AddressRepo = null;
       
       
        public ScheduleManager(IUnitOfWork uow)
        {
            ScheduleRepo = uow.GetGenericRepository<Schedule>();
            EmployeeRepo = uow.GetGenericRepository<Employee>();
            AddressRepo = uow.GetGenericRepository<Address>();
        }
        public List<Entities.Schedule> GetAllSchedule()
        {
            //throw new NotImplementedException();
            List<Schedule> schedule = new List<Schedule>();
            try
            {
                schedule = ScheduleRepo.GetAll().ToList();
            }
            catch (Exception)
            {
                throw new NullReferenceException("No Schedule Found");
            }

            return schedule;
        }

        public bool AddSchedule(Entities.Schedule schedule)
        {
            bool IsAdded = false;
            if (schedule == null)
                throw new NullReferenceException("Cannot insert Null value");
            Employee emp = new Employee();
            try
            {
                //AddressRepo.Detach(schedule.FromAddress);
                //AddressRepo.Detach(schedule.ToAddress);
                //EmployeeRepo.Detach(schedule.Offerer);
                ScheduleRepo.Create(schedule);
                IsAdded = true;
            }
            catch (Exception ex)
            {
                throw new ScheduleNotAddedException("Cannot Add Schedule"+ex);
            }
            return IsAdded;
        }

        public Entities.Schedule GetScheduleByID(int id)
        {
            if (id == 0)
                throw new NullReferenceException("No Schedule Found With id" + id);
            Schedule sch = new Schedule();
            try
            {
                sch = ScheduleRepo.GetAll().FirstOrDefault(x => x.ScheduleId == id);
                ScheduleRepo.Detach(sch);
            }
            catch (Exception ex)
            {
                throw new NoScheduleFoundException("No Schedule Found");
            }
            return sch;
        }
       
        public bool Update(Schedule schedule)
        {
            bool IsUpdated = false;

            //Schedule sch = GetScheduleByID(schedule.ScheduleId);
            if (schedule == null)
                throw new NoScheduleFoundException("Cannot find the Schedule");
            try
            {
                
                ScheduleRepo.Update(schedule);
                IsUpdated = true;
            }
            catch (Exception)
            {
                throw new ScheduleNotUpdatedException("schedule not updated");
            }
            return IsUpdated;
        }

        public Entities.Schedule GetScheduleByEmployeeID(int id)
        {
            if (id == 0)
                throw new NullReferenceException("No Schedule Found With id" + id);
            List<Schedule> schs = new List<Schedule>();
            Schedule sch = new Schedule();
            try
            {
                schs = ScheduleRepo.GetAll().ToList();
                foreach (var item in schs)
                {
                    if (item.EmployeeID == id)
                    {
                        sch = item;
                    }
                }
            }
            catch (Exception)
            {
                throw new NoScheduleFoundException("No Schedule Found");
            }
            return sch;
        }
    }
}

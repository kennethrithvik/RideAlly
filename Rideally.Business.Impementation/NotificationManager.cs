using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Rideally.Business.Contract;
using Rideally.Data.Repository;
using Rideally.Entities;
using Rideally.Exceptions;
namespace Rideally.Business.Impementation
{
    class NotificationManager:INotificationManager
    {
        IGenericRepository<Notification> NotificationRepo = null;
        public NotificationManager(IUnitOfWork uow)
        {
            NotificationRepo = uow.GetGenericRepository<Notification>(); 
        }



        public List<Entities.Notification> GetAllNotification()
        {
            //throw new NotImplementedException();
            List<Notification> notifications = new List<Notification>();
            try
            {
                notifications = NotificationRepo.GetAll().ToList();
            }
            catch (Exception)
            {
                throw new NullReferenceException("No Notifications Found");
            }

            return notifications;
        }
        public List<Entities.Notification> GetAllNotificationByEmployeeId(int id)
        {
            if (id == 0)
                throw new NullReferenceException("No Notification Found With id" + id);
            List<Notification> notes = new List<Notification>();
            List<Notification> notes1 = new List<Notification>();

            try
            {

                notes = NotificationRepo.GetAll().Where(X=>X.Status=="New").OrderBy(x=>x.MessageDate).ToList();

                foreach (var item in notes)
                {
                    if (item.ToEmployeeId == id)
                    {
                        notes1.Add(item);
                    }
                }

            }
            catch (Exception)
            {
                throw new NoNotificationFoundException("No Notification Found");
            }
            return notes1;
        }

        public Notification GetByNotificationId(int id)
        {
            if (id == 0)
                throw new NullReferenceException("No Notification Found With id" + id);
            Notification note = new Notification();
            try
            {
                note = NotificationRepo.GetAll().FirstOrDefault(x => x.NotificationId ==id);
            }
            catch (Exception)
            {
                throw new NoNotificationFoundException("No Notification Found");
            }
            return note;
        }
        public bool AddNotification(Entities.Notification notification)
        {
            bool IsAdded = false;
            if (notification == null)
                throw new NullReferenceException("Cannot insert Null value");
        
          



            Employee emp = new Employee();
            try
            {
                NotificationRepo.Create(notification);
                IsAdded = true;
            }
            catch (Exception)
            {
                throw new NotificationNotAddedException("Cannot Add Notification");
            }
            return IsAdded;
        }


        public List<Entities.Notification> GetAllNotificationByScheduleId(int id)
        {
            if (id == 0)
                throw new NullReferenceException("No Notification Found With id" + id);
            List<Notification> notes = new List<Notification>();
            List<Notification> notes1 = new List<Notification>();

            try
            {

                notes = NotificationRepo.GetAll().OrderBy(x => x.MessageDate).ToList();

                foreach (var item in notes)
                {
                    if (item.ScheduleId == id)
                    {
                        notes1.Add(item);
                    }
                }

            }
            catch (Exception)
            {
                throw new NoNotificationFoundException("No Notification Found");
            }
            return notes1;
        }

        public bool Update(Notification notification)
        {
            bool IsUpdated = false;

            //Schedule sch = GetScheduleByID(schedule.ScheduleId);
            if (notification == null)
                throw new NoNotificationFoundException("Cannot find the notification");
            try
            {

                NotificationRepo.Update(notification);
                IsUpdated = true;
            }
            catch (Exception)
            {
                throw new NotificationNotUpdatedException("notification not updated");
            }
            return IsUpdated;
        }
    }
}

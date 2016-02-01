using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Rideally.Entities;

namespace Rideally.Business.Contract
{
    public interface INotificationManager
    {
        List<Notification> GetAllNotification();
        List<Notification> GetAllNotificationByEmployeeId(int  id);
        bool AddNotification(Notification notification);
        Notification GetByNotificationId(int id);
        List<Entities.Notification> GetAllNotificationByScheduleId(int id);
        bool Update(Notification notification);
    }
}

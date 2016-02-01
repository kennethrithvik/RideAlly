using System;
using System.Collections.Generic;
using System.Linq;
using System.Net;
using System.Net.Http;
using System.Web.Http;
using Newtonsoft.Json;
using Rideally.Business.Contract;
using Rideally.Entities;
using Rideally.WebApi.MVC.Models;


namespace Rideally.WebApi.MVC.Controllers
{
    public class NotificationController : ApiController
    {
        INotificationManager manager = null;
        IScheduleManager smanag;
        IEmployeeManager emanag;
        IRiderMasterManager RiderManager;

        public NotificationController(INotificationManager manager, IScheduleManager sm, IEmployeeManager emanag, IRiderMasterManager RiderManager)
        {
            this.manager = manager;
           this.smanag = sm;
            this.emanag = emanag;
            this.RiderManager = RiderManager;

        }

        public List<Notification> Get()
        {
            return manager.GetAllNotification();
        }

        public List<Notification> Get(int id)
        {
            return manager.GetAllNotificationByEmployeeId(id);
        }
        public bool Post([FromBody]Notification note)
        {
            return manager.AddNotification(note);
        }

        [HttpPut]
        [Route("api/Notification/RiderAccept")]
        public bool RiderAccept(AcceptCorider corider)
        {
            RiderMaster master = new RiderMaster();
            master.ScheduleID = corider.sid;
            master.SeekerId = corider.coriderid;

            RiderManager.AddRiderMaster(master);
            Schedule s = smanag.GetScheduleByID(corider.sid);
            //RiderMaster rm = new RiderMaster();
            ////Employee e = ;
            //rm.Seeker = emanag.GetEmployeeByID(corider.coriderid);
            //s.Riderer = new List<RiderMaster>();
            //s.Riderer.Add(rm);
            s.SeatsAvailable--;
            smanag.Update(s);
            return true;

        }
        [HttpPost]
        [Route("api/Notification/Update")]
        public bool Update([FromBody]ScheduleModel scheduleid)
        {
            List<Notification> notifications = manager.GetAllNotificationByScheduleId(scheduleid.sid);
            foreach (var item in notifications)
            {
                item.Status = "Old";



                manager.Update(item);
            }

            return true;
            //  return manager.Update(notification);
        }

    }
}

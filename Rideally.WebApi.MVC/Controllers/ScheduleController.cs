using Rideally.Business.Contract;
using Rideally.Entities;
using Rideally.WebApi.MVC.Models;
using System;
using System.Collections.Generic;
using System.Globalization;
using System.Linq;
using System.Net;
using System.Net.Http;
using System.Web.Http;

namespace Rideally.WebApi.MVC.Controllers
{
    public class ScheduleController : ApiController
    {

        IScheduleManager scheduleManager;
        IEmployeeManager empMan;
        IAddressManager addMan;
        IVehicleManager vehman;
         public ScheduleController(IScheduleManager adr,IEmployeeManager em,IAddressManager am,IVehicleManager vm)
         {
            scheduleManager = adr;
            empMan = em;
             addMan= am;
             vehman = vm;
         }

         public List<Schedule> Get()
         {
            
             return scheduleManager.GetAllSchedule();
         }

         public Schedule Get(int id)
         {
             return scheduleManager.GetScheduleByID(id);
         }

         
         [Route("api/Schedule/GetSEByID")]
         [HttpPost]
         public VehicleOwnerDetails GetSEByID([FromBody]ScheduleModel obj)
         {
             VehicleOwnerDetails vod = new VehicleOwnerDetails();
             vod.schedule = scheduleManager.GetScheduleByID(obj.sid);
             vod.employee = empMan.GetEmployeeByID(vod.schedule.EmployeeID);
             vod.ToAddress = addMan.GetAddressByID(vod.schedule.ToAddressID);
             vod.FromAddress = addMan.GetAddressByID(vod.schedule.FromAddressID);
             return vod;
         }



         [Route("api/Schedule/Retrieve")]
         [HttpPost]
         public List<ScheduleDetails> Retrieve(RideSeeker obj)
         {
             //Address data = new JavaScriptSerializer().Deserialize<Address>(HomeAddress);

             List<ScheduleDetails> ScheduleList = new List<ScheduleDetails>();
             //DateTime SchDate = null;
             //SchDate=new DateTime(rider.ScheduledDate);

             foreach (Schedule item in scheduleManager.GetAllSchedule())
             {
                 DateTimeFormatInfo format = new DateTimeFormatInfo();
                 format.ShortDatePattern = "yyyy/MM/dd";
                 format.DateSeparator = "/";

                 DateTime shortDate = Convert.ToDateTime(item.ScheduledDate.Date, format);

                 if ((item.ScheduledTime.Equals(obj.ScheduledTime))
                    && (item.ScheduledStatus.Equals("Active")) &&
                   (shortDate.Date.CompareTo(obj.ScheduledDate.Date) == 0)&&(item.SeatsAvailable > 0))
                 {


                     Employee emp = empMan.GetEmployeeByID(item.EmployeeID);
                     Address fromAdd = addMan.GetAddressByID(item.FromAddressID);
                     Address toAdd = addMan.GetAddressByID(item.ToAddressID);
                     ScheduleDetails sd = new ScheduleDetails();
                     sd.Name = emp.EmployeeName;
                     sd.EmailID = emp.EmailID;
                     sd.PhoneNo = emp.MobileNo;
                     sd.ScheduleID = item.ScheduleId;
                     
                     sd.OffererOfficeAddressLatitude = emp.OfficeAddress.Latitude;
                     sd.OffererOfficeAddressLongitude = emp.OfficeAddress.Longitude;
                     sd.FromAddressLatitude = fromAdd.Latitude;
                     sd.FromAddressLongitude = fromAdd.Longitude;
                     sd.ToAddressLatitude = toAdd.Latitude;
                     sd.ToAddressLongitude = toAdd.Longitude;
                 

                     if (obj.Direction.Equals("From Home"))
                     {
                         if(obj.OfficeAddressLatitude==sd.ToAddressLatitude && obj.OfficeAddressLongitude== sd.ToAddressLongitude)
                         {
                             ScheduleList.Add(sd);
                         }
                    
                     
                    
                     }
                     else if (obj.Direction.Equals("To Home"))
                     {
                         if (obj.OfficeAddressLatitude == sd.FromAddressLatitude && obj.OfficeAddressLongitude == sd.FromAddressLongitude)
                         {
                             ScheduleList.Add(sd);
                         }
                     }
                 }


             }
             return ScheduleList;
         }

         [Route("api/Schedule/InsertSchedule")]
         public void InsertSchedule(ScheduleViewModule Sch)
         {
             Schedule schedule = new Schedule();
             Employee emp = new Employee();
             emp =empMan.GetEmployeeByID(Sch.EmployeeId);
             
             schedule.ScheduledDate = Sch.date;
             schedule.ScheduledTime = Sch.starttime;
             schedule.ScheduledStatus = Sch.ScheduleStatus;
             schedule.EmployeeID = Sch.EmployeeId;
             Vehicle veh= vehman.GetVehicleByID(emp.Vehicle.VehicleId);
             schedule.SeatsAvailable = veh.VehicleType.NoOfSeats - 1 ;
             if(Sch.Direction.CompareTo("From_Home")==0)
             {
                 schedule.ToAddressID = emp.OfficeAddress.AddressId;
                 schedule.FromAddressID = emp.HomeAddress.AddressId;
             }
             else
             {
                 schedule.ToAddressID = emp.HomeAddress.AddressId;
                 schedule.FromAddressID = emp.OfficeAddress.AddressId;
             }

             scheduleManager.AddSchedule(schedule);
         }

         public bool Post([FromBody]Schedule sch)
         {

             return scheduleManager.Update(sch);
         }

       

    }
}

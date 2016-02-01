using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using Rideally.Entities;

namespace Rideally.WebApi.MVC.Models
{
    public class VehicleDetails
    {
        public string Color { get; set; }
        public string VehicleNumber { get; set; }
        //public string VehicleMasterType { get; set; }
        //public string BrandName { get; set; }
        //public string ModelName { get; set; }
        //public string VehicleTypeDesc { get; set; }
        //public string TripType{get; set;}
        public int VehicleID { get; set; }
        public DateTime date { get; set; }
        public string starttime { get; set; }
        public int FromAddressID { get; set; }
        public int ToAddressID { get; set; }
        public int RemainingSeats { get; set; }
        public Vehicle Vehicle { get; set; }
        public int EmployeeID { get; set; }
        public Employee Employee { get; set; }
        public string ScheduleStatus { get; set; }
    }
}
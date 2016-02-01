using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;

namespace Rideally.WebApi.MVC.Models
{
    public class RideSeeker
    {
        public String ScheduledTime { get; set; }

        public DateTime ScheduledDate { get; set; }
        public String HomeAddressLatitude { get; set; }
        public String HomeAddressLongitude { get; set; }
        public String Direction { get; set; }
        //public int OfficeAddressID { get; set; }
       public String OfficeAddressLatitude { get; set; }
       public String OfficeAddressLongitude { get; set; }
                        
    }
}
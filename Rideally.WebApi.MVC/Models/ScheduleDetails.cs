using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;

namespace Rideally.WebApi.MVC.Models
{
    public class ScheduleDetails
    {
        public int ScheduleID { get; set; }

        public string OffererOfficeAddressLatitude { get; set; }

        public string OffererOfficeAddressLongitude { get; set; }
        public string FromAddressLatitude { get; set; }
        public string FromAddressLongitude { get; set; }

        public string ToAddressLatitude { get; set; }

        public string ToAddressLongitude { get; set; }

        public string Name { get; set; }

        public string EmailID { get; set; }

        public string PhoneNo { get; set; }
    }
}
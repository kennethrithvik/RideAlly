using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;
using System.Linq;
using System.Web;

namespace Rideally.WebApi.MVC.Models
{
    public class RegEmployee
    {
        public string EmployeeName { get; set; }
        public string Gender { get; set; }
        public string MobileNo { get; set; }
        public string EmailID { get; set; }

        public string HomeLatitude { get; set; }
        public string HomeLongitude { get; set; }
        public string OfficeLatitude { get; set; }
        public string OfficeLongitude { get; set; }
    }
}
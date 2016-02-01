using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;

namespace Rideally.WebApi.MVC.Models
{
    public class ScheduleViewModule
    {
        public int EmployeeId { get; set; }

        public string Direction { get; set; }

        public string ScheduleStatus { get; set; }

        public string starttime { get; set; }

        public DateTime date { get; set; }
    }
}
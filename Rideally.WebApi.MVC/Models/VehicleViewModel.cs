using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;

namespace Rideally.WebApi.MVC.Models
{
    public class VehicleViewModel
    {
        public string vehicleNo { get; set; }
        public string color { get; set; }
        public string modelName { get; set; }

        public int EmployeeId { get; set; }


        public string VehicleDesc { get; set; }


        public string VehicleType { get; set; }

        public string BrandName { get; set; }


    }
}
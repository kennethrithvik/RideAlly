using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;

namespace Rideally.WebApi.MVC.Models
{
    public class VehicleOwnerDetails
    {
        public Rideally.Entities.Schedule schedule { get; set; }
        public Rideally.Entities.Employee employee { get; set; }

        public Rideally.Entities.Address FromAddress { get; set; }

        public Rideally.Entities.Address ToAddress { get; set; }
    }
}
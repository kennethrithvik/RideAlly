using Newtonsoft.Json;
using Rideally.Business.Contract;
using Rideally.Entities;
using Rideally.WebApi.MVC.Models;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Net;
using System.Net.Http;
using System.Text;
using System.Web.Http;
using Rideally.Business;


namespace Rideally.WebApi.MVC.Controllers
{
    public class VehicleController : ApiController
    {
        IVehicleManager manager = null;
        //public VehicleController() { }
        public VehicleController(IVehicleManager manager)
        {
            this.manager = manager;
        }
   
        public List<Vehicle> Get()
        {

            return manager.GetAllVehicle();
        }
        public Vehicle Get(int id)
        {
            return manager.GetVehicleByID(id);
            //return "value";
        }

        // POST: api/Vehicle
        //public void Post(VehicleDetailsEntity veh)
        //{
        //    //Employee e = new Employee();
        //    //Vehicle v = new Vehicle();
        //    //EmployeeVehicle ev = new EmployeeVehicle();

        //}
    }
}

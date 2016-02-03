using Rideally.Entities;
using Rideally.WebApi.MVC.Models;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Net;
using System.Net.Http;
using System.Web.Http;
//System.Web.Security.Membership.GeneratePassword(length, numberOfNonAlphanumericCharacters);
//var a = Rideally.Util.SaltHash.VerifyHash("password", null, auth.Password);
namespace Rideally.WebApi.MVC.Controllers
{
    
    public class EmployeeController : ApiController
    {
        
        private Rideally.Business.Contract.IEmployeeManager EmpMang;
        private Rideally.Business.Contract.IVehicleManager VehicleManager;
        public EmployeeController (Rideally.Business.Contract.IEmployeeManager EmpMang1,Rideally.Business.Contract.IVehicleManager vm)
	    {
            EmpMang = EmpMang1;
            VehicleManager = vm;
	    }
        // GET: api/Employee
        public IEnumerable<Rideally.Entities.Employee> Get()
        {
            IList<Rideally.Entities.Employee> EmpList = EmpMang.GetAllEmployees();
            return EmpList;
        }

        // GET: api/Employee/5
        public Rideally.Entities.Employee Get(int id)
        {
            Rideally.Entities.Employee Emp = EmpMang.GetEmployeeByID(id);
            return Emp;
        }

        // POST: api/Employee
        public string Post([FromBody]RegEmployee RegEmp)
        {
            foreach (var item in EmpMang.GetAllEmployees())
            {
                if (item.EmailID.Equals(RegEmp.EmailID))
                    return "Username already registered";
            }

            Rideally.Entities.Employee emp = new Rideally.Entities.Employee();
            Rideally.Entities.Address home = new Entities.Address();
            Rideally.Entities.Address office = new Entities.Address();
            home.Latitude = RegEmp.HomeLatitude;      home.Longitude = RegEmp.HomeLongitude;       home.type = true;
            office.Latitude = RegEmp.OfficeLatitude;  office.Longitude = RegEmp.OfficeLongitude;   office.type = false;
            emp.HomeAddress = home; emp.OfficeAddress = office;

            Rideally.Entities.Authentication auth = new Entities.Authentication();
            auth.Login = RegEmp.EmailID;
            string password = "password";// System.Web.Security.Membership.GeneratePassword(8, 0);
            auth.Password = Rideally.Util.SaltHash.ComputeHash(password, null, null);
            Rideally.Util.Email.SendMail(RegEmp.EmailID, "RideAlly User Password", "Your password for RideAlly = " + password);
            
            emp.UserAuthentication = auth;
            emp.EmployeeName = RegEmp.EmployeeName;

            emp.Gender = RegEmp.Gender;
            emp.MobileNo = RegEmp.MobileNo;
            emp.EmailID = RegEmp.EmailID;
            return EmpMang.AddEmployee(emp).ToString();
        }

        // POST: api/Employee/5
        [HttpPost]
        [Route("api/UpdateEmployee")]
        public void PostEmployeeUpdate([FromBody]Rideally.Entities.Employee Emp)
        {
            Employee emp = EmpMang.GetEmployeeByID(Emp.EmployeeID);
            emp.EmployeeName = Emp.EmployeeName;
            emp.EmailID = Emp.EmailID;
            emp.MobileNo = Emp.MobileNo;
            EmpMang.UpdateEmployee(emp);
        }

        // DELETE: api/Employee/5
        public void Delete(int id)
        {            
            EmpMang.DeleteEmployee(EmpMang.GetEmployeeByID(id));
        }
        
        [Route("api/Employee/UpdateVehicle/{Color}/{EmployeeID}/{VehicleID}/{VehicleNumber}")]
        public void UpdateVehicle(string Color, int EmployeeID, int VehicleID, string VehicleNumber )
        {
            Employee emp = null;
           
            //emp = EmpMang.GetEmployeeByID(veh.EmployeeID);
           
            //emp.Vehicle = VehicleManager.GetVehicleByID(veh.VehicleID);

         
            //EmployeeVehicle empveh = new EmployeeVehicle();
            //empveh.Color = veh.Color;
            //empveh.VehicleNumber = veh.VehicleNumber;
            ////emp.Vehicle.VehicleId = veh.VehicleID;
            //emp.EmployeeVehicle.Add(empveh);

            //EmpMang.UpdateEmployee(emp);
          
            


        }

        [HttpPost]
        [Route("api/Employee/Login")]
        public Employee login(Login LogEmp)
        {
            Employee e;
            foreach (var item in EmpMang.GetAllEmployees())
            {
                if (LogEmp.EmailID.Equals(item.EmailID))
                {
                    e = item;
                    if (Rideally.Util.SaltHash.VerifyHash(LogEmp.Password, null, e.UserAuthentication.Password))
                    {
                        return e;
                    }
                }
            }
            return null;
        }

        [HttpPost]
        [Route("api/Employee/AddVehicleDetail")]
        public bool AddVehicleDetail([FromBody]VehicleViewModel vehicle)
        { 
            EmployeeVehicle empVeh = new EmployeeVehicle();
            empVeh.Color = vehicle.color;
            empVeh.VehicleNumber = vehicle.vehicleNo;

            VehicleTypeMaster vtm = new VehicleTypeMaster();
            vtm.VehicleMasterType = vehicle.VehicleType;


            int nos = 0;
            if(vehicle.VehicleType.CompareTo("Two Wheeler")==0)
            {
                nos = 2;
            }
            else
            {
                nos = 4;
            }

            
           

            Brand brn = new Brand();
            brn.BrandName = vehicle.BrandName;
            brn.VType = vtm;


            VehicleType vt = new VehicleType();
            vt.VehicleTypeDesc = vehicle.VehicleDesc;
            vt.NoOfSeats = nos;
            vt.VehicleTypeMaster = vtm;

            Vehicle v = new Vehicle();
            v.Brand = brn;
            v.ModelName = vehicle.modelName;
            v.VehicleType = vt;
            
            

            //Vehicle veh = VehicleManager.GetVehicleByModelName(vehicle.modelName);
            Employee Emp = EmpMang.GetEmployeeByID(vehicle.EmployeeId);
            Emp.Vehicle = v;
            //EmpMang.UpdateEmployee(Emp);
            //Emp.Vehicle = veh;
            Emp.EmployeeVehicle.Add(empVeh);
            return EmpMang.UpdateEmployee(Emp);
      
        }
    }
}

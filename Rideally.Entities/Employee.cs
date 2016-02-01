using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;

namespace Rideally.Entities
{
    public class Employee
    {
        public int EmployeeID { get; set; }
        public string EmployeeName { get; set; }
        public string Gender { get; set; }
        public string MobileNo { get; set; }
        public string EmailID { get; set; }
        public virtual Authentication UserAuthentication { get; set; }
        public virtual Address HomeAddress { get; set; }
        public virtual Address OfficeAddress { get; set; }
        public virtual Vehicle Vehicle { get; set; }
        public virtual List<EmployeeVehicle> EmployeeVehicle { get; set; }
    }
}

using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Rideally.Entities
{
    public class EmployeeVehicle
    {
        [Key]
        [DatabaseGenerated(System.ComponentModel.DataAnnotations.Schema.DatabaseGeneratedOption.Identity)]
        public int EmployeeVehicleId { get; set; }
        public string Color { get; set; }
        public string VehicleNumber { get; set; }
    }
}

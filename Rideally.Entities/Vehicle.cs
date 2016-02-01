using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;
using System.Linq;
using System.Text;

namespace Rideally.Entities
{
    public class Vehicle
    {
        [Key]
        [DatabaseGenerated(System.ComponentModel.DataAnnotations.Schema.DatabaseGeneratedOption.Identity)]
        public int VehicleId { get; set; }
        public virtual VehicleType VehicleType { get; set; }
        public virtual Brand Brand { get; set; }
        public string ModelName { get; set; }
    }
}

using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;
using System.Linq;
using System.Text;

namespace Rideally.Entities
{
    public class VehicleType
    {
        [Key]
        [DatabaseGenerated(System.ComponentModel.DataAnnotations.Schema.DatabaseGeneratedOption.Identity)]
        public int VehicleTypeID { get; set; }
        public string VehicleTypeDesc { get; set; }
        public int NoOfSeats { get; set; }
        public virtual VehicleTypeMaster VehicleTypeMaster { get; set; }
    }
}

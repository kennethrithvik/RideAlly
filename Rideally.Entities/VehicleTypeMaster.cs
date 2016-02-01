using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;
using System.Linq;
using System.Text;

namespace Rideally.Entities
{
    public class VehicleTypeMaster
    {
        [Key]
        [DatabaseGenerated(System.ComponentModel.DataAnnotations.Schema.DatabaseGeneratedOption.Identity)]
        public int VehicleTypeMasterID { get; set; }
        public string VehicleMasterType { get; set; }
    }
}

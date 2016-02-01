using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Rideally.Entities
{
    public class Schedule
    {
        [Key]
        [DatabaseGenerated(System.ComponentModel.DataAnnotations.Schema.DatabaseGeneratedOption.Identity)]
        public int ScheduleId { get; set; }
        public int EmployeeID { get; set; }
        public int FromAddressID { get; set; }
        public int ToAddressID { get; set; }

        [ForeignKey("EmployeeID")]
        public Employee Offerer { get; set; }

        [ForeignKey("FromAddressID")]
        public Address FromAddress { get; set; }

        [ForeignKey("ToAddressID")]
        public Address ToAddress { get; set; }

        public DateTime ScheduledDate { get; set; }
        public string ScheduledTime { get; set; }
        public string ScheduledStatus { get; set; }
        public int SeatsAvailable { get; set; }
        
    }
}

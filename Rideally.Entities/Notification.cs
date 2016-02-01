using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Rideally.Entities
{
   public class Notification
    {
        [Key]
         [DatabaseGenerated(System.ComponentModel.DataAnnotations.Schema.DatabaseGeneratedOption.Identity)]
        public int NotificationId { get; set; }
        public string Message { get; set; }
        public string Status { get; set; }
        public DateTime MessageDate { get; set; }
        public string MessageTime { get; set; }
        public int FromEmployeeId { get; set; }
        public int ToEmployeeId { get; set; }
        public int ScheduleId { get; set; }
        public RequestType RequestType { get; set; }
    }
}

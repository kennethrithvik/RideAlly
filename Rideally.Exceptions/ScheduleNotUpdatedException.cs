using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Rideally.Exceptions
{
   public class ScheduleNotUpdatedException:ApplicationException
    {
         public ScheduleNotUpdatedException()
        {

        }
        public ScheduleNotUpdatedException(string message)
            : base(message)
        {

        }
        public ScheduleNotUpdatedException(string message, Exception e)
            : base(message, e)
        {


        }
    }
}

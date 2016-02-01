using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace Rideally.Exceptions
{
  public  class ScheduleNotAddedException : ApplicationException
    {
        public ScheduleNotAddedException()
        {

        }
        public ScheduleNotAddedException(string message)
            : base(message)
        {

        }
        public ScheduleNotAddedException(string message, Exception e)
            : base(message, e)
        {

        }
    }
}

using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Rideally.Exceptions
{
   public class NoScheduleFoundException:ApplicationException
    {
        public NoScheduleFoundException()
        {

        }
        public NoScheduleFoundException(string message)
            : base(message)
        {

        }
        public NoScheduleFoundException(string message, Exception e)
            : base(message, e)
        {
                
        }
    }
}

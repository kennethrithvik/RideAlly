using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Rideally.Exceptions
{
   public class NoNotificationFoundException:ApplicationException
    {
        public NoNotificationFoundException()
        {
                
        }
        public NoNotificationFoundException(string message)
            : base(message)
        {
                
        }
        public NoNotificationFoundException(string message, Exception e)
            : base(message, e)
        {
                
        }
    }
}

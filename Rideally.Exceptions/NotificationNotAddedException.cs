using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Rideally.Exceptions
{
   public class NotificationNotAddedException:ApplicationException
    {
        public NotificationNotAddedException()
        {
                
        }
        public NotificationNotAddedException(string message)
            : base(message)
        {
                
        }
        public NotificationNotAddedException(string message, Exception e)
            : base(message, e)
        {
                
        }
    }
}

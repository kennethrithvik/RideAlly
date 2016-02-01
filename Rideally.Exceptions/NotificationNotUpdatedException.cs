using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Rideally.Exceptions
{
   public class NotificationNotUpdatedException:ApplicationException
    {
        public NotificationNotUpdatedException()
        {
                
        }
        public NotificationNotUpdatedException(string message)
            : base(message)
        {
                
        }
        public NotificationNotUpdatedException(string message, Exception e)
            : base(message, e)
        {

        }

    }
}

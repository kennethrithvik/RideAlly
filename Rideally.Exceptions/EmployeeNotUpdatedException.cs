using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Rideally.Exceptions
{
   public class EmployeeNotUpdatedException:ApplicationException
    {
        public EmployeeNotUpdatedException()
        {
                
        }
        public EmployeeNotUpdatedException(string message)
            : base(message)
        {

        }
        public EmployeeNotUpdatedException(string message, Exception e)
            : base(message, e)
        {
                
        }
    }
}

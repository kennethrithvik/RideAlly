using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Rideally.Exceptions
{
    public class NoEmployeeFoundException:ApplicationException
    {
        public NoEmployeeFoundException()
        {
                
        }
        public NoEmployeeFoundException(string message):base(message)
        {

        }
        public NoEmployeeFoundException(string message,Exception e):base(message,e)
        {

        }
    }
}

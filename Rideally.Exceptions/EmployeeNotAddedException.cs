using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Rideally.Exceptions
{
    public class EmployeeNotAddedException :ApplicationException
    {
        public EmployeeNotAddedException()
        {

        }
        public EmployeeNotAddedException(string message):base(message)
        {

        }
        public EmployeeNotAddedException(string message,Exception e):base(message,e)
        {

        }
    }
}

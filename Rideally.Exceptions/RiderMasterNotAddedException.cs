using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Rideally.Exceptions
{
    public class RiderMasterNotAddedException : Exception
    {
        public RiderMasterNotAddedException()
        {

        }

        public RiderMasterNotAddedException(string message) : base(message)
        {

        }

        public RiderMasterNotAddedException(string message, Exception e) : base(message,e)
        {

        }


    }
}

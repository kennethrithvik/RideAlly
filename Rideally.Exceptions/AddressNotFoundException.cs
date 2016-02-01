using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Rideally.Exceptions
{
    public class AddressNotFoundException:ApplicationException
    {
        public AddressNotFoundException()
        {
                
        }
        public AddressNotFoundException(string message):base(message)
        {

        }
        public AddressNotFoundException(string message,Exception e):base(message,e)
        {

        }
    }
}

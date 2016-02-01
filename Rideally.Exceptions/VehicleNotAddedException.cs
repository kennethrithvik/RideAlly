﻿using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Rideally.Exceptions
{
    public class VehicleNotAddedException : Exception
    {
        public VehicleNotAddedException()
        {

        }
        public VehicleNotAddedException(string message) : base(message)
        {

        }
        public VehicleNotAddedException(string message, Exception e) : base(message, e)
        {

        }
    }
}

using Rideally.Entities;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Rideally.Data.Repository
{
    public interface IUserAuthentication
    {
        bool IsValidUser(Rideally.Entities.Employee employee);

    }
}

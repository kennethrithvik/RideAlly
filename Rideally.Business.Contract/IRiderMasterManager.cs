using Rideally.Entities;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Rideally.Business.Contract
{
    public interface IRiderMasterManager
    {
        List<RiderMaster> GetAllRiderMaster();
        bool AddRiderMaster(RiderMaster RiderMaster);
        RiderMaster GetRiderMasterByID(int id);
    }
}

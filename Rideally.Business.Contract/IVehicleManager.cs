using Rideally.Entities;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Rideally.Business.Contract
{
    public interface IVehicleManager
    {
        List<Vehicle> GetAllVehicle();
        bool AddVehicle(Vehicle vehicle);
        Vehicle GetVehicleByID(int id);
        List<Vehicle> GetAllVehicleByBrandName(string brandName);
        List<Vehicle> GetAllByVehicleType(int vehicleTypeId);

        Vehicle GetVehicleByModelName(string ModelName);
    }
}

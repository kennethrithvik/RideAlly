using Rideally.Business.Contract;
using Rideally.Data.Repository;
using Rideally.Entities;
using Rideally.Exceptions;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Rideally.Business.Impementation
{
    public class VehicleManager : IVehicleManager
    {
        IGenericRepository<Vehicle> VehicleRepo = null;

        public VehicleManager(IUnitOfWork uow)
        {
            VehicleRepo = uow.GetGenericRepository<Vehicle>();
        }

        public List<Entities.Vehicle> GetAllVehicle()
        {
            List<Vehicle> Vehicles = new List<Vehicle>();
            try
            {
                Vehicles = VehicleRepo.GetAll().ToList();
            }
            catch (Exception)
            {
                throw new NullReferenceException("No Vehicle found");
            }
            return Vehicles;
        }

        public bool AddVehicle(Entities.Vehicle vehicle)
        {
            bool isAdded = false;
            if (vehicle == null)
            {
                throw new NullReferenceException("Cannot insert Null value");
            }
            try
            {
                VehicleRepo.Create(vehicle);
                isAdded = true;
            }
            catch (Exception)
            {
                throw new VehicleNotAddedException("Cannot add Vehicle");
            }
            return isAdded;
        }

        public Entities.Vehicle GetVehicleByID(int id)
        {
            try
            {
                return GetAllVehicle().FirstOrDefault(p => p.VehicleId == id);
            }
            catch (Exception)
            {
                throw new NullReferenceException("No such vehicle found");
            }
        }

        public List<Entities.Vehicle> GetAllVehicleByBrandName(string brandName)
        {
            try
            {
                return GetAllVehicle().FindAll(p => p.Brand.BrandName.Equals(brandName));
            }
            catch (Exception)
            {
                throw new NullReferenceException("No vehicle in such brand exists");
            }
        }

        public List<Entities.Vehicle> GetAllByVehicleType(int vehicleTypeId)
        {
            try
            {
                return GetAllVehicle().FindAll(p => p.VehicleType.VehicleTypeID == vehicleTypeId);
            }
            catch (Exception)
            {
                throw new NullReferenceException("No vehicle in such brand exists");
            }
        }
        public int GetSeatsByVehicleType(string VehTypeDescp)
        {
            try
            {
                return (GetAllVehicle().Where((v)=>v.VehicleType.VehicleTypeDesc==VehTypeDescp).Select((v)=>v.VehicleType.NoOfSeats)).SingleOrDefault();
            }
            catch (Exception)
            {
                throw new NullReferenceException("No vehicle in such brand exists");
            }
        }

        public Vehicle GetVehicleByModelName(string ModelName)
        {
            try
            {
                return GetAllVehicle().FirstOrDefault(p => p.ModelName.CompareTo(ModelName)==0);
            }
            catch (Exception)
            {
                throw new NullReferenceException("No such vehicle found");
            }
        }

    }
}

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
    public class RiderMasterManager : IRiderMasterManager
    {
        IGenericRepository<RiderMaster> RiderMasterRepo = null;
        public RiderMasterManager(IUnitOfWork uow)
        {
            RiderMasterRepo = uow.GetGenericRepository<RiderMaster>();
        }
        public List<Entities.RiderMaster> GetAllRiderMaster()
        {
            List<RiderMaster> RiderMasters = new List<RiderMaster>();
            try
            {
                RiderMasters = RiderMasterRepo.GetAll().ToList();
            }
            catch (Exception)
            {
                throw new NullReferenceException("No rider found");
            }
            return RiderMasters;
        }

        public bool AddRiderMaster(Entities.RiderMaster RiderMaster)
        {
            bool IsAdded = false;
            if (RiderMaster == null)
                throw new NullReferenceException("Cannot insert Null value");
            Employee emp = new Employee();
            try
            {
                RiderMasterRepo.Create(RiderMaster);
                IsAdded = true;
            }
            catch (Exception)
            {
                throw new RiderMasterNotAddedException("Cannot Add RiderMaster");
            }
            return IsAdded;
        }

        public Entities.RiderMaster GetRiderMasterByID(int id)
        {
            if (id == 0)
                throw new NullReferenceException("No Rider Found With id" + id);
            RiderMaster riderMaster = new RiderMaster();
            try
            {
                riderMaster = RiderMasterRepo.GetAll().FirstOrDefault(x => x.RiderMasterID == id);
            }
            catch (Exception)
            {
                throw new NoEmployeeFoundException("No Employee Found");
            }
            return riderMaster
                
                
                ;
        }
    }
}

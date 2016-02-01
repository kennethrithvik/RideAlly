using Rideally.Business.Contract;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Rideally.Data.Repository;
using Rideally.Entities;
using Rideally.Exceptions;

namespace Rideally.Business.Impementation
{
    public class AddressManager : IAddressManager
    {
        IGenericRepository<Address> AddressRepo = null;
        public AddressManager(IUnitOfWork uow)
        {
            AddressRepo = uow.GetGenericRepository<Address>();
        }
        public List<Entities.Address> GetAllAdress()
        {
            List<Address> Address = new List<Address>();
            try
            {
                Address = AddressRepo.GetAll().ToList();
            }
            catch(Exception)
            {
                throw new NullReferenceException("No Address Found");
            }

            return Address;
        }

        public bool AddAdress(Entities.Address address)
        {
            bool IsAdded = false;
            if (address == null)
                throw new NullReferenceException("Cannot insert null value");
            try
            {
                AddressRepo.Create(address);
                IsAdded = true;
            }
            catch(Exception)
            {
                throw new AddressNotAddedException("Cannot Add Address");
            }
            return IsAdded;

        }

        public Entities.Address GetAddressByID(int id)
        {
            if (id == 0)
                throw new NullReferenceException("Id cannot be Empty");
            Address Address = new Address();

            try
            {
                Address = AddressRepo.GetAll().FirstOrDefault(x => x.AddressId == id);
            }
            catch(Exception)
            {
                throw new AddressNotFoundException("No Address Found with this ID" + id);
            }
            return Address;
        }
    }
}

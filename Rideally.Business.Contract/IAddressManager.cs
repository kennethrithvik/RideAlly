using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Rideally.Entities;
namespace Rideally.Business.Contract
{
    public interface IAddressManager
    {
        List<Address> GetAllAdress();
        bool AddAdress(Address address);
        Address GetAddressByID(int id);
    }
}

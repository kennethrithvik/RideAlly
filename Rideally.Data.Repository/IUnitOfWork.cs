using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Rideally.Data.Repository
{
    public interface IUnitOfWork
    {
        IGenericRepository<T> GetGenericRepository<T>() where T : class;
    }
}

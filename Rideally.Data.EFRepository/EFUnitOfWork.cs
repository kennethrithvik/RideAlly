using Rideally.Data.Repository;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Rideally.Data.EFRepository
{
    internal class EFUnitOfWork : IUnitOfWork
    {
        public IGenericRepository<T> GetGenericRepository<T>() where T : class
        {
            return new EFGenericRepository<T>();
        }
    }
}

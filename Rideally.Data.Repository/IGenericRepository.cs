using System;
using System.Collections.Generic;
using System.Linq;
using System.Linq.Expressions;
using System.Text;
using System.Threading.Tasks;

namespace Rideally.Data.Repository
{
    public interface IGenericRepository<T> where T : class
    {
        Task<IEnumerable<T>> GetAllAsync();
        IEnumerable<T> GetAll();


        IEnumerable<T> GetAll(string includeProperties = "");
        Task<IEnumerable<T>> GetAllAsync(string includeProperties = "");

        Task<IEnumerable<T>> GetAsync(Expression<Func<T, bool>> predicate);
        IEnumerable<T> Get(Expression<Func<T, bool>> predicate);

        IEnumerable<T> Get(Expression<Func<T, bool>> predicate, string includeProperties = "");
        Task<IEnumerable<T>> GetAsync(Expression<Func<T, bool>> predicate, string includeProperties = "");

        T Find(params object[] keys);
        Task<T> FindAsync(params object[] keys);

        Task<T> FindAsync(Expression<Func<T, bool>> predicate);
        T Find(Expression<Func<T, bool>> predicate);

        Task<T> FindAsync(Expression<Func<T, bool>> predicate, string includeProperties = "");
        T Find(Expression<Func<T, bool>> predicate, string includeProperties = "");

        Task<T> FindAsync(Expression<Func<T, bool>> predicate, params string[] includeProperties);
        T Find(Expression<Func<T, bool>> predicate, params string[] includeProperties);

        bool Any(Expression<Func<T, bool>> predicate);
        Task<bool> AnyAsync(Expression<Func<T, bool>> predicate);

        T Create(T t);
        IEnumerable<T> CreateRange(IEnumerable<T> t);

        void Update(T t);

        void Delete(object id);

        void Delete(T t);

        Task<int> SaveAsync();
        int Save();

        Task<int> CountAsync();
        int Count();

        void Attach(T t);

        void Detach(T t);
    }
}

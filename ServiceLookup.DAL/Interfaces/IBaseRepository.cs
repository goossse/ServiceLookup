using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace ServiceLookup.DAL.Interfaces
{
    public interface IBaseRepository<T> where T : class
    {
        void Create(T entity);
        Task<T> FindById(int id);
        Task<IEnumerable<T>> Get();
        Task Remove(int id);
        void Update(T item);

    }
}

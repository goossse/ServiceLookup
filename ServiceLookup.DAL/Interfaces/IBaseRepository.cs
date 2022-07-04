using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace ServiceLookup.DAL.Interfaces
{
    internal interface IBaseRepository<T> where T : class
    {
        bool Create(T entity);
        T GetT(int id);
        IEnumerable<T> Select();
        bool Delete(T entity);

    }
}

using ServiceLookup.DAL.Entity;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace ServiceLookup.DAL.Interfaces
{
    public interface IServiceRepository : IBaseRepository<Service>
    {
        Task<Service> GetByTitleAsync(string name);
    }
}

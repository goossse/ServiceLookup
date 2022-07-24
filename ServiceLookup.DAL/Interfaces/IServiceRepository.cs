using ServiceLookup.DAL.Entity;
using ServiceLookup.DAL.Entity.PagedList;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace ServiceLookup.DAL.Interfaces
{
    public interface IServiceRepository : IBaseRepository<Service>
    {
        public Task<IEnumerable<Service>> GetByUser(int userId);
        public Task<PagedList<Service>> FindByProperties(string searchText, int? typeId = null, string sortOrder = "Самые новые",
            bool IsRatedOnly = false, int? rateStart = 0, int? rateEnd = 10, int page = 1, int pageSize = 12, int? userId = null);
        public Task<IEnumerable<Service>> GetByType(int typeId);

    }
}

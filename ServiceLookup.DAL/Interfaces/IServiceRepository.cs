using ServiceLookup.DAL.Entity;
using ServiceLookup.DAL.Entity.PagedList;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Linq.Expressions;
using System.Text;
using System.Threading.Tasks;

namespace ServiceLookup.DAL.Interfaces
{
    public interface IServiceRepository : IBaseRepository<Service>
    {
        public Task<IEnumerable<Service>> GetIncluding(params Expression<Func<Service, object>>[] Properties);
        public Task<IEnumerable<Service>> GetIncludingFiltred(Expression<Func<Service, bool>> Filter,
            params Expression<Func<Service, object>>[] Properties);
        public Task<PagedList<Service>> GetIncludingFiltred(List<Expression<Func<Service, bool>>> Filters, Expression<Func<Service, object>> Order, bool IsDesc,
             int page = 1, int pageSize = 15, params Expression<Func<Service, object>>[] Properties);

    }
}

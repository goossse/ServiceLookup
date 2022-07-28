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
    public interface IRequestRepository : IBaseRepository<Request>
    {
        public Task<IEnumerable<Request>> GetIncluding(params Expression<Func<Request, object>>[] Properties);
        public Task<PagedList<Request>> GetIncludingFiltred(Expression<Func<Request, bool>> Filter, int page = 1,
            int pageSize = 6, params Expression<Func<Request, object>>[] Properties);
        public Task<Request> GetById(int id);

    }
}

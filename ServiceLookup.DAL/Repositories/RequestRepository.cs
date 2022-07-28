using Microsoft.EntityFrameworkCore;
using ServiceLookup.DAL.Entity;
using ServiceLookup.DAL.Entity.PagedList;
using ServiceLookup.DAL.Interfaces;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Linq.Expressions;
using System.Text;
using System.Threading.Tasks;

namespace ServiceLookup.DAL.Repositories
{
    public class RequestRepository : IRequestRepository
    {
        private readonly ApplicationDbContext db;

        public RequestRepository()
        {
        }

        public RequestRepository(ApplicationDbContext _db)
        {
            db = _db;
        }

        public void Create(Request item)
        {
            db.Requests.Add(item);
            db.SaveChanges();
        }

        public async Task<Request> GetById(int id)
        {
            return await db.Requests.AsNoTracking().FirstAsync(r => r.Id == id);
        }

        public async Task<Request> FindById(int id)
        {
            return await db.Requests.AsNoTracking().Include(r => r.Service)
                .Include(r => r.Price).AsNoTracking().FirstAsync(r => r.Id == id);
        }

        public IQueryable<Request> Include(params Expression<Func<Request, object>>[] Properties)
        {
            IQueryable<Request> query = db.Requests.AsNoTracking();
            return Properties
                .Aggregate(query, (current, includeProperty) => current.Include(includeProperty));
        }

        //For includes
        public async Task<IEnumerable<Request>> GetIncluding(params Expression<Func<Request, object>>[] Properties)
        {
            return await Include(Properties).ToListAsync();
        }

        //For PagedList with filter and many includes
        public async Task<PagedList<Request>> GetIncludingFiltred(Expression<Func<Request, bool>> Filter, int page = 1, int pageSize = 5, params Expression<Func<Request, object>>[] Properties)
        {
            IQueryable<Request> query = Include(Properties);
            return await GetPagedAsync(query.Where(Filter), page, pageSize);
        }

        public async Task<PagedList<Request>> GetPagedAsync(IQueryable<Request> query, int page, int pageSize = 6)
        {
            return new PagedList<Request>()
            {
                Count = query.Count(),
                Items = await query.Skip((page - 1) * pageSize).Take(pageSize).ToListAsync()
            };
        }

        public async Task<IEnumerable<Request>> Get()
        {
            return await db.Requests.AsNoTracking().Include(r => r.Service)
                .Include(r => r.Condition)
                .Include(r => r.Price).ToListAsync();
        }
        public async Task Remove(int id)
        {
            var item = await db.Requests.FindAsync(id);
            db.Requests.Remove(item);
            await db.SaveChangesAsync();
        }

        public async Task Update(Request item)
        {
            db.Requests.Update(item);
            await db.SaveChangesAsync();
        }
    }
}

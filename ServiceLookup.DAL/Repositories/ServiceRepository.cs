using Microsoft.EntityFrameworkCore;
using ServiceLookup.DAL.Interfaces;
using ServiceLookup.DAL.Entity;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using ServiceLookup.DAL.Entity.PagedList;
using System.Linq.Expressions;

namespace ServiceLookup.DAL.Repositories
{
    public class ServiceRepository : IServiceRepository

    {
        private readonly ApplicationDbContext db;
        public ServiceRepository(ApplicationDbContext _db)
        {
            db = _db;
        }

        public void Create(Service item)
        {
            db.Services.Add(item);
            db.SaveChanges();
        }

        public async Task<IEnumerable<Service>> Get()
        {
            return await db.Services.AsNoTracking().ToListAsync();
        }

        public async Task<Service> FindById(int id)
        {
            return await db.Services.AsNoTracking().Include(s => s.Price)
                                .Include(s => s.ServiceType).FirstAsync(s => s.Id == id);
        }

        public IQueryable<Service> Include(params Expression<Func<Service, object>>[] Properties)
        {
            IQueryable<Service> query = db.Services.AsNoTracking();
            return Properties
                .Aggregate(query, (current, includeProperty) => current.Include(includeProperty));
        }

        //For includes
        public async Task<IEnumerable<Service>> GetIncluding(params Expression<Func<Service, object>>[] Properties)
        {
            return await Include(Properties).ToListAsync();
        }

        //For PagedList of filters, sorting, and many includes
        public async Task<PagedList<Service>> GetIncludingFiltred(List<Expression<Func<Service, bool>>> Filters, Expression<Func<Service, object>> Order, bool IsDesc,
             int page = 1, int pageSize = 15, params Expression<Func<Service, object>>[] Properties)
        {
            IQueryable<Service> query = Include(Properties);//Нужен ли asnotracking???
            foreach (var filter in Filters)
            {
                query = query.Where(filter);
            }
            if (IsDesc)
            {
                query = query.OrderByDescending(Order);
            }
            else
            {
                query = query.OrderBy(Order);
            }
            return await GetPagedAsync(query, page, pageSize);
        }

        //For one filter and many includes
        public async Task<IEnumerable<Service>> GetIncludingFiltred(Expression<Func<Service, bool>> Filter,
            params Expression<Func<Service, object>>[] Properties)
        {
            IQueryable<Service> query = Include(Properties);//Нужен ли asnotracking???
            return await query.Where(Filter).ToListAsync();
        }

        public async Task<PagedList<Service>> GetPagedAsync(IQueryable<Service> query, int page, int pageSize = 15)
        {
            return new PagedList<Service>()
            {
                Count = query.Count(),
                Items = await query.Skip((page - 1) * pageSize).Take(pageSize).ToListAsync()
            };
        }

        public async Task Remove(int id)
        {
            var item = await db.Services.FindAsync(id);
            db.Services.Remove(item);
            await db.SaveChangesAsync();
        }

        public async Task Update(Service item)
        {
            db.Services.Update(item);
            await db.SaveChangesAsync();
        }
    }
}

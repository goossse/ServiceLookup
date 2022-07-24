using Microsoft.EntityFrameworkCore;
using ServiceLookup.DAL.Interfaces;
using ServiceLookup.DAL.Entity;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using ServiceLookup.DAL.Entity.PagedList;

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

        public async Task<Service> FindById(int id)
        {
            return await db.Services.AsNoTracking().Include(s => s.Price)
                                .Include(s => s.ServiceType).FirstAsync(s => s.Id == id);
        }



        public async Task<PagedList<Service>> FindByProperties(string searchText, int? typeId = null, string sortOrder = "Самые новые",
            bool IsRatedOnly = false, int? rateStart = 0, int? rateEnd = 10, int page = 1, int pageSize = 12, int? userId = null)
        {
            IQueryable<Service> query = db.Services.Include(s => s.Price);
            //Filtration
            if (typeId != null) { query = query.Where(s => s.TypeId == typeId); }
            if (IsRatedOnly) 
            {
                query = query.Where(s => s.AverageRate != null);
                query = query.Where(s => s.AverageRate > rateStart);
                query = query.Where(s => s.AverageRate < rateEnd);
            }
            if (!String.IsNullOrEmpty(searchText)) { query = query.Where(s => s.Title!.Contains(searchText)); }
            //Sorting
            switch (sortOrder)
            {
                case "Найновіші":
                    query = query.OrderByDescending(s => s.DateOfCreating); break;
                case "Найстаріші":
                    query = query.OrderBy(s => s.DateOfCreating); break;
                case "Ім'я":
                    query = query.OrderBy(s => s.Title); break;
                case "Ім'я (у зворотньому)":
                    query = query.OrderByDescending(s => s.Title); break;
                case "Рейтинг":
                    query = query.OrderBy(s => s.AverageRate); break;
                case "Рейтинг (у зворотньому)":
                    query = query.OrderByDescending(s => s.AverageRate); break;
            }     
            /*return await query.ToListAsync();*/
            return await GetPagedAsync(query, page, pageSize);
        }

        public async Task<PagedList<Service>> GetPagedAsync(IQueryable<Service> query, int page, int pageSize = 15)
        {
            return new PagedList<Service>() { Count = query.Count(),
                Items = await query.Skip((page - 1) * pageSize).Take(pageSize).ToListAsync()   };
        }

        public async Task<IEnumerable<Service>> Get()
        {
            return await db.Services.AsNoTracking().Include(s => s.Price)
                .ToListAsync();
        }

        public async Task<IEnumerable<Service>> GetByUser(int userId)
        {
            return await db.Services.AsNoTracking().Include(s => s.Price).Where(s => s.UserId == userId).ToListAsync();
        }

        public async Task<IEnumerable<Service>> GetByType(int typeId)
        {
            return await db.Services.AsNoTracking().Include(s => s.Price).Where(s => s.TypeId == typeId).ToListAsync();
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

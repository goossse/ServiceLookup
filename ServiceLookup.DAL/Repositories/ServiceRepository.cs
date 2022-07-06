using Microsoft.EntityFrameworkCore;
using ServiceLookup.DAL.Interfaces;
using ServiceLookup.DAL.Entity;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace ServiceLookup.DAL.Repositories
{
    public class ServiceRepository : IServiceRepository

    {
        private readonly ApplicationDbContext db;
        public ServiceRepository (ApplicationDbContext _db)
        {
            db = _db;
        }
        public async Task<bool> Create(Service entity)
        {
            db.Services.Add(entity);
            await db.SaveChangesAsync();
            return true;
        }

        public async Task<bool> Delete(Service entity)
        {
            db.Services.Remove(entity);
            await db.SaveChangesAsync();
            return true;
        }

        public async Task<Service> Get(int id)
        {
           return await db.Services.FirstOrDefaultAsync(s => s.Id == id);
        }

        public async Task<Service> GetByTitleAsync(string title)
        {
            return await db.Services.FirstOrDefaultAsync(s => s.Title == title);
        }

        public Task<List<Service>> GetAll()
        {
            return db.Services.ToListAsync();
        }
    }
}

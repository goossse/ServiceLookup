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

        public void Create(Service item)
        {
            db.Services.Add(item);
            db.SaveChanges();
        }

        public async Task<Service> FindById(int id)
        {
            return await db.Services.FindAsync(id);
        }

        public async Task<IEnumerable<Service>> FindByTitleAsync(string text)
        {
            return await db.Services.Where(s => s.Title.Contains(text)).ToListAsync();
        }

        public async Task<IEnumerable<Service>> Get()
        {
            return await db.Services.ToListAsync();
        }

        public async Task<Service> GetByTitleAsync(string title)
        {
            return await db.Services.FirstOrDefaultAsync(s => s.Title == title);
        }

        public async Task<IEnumerable<Service>> GetByUser(int userId)
        {
            return await db.Services.Where(s=> s.UserId == userId).ToListAsync();
        }

        public async Task Remove(int id)
        {
            var item = await db.Services.FindAsync(id);
            db.Services.Remove(item);
            await db.SaveChangesAsync();
        }

        public void Update(Service item)
        {
            db.Services.Update(item);
            db.SaveChanges();
        }
    }
}

using Microsoft.EntityFrameworkCore;
using ServiceLookup.DAL.Interfaces;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace ServiceLookup.DAL.Repositories
{
    public class BaseRepository<T> : IBaseRepository<T> where T : class
    {
        private readonly ApplicationDbContext db;
        private readonly DbSet<T> dbSet;
        public BaseRepository(ApplicationDbContext _db)
        {
            db = _db;
            dbSet = _db.Set<T>();
        }
        public BaseRepository()
        {
            db = new ApplicationDbContext();
            dbSet = db.Set<T>();
        }

        public void Create(T item)
        {
            dbSet.Add(item);
            db.SaveChanges();
        }

        public async Task<T> FindById(int id)
        {
            return await dbSet.FindAsync(id);
        }

        public async Task<IEnumerable<T>> Get()
        {
            return await dbSet.AsNoTracking().ToListAsync();
        }

        public async Task Remove(int id)
        {
            var item = await dbSet.FindAsync(id);
            dbSet.Remove(item);
            await db.SaveChangesAsync();
        }

        public async Task Update(T item)
        {
            db.Update(item);
            await db.SaveChangesAsync();
        }
    }
}

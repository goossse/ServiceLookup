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

        public T FindById(int id)
        {
            return dbSet.Find(id);
        }

/*        public Task<T> FindByIdAsync(int id)
        {
            var item = dbSet.FindAsync(id);
            db.Entry(item).State = EntityState.Detached;
            return item;
        }*/

        public IEnumerable<T> Get()
        {
            return dbSet.AsNoTracking().ToList();
        }

        public void Remove(T item)
        {
            dbSet.Remove(item);
            db.SaveChanges();
        }

        public void Update(T item)
        {
            dbSet.Update(item);
            db.SaveChanges();
        }
    }
}

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
    public class ReviewRepository : IReviewRepository
    {
        private readonly ApplicationDbContext db;

        public ReviewRepository()
        {
        }
        public ReviewRepository(ApplicationDbContext _db)
        {
            db = _db;
        }
        public void Create(Review item)
        {
            db.Reviews.Add(item);
            db.SaveChanges();
        }

        public async Task<Review> FindById(int id)
        {
            return await db.Reviews.AsNoTracking().Include(r => r.Service)
                .Include(r => r.Criterias)
                .FirstAsync(r => r.Id == id);
        }

        public IQueryable<Review> Include(params Expression<Func<Review, object>>[] Properties)
        {
            IQueryable<Review> query = db.Reviews.AsNoTracking();
            return Properties
                .Aggregate(query, (current, includeProperty) => current.Include(includeProperty));
        }

        //For includes
        public async Task<IEnumerable<Review>> GetIncluding(params Expression<Func<Review, object>>[] Properties)
        {
            return await Include(Properties).ToListAsync();
        }

        //For filter and many includes
        public async Task<IEnumerable<Review>> GetIncludingFiltred(Expression<Func<Review, bool>> Filter, params Expression<Func<Review, object>>[] Properties)
        {
            IQueryable<Review> query = Include(Properties);
            return await query.Where(Filter).ToListAsync();
        }

        public async Task<IEnumerable<Review>> Get()
        {
            return await db.Reviews.AsNoTracking().Include(r => r.Service).ToListAsync();
        }

        public async Task<IEnumerable<Review>> GetMyReviews(int id)
        {
            return await db.Reviews.AsNoTracking().Include(r => r.Service)
                .Include(r=>r.Criterias)
                .Where(r => r.Service!.UserId == id)
                .ToListAsync();
        }

        public async Task Remove(int id)
        {
            var item = await db.Reviews.FindAsync(id);
            db.Reviews.Remove(item);
            await db.SaveChangesAsync();
        }

        public async Task Update(Review item)
        {
            db.Reviews.Update(item);
            await db.SaveChangesAsync();
        }
    }
}

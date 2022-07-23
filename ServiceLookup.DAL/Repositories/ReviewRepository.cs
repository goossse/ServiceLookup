using Microsoft.EntityFrameworkCore;
using ServiceLookup.DAL.Entity;
using ServiceLookup.DAL.Interfaces;
using System;
using System.Collections.Generic;
using System.Linq;
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
            return await db.Reviews.Include(r => r.Service).FirstAsync(r => r.Id == id);
        }

        public async Task<IEnumerable<Review>> Get()
        {
            return await db.Reviews.Include(r => r.Service).ToListAsync();
        }

        public async Task<IEnumerable<Review>> GetMyReviews(int id)
        {
            return await db.Reviews.Include(r => r.Service)
                .Where(r => r.Service!.UserId == id)
                .ToListAsync();
        }

        public async Task Remove(int id)
        {
            var item = await db.Reviews.FindAsync(id);
            db.Reviews.Remove(item);
            await db.SaveChangesAsync();
        }

        public void Update(Review item)
        {
            db.Reviews.Update(item);
            db.SaveChanges();
        }
    }
}

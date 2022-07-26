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

        public async Task<Request> FindById(int id)
        {
            return await db.Requests.AsNoTracking().Include(r => r.Service)
                /*.Include(r => r.ConditionId)*/
                .Include(r => r.Price).FirstAsync(r => r.Id == id);
        }


















        public async Task<IEnumerable<Request>> Get()
        {
            return await db.Requests.AsNoTracking().Include(r => r.Service)
                /*.Include(r=>Condition)*/
                .Include(r => r.Price).ToListAsync();
        }

        public async Task<IEnumerable<Request>> GetRequestsServices(int id, int conditionId)
        {
            IEnumerable<Request> requests = await db.Requests.AsNoTracking().Include(r => r.Service)
                .Include(r => r.Price)
                /*.Include(r=>Condition)*/
                .Where(r => r.Service!.UserId == id /*&& r.ConditionId == conditionId*/).ToListAsync();
            return requests;
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

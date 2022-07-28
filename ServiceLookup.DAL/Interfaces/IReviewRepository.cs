using ServiceLookup.DAL.Entity;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Linq.Expressions;
using System.Text;
using System.Threading.Tasks;

namespace ServiceLookup.DAL.Interfaces
{
    public interface IReviewRepository : IBaseRepository<Review>
    {
        public Task<IEnumerable<Review>> GetMyReviews(int id);
        public Task<IEnumerable<Review>> GetIncluding(params Expression<Func<Review, object>>[] Properties);
        public Task<IEnumerable<Review>> GetIncludingFiltred(Expression<Func<Review, bool>> Filter, params Expression<Func<Review, object>>[] Properties);


    }
}

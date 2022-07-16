using ServiceLookup.DAL.Entity;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace ServiceLookup.DAL.Interfaces
{
    public interface IUnitOfWork
    {
        IBaseRepository<Condition> Conditions { get; }
        IBaseRepository<Price> Prices { get; }
        IBaseRepository<Request> Requests { get; }
        IBaseRepository<Review> Reviews { get; }
        IBaseRepository<ReviewCriteria> ReviewCriterias { get; }
        IServiceRepository Services { get; }
        IBaseRepository<ServiceType> ServiceTypes { get; }
        IBaseRepository<User> Users { get; }
        void Save();
        void Dispose (bool disposing);
        void Dispose();


    }
}

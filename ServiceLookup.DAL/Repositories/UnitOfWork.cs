using ServiceLookup.DAL.Entity;
using ServiceLookup.DAL.Interfaces;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace ServiceLookup.DAL.Repositories
{
    public class UnitOfWork : IUnitOfWork
    {
        private static ApplicationDbContext db/* = new ApplicationDbContext()???*/;
        public IBaseRepository<Condition> repositoryConditions;

        public IBaseRepository<Price> repositoryPrices;

        public IBaseRepository<Request> repositoryRequests;

        public IBaseRepository<Review> repositoryReviews;

        public IBaseRepository<ReviewCriteria> repositoryReviewCriterias;

        public IServiceRepository repositoryServices;

        public IBaseRepository<ServiceType> repositoryServiceTypes;

        public IBaseRepository<User> repositoryUsers;


        public UnitOfWork()
        {

        }
        public UnitOfWork (ApplicationDbContext _db)
        {
            db = _db;
        }
        public IBaseRepository<Condition> Conditions
        {
            get
            {
                if (repositoryConditions == null)
                    repositoryConditions = new BaseRepository<Condition>(db);
                return repositoryConditions;
            }
        }
        public IBaseRepository<Price> Prices
        {
            get
            {
                if (repositoryPrices == null)
                    repositoryPrices = new BaseRepository<Price>(db);
                return repositoryPrices;
            }
        }
        public IBaseRepository<Request> Requests
        {
            get
            {
                if (repositoryRequests == null)
                    repositoryRequests = new BaseRepository<Request>(db);
                return repositoryRequests;
            }
        }
        public IBaseRepository<Review> Reviews
        {
            get
            {
                if (repositoryReviews == null)
                    repositoryReviews = new BaseRepository<Review>(db);
                return repositoryReviews;
            }
        }
        public IBaseRepository<ReviewCriteria> ReviewCriterias
        {
            get
            {
                if (repositoryReviewCriterias == null)
                    repositoryReviewCriterias = new BaseRepository<ReviewCriteria>(db);
                return repositoryReviewCriterias;
            }
        }
        public IServiceRepository Services
        {
            get
            {
                if (repositoryServices == null)
                    repositoryServices = new ServiceRepository(db);
                return repositoryServices;
            }
        }
        public IBaseRepository<ServiceType> ServiceTypes
        {
            get
            {
                if (repositoryServiceTypes == null)
                    repositoryServiceTypes = new BaseRepository<ServiceType>(db);
                return repositoryServiceTypes;
            }
        }
        public IBaseRepository<User> Users
        {
            get
            {
                if (repositoryUsers == null)
                    repositoryUsers = new BaseRepository<User>(db);
                return repositoryUsers;
            }
        }

        private bool disposed = false;

        public virtual void Dispose(bool disposing)
        {
            if (!this.disposed)
            {
                if (disposing)
                {
                    db.Dispose();
                }
                this.disposed = true;
            }
        }

        public void Dispose()
        {
            Dispose(true);
            GC.SuppressFinalize(this);
        }


        public void Save()
        {
            db.SaveChanges();
        }
    }
}

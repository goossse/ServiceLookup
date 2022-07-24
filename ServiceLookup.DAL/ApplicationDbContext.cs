using Microsoft.AspNetCore.Identity.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore;
using ServiceLookup.DAL.Entity;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace ServiceLookup.DAL
{
    public class ApplicationDbContext : IdentityDbContext<User, Role, int>
    {

        public ApplicationDbContext(DbContextOptions<ApplicationDbContext> options) : base(options)
        {
            /*Database.EnsureCreated();*/
        }
        public ApplicationDbContext()
        {
            /*Database.EnsureCreated();*/

        }

/*        public DbSet<User> Users { get; set; } = null!;
*/        public DbSet<Condition> Conditions { get; set; } = null!;
        public DbSet<Price> Prices { get; set; } = null!;
        public DbSet<Request> Requests { get; set; } = null!;
        public DbSet<Review> Reviews { get; set; } = null!;
        public DbSet<ReviewCriteria> ReviewCriterias { get; set; } = null!;
        public DbSet<Service> Services { get; set; } = null!;
        public DbSet<ServiceType> ServiceTypes { get; set; } = null!;
    }
}

using ServiceLookup.DAL.Entity;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace ServiceLookup.BL.Services.Interfaces
{
    public interface IAdmin
    {
        public Task<User> GetUser(int id);
        public Task<IEnumerable<User>> GetUsers();


    }
}

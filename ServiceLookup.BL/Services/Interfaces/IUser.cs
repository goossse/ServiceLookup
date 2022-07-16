using ServiceLookup.BL.DTO;
using ServiceLookup.DAL.Entity;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace ServiceLookup.BL.Services.Interfaces
{
    public interface IUser
    {
        public Task<IEnumerable<UserDTO>> GetUsers();
        public Task<UserDTO> GetUserAsync(int id);
    }
}

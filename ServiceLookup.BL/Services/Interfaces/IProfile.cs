using Microsoft.AspNetCore.Identity;
using ServiceLookup.BL.DTO;
using ServiceLookup.DAL.Entity;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace ServiceLookup.BL.Services.Interfaces
{
    public interface IProfile
    {
        public Task<UserDTO> GetProfile(int id);
        public Task EditProfile(User user, UserManager<User> userManager);
        public void DeleteProfile(int id);
    }
}

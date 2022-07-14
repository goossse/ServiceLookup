using Microsoft.AspNetCore.Identity;
using ServiceLookup.BL.Services.Interfaces;
using ServiceLookup.DAL;
using ServiceLookup.DAL.Entity;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace ServiceLookup.BL.Services.Implementations
{
    public class UserService :IUser
    {
        UserManager<User> userManager;
        
        public UserService(UserManager<User> _userManager)
        {
            userManager = _userManager;
        }

        public IEnumerable<User> GetUsers()
        {
            return userManager.Users.ToList();
        }

        public async Task<User> GetUserAsync(int id)
        {
            User user = await userManager.GetUserAsync(int id); //поменять ключ для юзера
            return user;
        }
    }
}

using ServiceLookup.BL.Services.Interfaces;
using ServiceLookup.DAL.Entity;
using ServiceLookup.DAL.Interfaces;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace ServiceLookup.BL.Services.Implementations
{
    public class AdminService : IAdmin
    {
        private readonly IUnitOfWork unitOfWork;

        public AdminService(IUnitOfWork _unitOfWork)
        {
            unitOfWork = _unitOfWork;
        }


        public async Task<User> GetUser(int id)
        {
            return await unitOfWork.Users.FindById(id);
        }
        public async Task<IEnumerable<User>> GetUsers()
        {
            return await unitOfWork.Users.Get();
        }
    }
}

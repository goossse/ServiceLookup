using AutoMapper;
using Microsoft.AspNetCore.Identity;
using ServiceLookup.BL.DTO;
using ServiceLookup.BL.Mapper;
using ServiceLookup.BL.Services.Interfaces;
using ServiceLookup.DAL;
using ServiceLookup.DAL.Entity;
using ServiceLookup.DAL.Interfaces;
using ServiceLookup.DAL.Repositories;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace ServiceLookup.BL.Services.Implementations
{
    public class UserService :IUser
    {
        private IUnitOfWork unitOfWork;
        private readonly IMapper mapper;
        public UserService(ApplicationDbContext db)
        {
            unitOfWork = new UnitOfWork(db);
            var mappingConfig = new MapperConfiguration(mc =>
            {
                mc.AddProfile(new AutoMapperData());
            });
            mapper = mappingConfig.CreateMapper();
        }

        public async Task<IEnumerable<UserDTO>> GetUsers()
        {
            var list = mapper.Map<List<UserDTO>>(await unitOfWork.Users.Get());
            return list;
        }

        public async Task<UserDTO> GetUserAsync(int id)
        {
            var user = mapper.Map<UserDTO>(await unitOfWork.Users.FindById(id));
            return user;
        }

        public async Task<IEnumerable<ReviewDTO>> GetServicesReviews(int id)
        {
            var list = await unitOfWork.Reviews.GetIncludingFiltred(r => r.ServiceId == id, r => r.Service!, r => r.Criterias!);
            return mapper.Map<IEnumerable<ReviewDTO>>(list);
        }
    }
}

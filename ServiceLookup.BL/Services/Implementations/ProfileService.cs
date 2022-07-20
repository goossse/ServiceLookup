using AutoMapper;
using Microsoft.AspNetCore.Identity;
using ServiceLookup.BL.DTO;
using ServiceLookup.BL.Mapper;
using ServiceLookup.BL.Services.Interfaces;
using ServiceLookup.DAL;
using ServiceLookup.DAL.Entity;
using ServiceLookup.DAL.Interfaces;
using ServiceLookup.DAL.Repositories;

namespace ServiceLookup.BL.Services.Implementations
{
    public class ProfileService : IProfile
    {
        IUnitOfWork unitOfWork;
        private readonly IMapper mapper;
        public ProfileService(ApplicationDbContext db)
        {
            unitOfWork = new UnitOfWork(db);
            var mappingConfig = new MapperConfiguration(mc =>
            {
                mc.AddProfile(new AutoMapperData());
            });
            mapper = mappingConfig.CreateMapper();
        }

        public async void DeleteProfile(int id)
        {
            await unitOfWork.Users.Remove(id);
        }

        public async Task EditProfile(User _user, UserManager<User> userManager)
        {
            await userManager.UpdateAsync(_user);
        }

        public async Task<UserDTO> GetProfile(int id)
        {
            User profile = await unitOfWork.Users.FindById(id);
            return (mapper.Map<UserDTO>(profile));
        }
    }
}

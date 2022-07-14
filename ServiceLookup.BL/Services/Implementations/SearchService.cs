using AutoMapper;
using ServiceLookup.BL.DTO;
using ServiceLookup.BL.Mapper;
using ServiceLookup.BL.Services.Interfaces;
using ServiceLookup.DAL;
using ServiceLookup.DAL.Interfaces;
using ServiceLookup.DAL.Repositories;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace ServiceLookup.BL.Services.Implementations
{
    public class SearchService : ISearch
    {
        private readonly IMapper mapper;
        private IUnitOfWork unitOfWork;
        public SearchService(ApplicationDbContext db)
        {
            unitOfWork = new UnitOfWork(db);
            var mappingConfig = new MapperConfiguration(mc =>
            {
                mc.AddProfile(new AutoMapperData());
            });
            mapper = mappingConfig.CreateMapper();
        }

        public async Task<IEnumerable<ServiceDTO>> GetServices()
        {
            var list = mapper.Map<List<ServiceDTO>>(await unitOfWork.Services.Get());
            return list;
        }

        public async Task<ServiceDTO> GetService(int id)
        {
            return mapper.Map<ServiceDTO>(await unitOfWork.Services.FindById(id));
        }

        public IEnumerable<ServiceDTO> GetUsersServices(int userId)
        {
            var list = mapper.Map<List<ServiceDTO>>(unitOfWork.Services.GetByUser(userId));
            return list;
        }
    }
}

using AutoMapper;
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
    public class ServiceService : IService
    {
        private readonly IMapper mapper;
        private IUnitOfWork unitOfWork;
        public ServiceService(ApplicationDbContext db)
        {
            unitOfWork = new UnitOfWork(db);
            var mappingConfig = new MapperConfiguration(mc =>
            {
                mc.AddProfile(new AutoMapperData());
            });
            mapper = mappingConfig.CreateMapper();
        }

        public bool CreateService(ServiceDTO _service)
        {
            var service = mapper.Map<Service>(_service);
            unitOfWork.Services.Create(service);
            return true;
        }
        public IEnumerable<ServiceDTO> MyServices(Guid userId)
        {
            /*            var List = mapper.Map<List<ServiceDTO>>(unitOfWork.Services.Get());
                        return List.Where(s => s.UserId == userId);*/
            throw new NotImplementedException();
        }
    }

}

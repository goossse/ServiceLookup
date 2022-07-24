
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
    public class ModerService : IModer
    {
        private readonly IMapper mapper;
        private IUnitOfWork unitOfWork;
        public ModerService(ApplicationDbContext db)
        {
            unitOfWork = new UnitOfWork(db);
            var mappingConfig = new MapperConfiguration(mc =>
            {
                mc.AddProfile(new AutoMapperData());
            });
            mapper = mappingConfig.CreateMapper();
        }
        public bool AddServiceType(ServiceTypeDTO _serviceType)
        {
            ServiceType serviceType = mapper.Map<ServiceType>(_serviceType);
            unitOfWork.ServiceTypes.Create(serviceType);
            return true;
        }
    }
}

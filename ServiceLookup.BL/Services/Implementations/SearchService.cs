using AutoMapper;
using ServiceLookup.BL.DTO;
using ServiceLookup.BL.DTO.PagedList;
using ServiceLookup.BL.Mapper;
using ServiceLookup.BL.Services.Interfaces;
using ServiceLookup.DAL;
using ServiceLookup.DAL.Entity;
using ServiceLookup.DAL.Entity.PagedList;
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
            var item = await unitOfWork.Services.FindById(id);
            return mapper.Map<ServiceDTO>(item);
        }

        public IEnumerable<ServiceDTO> GetUsersServices(int userId)
        {
            var list = mapper.Map<List<ServiceDTO>>(unitOfWork.Services.GetByUser(userId));
            return list;
        }

        public async Task<PagedListDTO<ServiceDTO>> FindServices(string _text, int? _typeId, string _sortOrder,
            bool _isRatedOnly, int? _rateStart, int? _rateEnd, int _page = 1, int _pageSize = 15)
        {
            PagedList<Service> pagedList = await unitOfWork.Services.FindByProperties(_text, _typeId, _sortOrder, _isRatedOnly, _rateStart, _rateEnd, _page, _pageSize);
            PagedListDTO<ServiceDTO> pagedListDTO = new PagedListDTO<ServiceDTO>() { Items = mapper.Map<List<ServiceDTO>>(pagedList.Items), Count = pagedList.Count };
            return pagedListDTO;
        }

        public async Task<PriceDTO> GetPrice(int id)
        {
            PriceDTO price = mapper.Map<PriceDTO>(await unitOfWork.Prices.FindById(id));
            return price;
        }

        public async Task<IEnumerable<ServiceTypeDTO>> GetTypes()
        {
            var list = mapper.Map<List<ServiceTypeDTO>>(await unitOfWork.ServiceTypes.Get());
            return list;
        }

        public async Task<ServiceTypeDTO> GetServiceType(int id)
        {
            ServiceTypeDTO serviceType = mapper.Map<ServiceTypeDTO>(await unitOfWork.ServiceTypes.FindById(id));
            return serviceType;
        }

        public async Task<IEnumerable<ServiceDTO>> GetServicesByType(int typeId)
        {
            var list = mapper.Map<List<ServiceDTO>>(await unitOfWork.Services.GetByType(typeId));
            return list;
        }

    }
}

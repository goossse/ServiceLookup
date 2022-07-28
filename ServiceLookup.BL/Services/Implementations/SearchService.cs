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
using System.Linq.Expressions;
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
            var list = mapper.Map<List<ServiceDTO>>(await unitOfWork.Services.GetIncluding(s => s.Price));
            return list;
        }

        public async Task<ServiceDTO> GetService(int id)
        {
            var item = await unitOfWork.Services.FindById(id);
            return mapper.Map<ServiceDTO>(item);
        }

        public IEnumerable<ServiceDTO> GetUsersServices(int userId)
        {
            var list = mapper.Map<List<ServiceDTO>>(unitOfWork.Services.GetIncludingFiltred(s => s.UserId == userId, s => s.Price!));
            return list;
        }

        public async Task<DTO.PagedList.PagedListDTO<ServiceDTO>> FindServices(string _text, int? _typeId, string _sortOrder,
            bool _isRatedOnly, int? _rateStart = 10, int? _rateEnd = 1, int _page = 1, int _pageSize = 15)
        {

            List<Expression<Func<Service, bool>>> filtersExps = new List<Expression<Func<Service, bool>>>();
            if (_typeId != null)
            {
                filtersExps.Add(s => s.ServiceTypeId == _typeId);
            }
            if (_isRatedOnly)
            {
                filtersExps.Add(s => s.AverageRate != null && s.AverageRate > _rateStart && s.AverageRate < _rateEnd);
            }
            if (!String.IsNullOrEmpty(_text))
            {
                filtersExps.Add(s => s.Title!.Contains(_text));
            }
            Expression<Func<Service, object>> orderExp = (s) => s.DateOfCreating;
            bool orderDesc = true;
            switch (_sortOrder)
            {
                case "Найстаріші":
                    orderExp = (s) => s.DateOfCreating;
                    orderDesc = false; break;
                case "Ім'я":
                    orderExp = (s) => s.Title!;
                    orderDesc = false; break;
                case "Ім'я (у зворотньому)":
                    orderExp = (s) => s.Title!; break;
                case "Рейтинг":
                    orderExp = (s) => s.AverageRate!;
                    orderDesc = false; break;
                case "Рейтинг (у зворотньому)":
                    orderExp = (s) => s.AverageRate!; break;
            }
            PagedList<Service> pagedList = await unitOfWork.Services.GetIncludingFiltred(filtersExps, orderExp, orderDesc, _page, _pageSize, s => s.Price!);
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

        public IEnumerable<ServiceDTO> GetServicesByType(int typeId)
        {
            var list = mapper.Map<List<ServiceDTO>>(unitOfWork.Services.GetIncludingFiltred(s => s.ServiceTypeId == typeId, s => s.Price!));
            return list;
        }

    }
}

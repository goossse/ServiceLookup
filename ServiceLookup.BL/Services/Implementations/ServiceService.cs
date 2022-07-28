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
            Service service = mapper.Map<Service>(_service);
            unitOfWork.Services.Create(service);
            return true;
        }

        public async Task DeleteService(int id)
        {
            await unitOfWork.Services.Remove(id);
        }

        public async Task EditService(ServiceDTO _service)
        {
            Service service = mapper.Map<Service>(_service);
            await unitOfWork.Services.Update(service);
        }

        public async Task<IEnumerable<ServiceDTO>> MyServices(int userId)
        {
            return mapper.Map<IEnumerable<ServiceDTO>>(await unitOfWork.Services.GetIncludingFiltred(s => s.UserId == userId, s => s.Price!));
        }

        public async Task<DTO.PagedList.PagedListDTO<RequestDTO>> GetMyRequests(int userId, int page, int pageSize)
        {
            PagedList<Request> list = await unitOfWork.Requests.GetIncludingFiltred(r => r.Service!.UserId == userId && r.ConditionId == 1, page, pageSize,
                r => (r.Condition!), r => r.Price, r => (r.Service!));
            return new DTO.PagedList.PagedListDTO<RequestDTO>() { Items = mapper.Map<IEnumerable<RequestDTO>>(list.Items), Count = list.Count };
        }

        public async Task<DTO.PagedList.PagedListDTO<RequestDTO>> GetMyBookings(int userId, int page, int pageSize)
        {
            PagedList<Request> list = await unitOfWork.Requests.GetIncludingFiltred(r => r.Service!.UserId == userId && r.ConditionId == 3, page, pageSize,
                r => (r.Condition!), r => r.Price, r => (r.Service!));
            return new DTO.PagedList.PagedListDTO<RequestDTO>() { Items = mapper.Map<IEnumerable<RequestDTO>>(list.Items), Count = list.Count };
        }

        public async Task<IEnumerable<ReviewDTO>> MyReviews(int userId)
        {
            return mapper.Map<IEnumerable<ReviewDTO>>(await unitOfWork.Reviews.GetIncludingFiltred(r => r.UserId == userId, r => r.Service!, r => r.Criterias!));
        }

        public async Task AnswerRequest(int id, int conditionId)
        {
            Request request = mapper.Map<Request>(await unitOfWork.Requests.FindById(id));
            request.ConditionId = conditionId;
            await unitOfWork.Requests.Update(request);
        }
    }

}

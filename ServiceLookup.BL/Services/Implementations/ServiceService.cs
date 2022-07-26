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
            return mapper.Map<IEnumerable<ServiceDTO>>(await unitOfWork.Services.GetIncludingFiltred(s => s.UserId == userId, s => s.Price));
        }

        public async Task<IEnumerable<RequestDTO>> MyRequests(int userId)
        {
            return mapper.Map<IEnumerable<RequestDTO>>(await unitOfWork.Requests.GetRequestsServices(userId, 0));
        }

        public async Task<IEnumerable<RequestDTO>> MyBookings(int userId)
        {
            return mapper.Map<IEnumerable<RequestDTO>>(await unitOfWork.Requests.GetRequestsServices(userId, 1));
        }

        public async Task<IEnumerable<ReviewDTO>> MyReviews(int userId)
        {
            return mapper.Map<IEnumerable<ReviewDTO>>(await unitOfWork.Reviews.GetMyReviews(userId));
        }

        public async Task AnswerRequest(int id, int conditionId)
        {
            Request request = mapper.Map<Request>(await unitOfWork.Requests.FindById(id));
            request.ConditionId = conditionId;
            await unitOfWork.Requests.Update(request);
        }
    }

}

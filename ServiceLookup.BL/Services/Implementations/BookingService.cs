using AutoMapper;
using Microsoft.AspNetCore.Identity;
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
    public class BookingService : IBooking
    {
        UserManager<User> userManager;
        IUnitOfWork unitOfWork;
        private readonly IMapper mapper;
        public BookingService(IUnitOfWork _unitOfWork, UserManager<User> _userManager)
        {
            unitOfWork = _unitOfWork;
            userManager = _userManager;
            var mappingConfig = new MapperConfiguration(mc =>
            {
                mc.AddProfile(new AutoMapperData());
            });
            mapper = mappingConfig.CreateMapper();
        }

        public void ApplyRequest(RequestDTO _request)
        {
            Request request = mapper.Map<Request>(_request);
            unitOfWork.Requests.Create(request);
        }

        public async Task<DTO.PagedList.PagedListDTO<RequestDTO>> GetRequests(int userId, int page = 1, int pageSize = 5)
        {
            PagedList<Request> list = await unitOfWork.Requests.GetIncludingFiltred(r => r.UserId == userId, page, pageSize,
                r => r.Price, r => (r.Condition!), r => (r.Service!));
            return new PagedListDTO<RequestDTO>() { Items = mapper.Map<IEnumerable<RequestDTO>>(list.Items), Count = list.Count };
        }

        public async Task DeleteRequest(int id)
        {
            await unitOfWork.Requests.Remove(id);
        }

        public async Task CreateReview(ReviewDTO review)
        {
            foreach(ReviewCriteriaDTO criteria in review.Criterias)
            {
                review.Rate += criteria.Rate;
            }
            review.Rate /= review.Criterias.Count;
            await CountAverageRate(review.ServiceId, review.Rate);
            unitOfWork.Reviews.Create(mapper.Map<Review>(review));

        }

        public async Task<List<string>> GetCriteriesList(int serviceId)
        {
            string Criterias = (await unitOfWork.Services.FindById(serviceId)).ServiceType!.Criterias!;
            List<string> list = Criterias.Split(" ").ToList();
            return list;
        }

        public async Task CountAverageRate(int serviceId, double newRate)
        {
            //Average for Service
            Service service = await unitOfWork.Services.FindById(serviceId);
            service.AverageRate = newRate;
            foreach(Review review in service.Reviews!)
            {
                service.AverageRate += review.Rate;
            }
            service.AverageRate /= service.Reviews.Count + 1;
            //Average for User
            service.User!.AverageRate = 0;
            IEnumerable<Service> services = await unitOfWork.Services.GetIncludingFiltred(s => s.UserId == service.UserId);
            foreach(Service serv in services)
            {
                service.User.AverageRate += serv.AverageRate;
            }
            service.User.AverageRate /= services.Count();
            await unitOfWork.Services.Update(service);
            await userManager.UpdateAsync(service.User);
        }

        public async Task<ReviewDTO> GetReview(int id)
        {
            return mapper.Map<ReviewDTO>(await unitOfWork.Reviews.FindById(id));
        }
        public async Task<RequestDTO> GetRequest(int id)
        {
            return mapper.Map<RequestDTO>(await unitOfWork.Requests.FindById(id));
        }
        public async Task MarkRequestCompleted(int id)
        {
            Request request = await unitOfWork.Requests.GetById(id);
            request.ConditionId = 4;
            await unitOfWork.Requests.Update(request);
        }
    }
}

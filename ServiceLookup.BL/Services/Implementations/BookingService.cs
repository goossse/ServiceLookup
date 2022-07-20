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
    public class BookingService :IBooking
    {
        IUnitOfWork unitOfWork;
        private readonly IMapper mapper;
        public BookingService(ApplicationDbContext db)
        {
            unitOfWork = new UnitOfWork(db);
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

        public async Task<IEnumerable<RequestDTO>> GetRequests(int id)
        {
            IEnumerable<RequestDTO> list = mapper.Map<IEnumerable<RequestDTO>>(await unitOfWork.Requests.Get()).Where(r => r.UserId == id);
            return list;
        }
        
        public async Task DeleteRequest(int id)
        {
            await unitOfWork.Requests.Remove(id);
        }
    }
}

using ServiceLookup.BL.DTO;
using ServiceLookup.BL.DTO.PagedList;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace ServiceLookup.BL.Services.Interfaces
{
    public interface IService
    {
        public bool CreateService(ServiceDTO _service);
        public Task<IEnumerable<ServiceDTO>> MyServices(int userId);
        public Task EditService(ServiceDTO _service);
        public Task DeleteService(int id);
        public Task<PagedListDTO<RequestDTO>> GetMyRequests(int userId, int page, int pageSize);
        public Task<PagedListDTO<RequestDTO>> GetMyBookings(int userId, int page, int pageSize);

        public Task<IEnumerable<ReviewDTO>> MyReviews(int userId);

        public Task AnswerRequest(int id, int conditionId);


    }
}

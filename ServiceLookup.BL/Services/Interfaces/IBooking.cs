using ServiceLookup.BL.DTO;
using ServiceLookup.BL.DTO.PagedList;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace ServiceLookup.BL.Services.Interfaces
{
    public interface IBooking
    {
        public void ApplyRequest(RequestDTO _request);
        public Task<PagedListDTO<RequestDTO>> GetRequests(int userId, int page = 1, int pageSize = 5);
        public Task DeleteRequest(int id);
        public Task CreateReview(ReviewDTO review);
        public Task<List<string>> GetCriteriesList(int serviceId);
        public Task<ReviewDTO> GetReview(int id);
        public Task MarkRequestCompleted(int id);
        public Task<RequestDTO> GetRequest(int id);


    }
}

using ServiceLookup.BL.DTO;
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
        public void EditService(ServiceDTO _service);
        public Task DeleteService(int id);
        public Task<IEnumerable<RequestDTO>> MyRequests(int userId);
        public Task<IEnumerable<RequestDTO>> MyBookings(int userId);

        public Task<IEnumerable<ReviewDTO>> MyReviews(int userId);

        public Task AnswerRequest(int id, int conditionId);


    }
}

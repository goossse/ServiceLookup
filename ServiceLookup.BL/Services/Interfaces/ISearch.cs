using ServiceLookup.BL.DTO;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace ServiceLookup.BL.Services.Interfaces
{
    public interface ISearch
    {
        public Task<IEnumerable<ServiceDTO>> GetServices();
        public Task<ServiceDTO> GetService(int id);
        public IEnumerable<ServiceDTO> GetUsersServices(int userId); //async??
        public Task<IEnumerable<ServiceDTO>> GetServicesByTitle(string text);
    }
}

using ServiceLookup.BL.DTO;
using ServiceLookup.BL.DTO.PagedList;
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
        public Task<PagedListDTO<ServiceDTO>> FindServices(string _text, int? _typeId, string _sortOrder,
            bool _isRatedOnly, int? _rateStart, int? _rateEnd, int _page = 1, int _pageSize = 15);
        public IEnumerable<ServiceDTO> GetServicesByType(int typeId); 
        public Task<PriceDTO> GetPrice(int id);
        public Task<IEnumerable<ServiceTypeDTO>> GetTypes();
        public Task<ServiceTypeDTO> GetServiceType(int id);


    }
}

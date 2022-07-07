using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace ServiceLookup.BL.DTO
{
    public class ServiceDTO
    {
        public int Id { get; set; }
        public string? Title { get; set; }
        public string? Info { get; set; }

        public int? UserId { get; set; }
        public UserDTO? User { get; set; }
        public List<ServiceTypeDTO>? Types { get; set; }
        public List<RequestDTO>? Requests { get; set; }
        public List<PriceDTO>? PriceList { get; set; }
        public List<ReviewDTO>? Reviews { get; set; }
    }
}

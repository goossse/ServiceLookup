using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace ServiceLookup.BL.DTO
{
    public class ServiceTypeDTO
    {
        public int Id { get; set; }
        public string? Name { get; set; }
        public List<ServiceDTO>? Services { get; set; }
    }
}

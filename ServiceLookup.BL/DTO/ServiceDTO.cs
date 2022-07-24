using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Microsoft.AspNetCore;

namespace ServiceLookup.BL.DTO
{
    public class ServiceDTO
    {
        public int Id { get; set; }
        public string Title { get; set; }
        public string? Info { get; set; }
        public string? Image { get; set; }
        public double? AverageRate { get; set; }
        public DateTime DateOfCreating { get; set; }
        public int PriceId { get; set; }
        public PriceDTO? Price { get; set; }
        public int? UserId { get; set; }
        public UserDTO? User { get; set; }
        public int? TypeId { get; set; }
        public ServiceTypeDTO? ServiceType { get; set; }
        public List<RequestDTO>? Requests { get; set; }
        public List<ReviewDTO>? Reviews { get; set; }
    }
}

using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace ServiceLookup.BL.DTO
{
    public class RequestDTO
    {
        public int Id { get; set; }
        public string? Description { get; set; }
        public DateTime StartOfBooking { get; set; }
        public DateTime EndOfBooking { get; set; }
        public DateTime DateTimeOfCreating { get; set; }
        public int? PriceId { get; set; }
        public PriceDTO? Price { get; set; }
        public int UserId { get; set; }
        public UserDTO? User { get; set; }

        public int ServiceId { get; set; }
        public ServiceDTO? Service { get; set; }
        public int ConditionId { get; set; }
    }
}

using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace ServiceLookup.BL.DTO
{
    public class PriceDTO
    {
        public int Id { get; set; }
        public string? Currency { get; set; }
        public int Value { get; set; }
        public int ServiceId { get; set; }

    }
}

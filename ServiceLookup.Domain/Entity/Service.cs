using ServiceLookup.Domain.Entity.Base;
using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace ServiceLookup.Domain.Entity
{
    public class Service : BaseClass
    {
        [Required]
        [StringLength(30, MinimumLength = 2)]
        public string? Title { get; set; }

        [Required]
        [StringLength(2000, MinimumLength = 2)]
        public string? Info { get; set; }

        public int? UserId { get; set; }
        public User? User { get; set; }
        public List<ServiceType>? Types { get; set; }
        public List<Request>? Requests { get; set; }
        public List<Price>? PriceList { get; set; }
        public List<Review>? Reviews { get; set; }
    }
}

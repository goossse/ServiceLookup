using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace ServiceLookup.DAL.Entity
{
    public class Service
    {
        [Required]
        public int Id { get; set; }
        [Required]
        [StringLength(30, MinimumLength = 2)]
        public string? Title { get; set; }

        [Required]
        [StringLength(2000, MinimumLength = 2)]
        public string? Info { get; set; }
        public string? Image { get; set; }
        public double? AverageRate { get; set; }
        public DateTime DateOfCreating { get; set; }
        public int? PriceId { get; set; }
        public Price? Price { get; set; }
        public int? UserId { get; set; }
        public User? User { get; set; }
        public List<ServiceType>? Types { get; set; }
        public List<Request>? Requests { get; set; }
        public List<Review>? Reviews { get; set; }
    }
}

using ServiceLookup.Domain.Entity.Base;
using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace ServiceLookup.Domain.Entity
{
    public class User : BaseClass
    {
        [Required]
        [StringLength(30, MinimumLength = 2)]
        public string? Name { get; set; }

        [Required]
        [StringLength(30, MinimumLength = 2)]
        public string? Surname { get; set; }

        [Required]
        [Range(typeof(DateTime), "01-01-1950", "01-01-2004")]
        public DateTime DateOfBirth { get; set; }

        [Required]
        [StringLength(50)]
        public string? ContactDetails { get; set; }

        [Required]
        [EmailAddress]
        public string? Email { get; set; }

        public List<Request>? Requests { get; set; }
        public List<Service>? Services { get; set; }
        public List<Review>? Reviews { get; set; }
    }
}

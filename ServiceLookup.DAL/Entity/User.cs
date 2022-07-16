using Microsoft.AspNetCore.Identity;
using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace ServiceLookup.DAL.Entity
{
    public class User : IdentityUser<int>
    {
        [Required]
        [StringLength(30, MinimumLength = 2)]
        public string? Name { get; set; }

        [Required]
        [StringLength(30, MinimumLength = 2)]
        public string? Surname { get; set; }
        public double? AverageRate { get; set; }
        public string? Image { get; set; }
        public DateTime DateOfRegistration { get; set; }
        public string? ShortDescription { get; set; }

        [Required]
        [Range(typeof(DateTime), "01-01-1950", "01-01-2004")]
        public DateTime DateOfBirth { get; set; }

        [StringLength(50)]
        public string? ContactDetails { get; set; }

        public List<Request>? Requests { get; set; }
        public List<Service>? Services { get; set; }
        public List<Review>? Reviews { get; set; }
    }
}

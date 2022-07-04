using ServiceLookup.Domain.Entity.Base;
using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace ServiceLookup.Domain.Entity
{
    public class Review : BaseClass
    {
        [Required]
        [MaxLength(10)]
        [MinLength(1)]
        public int Rate { get; set; }

        [StringLength(2000, MinimumLength = 2)]
        public string? Text { get; set; }

        [Required]
        public string? Info { get; set; }
        public int? UserId { get; set; }
        public User? User { get; set; }
        [Required]
        public int? ServiceId { get; set; }
        public Service? Service { get; set; }
    }
}

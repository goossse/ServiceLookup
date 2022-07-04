using ServiceLookup.Domain.Entity.Base;
using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace ServiceLookup.Domain.Entity
{
    public class ReviewCriteria : BaseClass
    {
        [Required]
        [StringLength(30, MinimumLength = 2)]
        public string? Title { get; set; }
        [Required]
        [MaxLength(10)]
        [MinLength(1)]
        public int Rate { get; set; }
    }
}

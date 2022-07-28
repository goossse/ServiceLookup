using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace ServiceLookup.DAL.Entity
{
    public class ReviewCriteria
    {
        [Required]
        public int Id { get; set; }
        [Required]
        [StringLength(30, MinimumLength = 2)]
        public string? Title { get; set; }
        [Required]
        [MaxLength(10)]
        [MinLength(1)]
        public int Rate { get; set; }
        public int ReviewId { get; set; }
        public Review? Review { get; set; }
    }
}

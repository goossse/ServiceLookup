using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace ServiceLookup.BL.DTO
{
    public class ReviewCriteriaDTO
    {
        public int Id { get; set; }
        public string? Title { get; set; }
        public int Rate { get; set; }
        public int ReviewId { get; set; }
        public ReviewDTO? Review { get; set; }
    }
}

using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace ServiceLookup.BL.DTO
{
    public class ReviewDTO
    {
        public int Id { get; set; }
        public int Rate { get; set; }

        public string? Text { get; set; }

        public string? Info { get; set; }
        public int UserId { get; set; }
        public UserDTO? User { get; set; }
        public int ServiceId { get; set; }
        public ServiceDTO? Service { get; set; }
        public List<ReviewCriteriaDTO>? Criterias { get; set; }
    }
}

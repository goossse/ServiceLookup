using Microsoft.AspNetCore.Identity;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace ServiceLookup.BL.DTO
{
    public class UserDTO : IdentityUser<int>
    {

        public string? Name { get; set; }

        public string? Surname { get; set; }

        public DateTime DateOfBirth { get; set; }

        public string? ContactDetails { get; set; }

        public double? AverageRate { get; set; }

        public List<RequestDTO>? Requests { get; set; }
        public List<ServiceDTO>? Services { get; set; }
        public List<ReviewDTO>? Reviews { get; set; }
    }
}

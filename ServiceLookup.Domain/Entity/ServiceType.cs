using ServiceLookup.Domain.Entity.Base;
using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace ServiceLookup.Domain.Entity
{
    public class ServiceType : BaseClass
    {
        [StringLength(30, MinimumLength = 2)]
        public string? Name { get; set; }
        public List<Service>? Services { get; set; }
    }
}

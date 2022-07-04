using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace ServiceLookup.Domain.Entity.Base
{
    public class BaseClass
    {
        [Required]
        public int Id { get; set; }

    }
}

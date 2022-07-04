using ServiceLookup.Domain.Entity.Base;
using ServiceLookup.Domain.Validator;
using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace ServiceLookup.Domain.Entity
{
    public class Price : BaseClass
    {
        [Valute]
        public string? Currency { get; set; }

        [Required]
        public int Value { get; set; }

        [Required]
        public int? ServiceId { get; set; }
        public Service? Service { get; set; }
    }
}

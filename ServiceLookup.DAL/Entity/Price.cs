using ServiceLookup.DAL.Entity.Base;
using ServiceLookup.DAL.Validator;
using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace ServiceLookup.DAL.Entity
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

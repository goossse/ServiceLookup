﻿using ServiceLookup.DAL.Entity.Base;
using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace ServiceLookup.DAL.Entity
{
    public class Condition : BaseClass
    {
        [Required]
        [StringLength(2000, MinimumLength = 2)]
        public string? Info { get; set; }

        [Required]
        public int? RequestId { get; set; }
        public Request? Request { get; set; }
    }
}
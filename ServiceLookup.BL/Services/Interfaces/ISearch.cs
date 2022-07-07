﻿using ServiceLookup.BL.DTO;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace ServiceLookup.BL.Services.Interfaces
{
    public interface ISearch
    {
        IEnumerable<ServiceDTO> GetServices();
    }
}
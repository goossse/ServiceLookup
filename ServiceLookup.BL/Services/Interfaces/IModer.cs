using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using ServiceLookup.BL.DTO;

namespace ServiceLookup.BL.Services.Interfaces
{
    public interface IModer
    {
        public bool AddServiceType(ServiceTypeDTO serviceType);
        public void AddCondition(ConditionDTO _condition);

    }
}

using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace ServiceLookup.Domain.Validator
{
    public class ValuteAttribute : ValidationAttribute
    {
        public override bool IsValid(object value)
        {
            if (value != null)
            {
                string Currency = value.ToString();
                if (Currency?.Length == 3 && Currency.ToUpper() == Currency)
                {
                    return true;
                }
            }
            return false;
        }
    }
}

using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace ServiceLookup.BL.DTO.PagedList
{
    public class PagedListDTO<T>
    {
        public IEnumerable<T>? Items { get; set; }
        public int Count { get; set; }
    }
}

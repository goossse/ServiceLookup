using ServiceLookup.BL.DTO;

namespace ServiceLookup.Models.UserVM
{
    public class SearchViewModel
    {
        public IEnumerable<ServiceTypeDTO>? Types { get; set; }
        public int? TypeId { get; set; }
        public string? TextSearch { get; set; }
        public bool IsRatedOnly { get; set; }
        public int? RateStart { get; set; }
        public int? RateEnd { get; set; }
        public string? Order { get; set; }
        //PageView
        public IEnumerable<ServiceDTO> services { get; set; }
        public PageViewModel pageViewModel { get; set; }
    }
}

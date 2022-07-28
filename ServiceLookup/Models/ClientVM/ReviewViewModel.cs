using ServiceLookup.BL.DTO;

namespace ServiceLookup.Models.ClientVM
{
    public class ReviewViewModel
    {
        public int Id { get; set; }
        public List<string>? Criterias { get; set; }
        public List<int>? Rates { get; set; }
        public string? Text { get; set; }
        public int ServiceId { get; set; }
        public ServiceDTO? Service { get; set; }
        public int RequestId { get; set; }
    }
}

using ServiceLookup.BL.DTO;

namespace ServiceLookup.Models.ClientVM
{
    public class ReviewViewModel
    {
        public int Id { get; set; }
        IEnumerable<ReviewCriteriaDTO>? Criterias { get; set; }
        public string? Text { get; set; }
        public int ServiceId { get; set; }
    }
}

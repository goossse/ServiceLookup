using ServiceLookup.BL.DTO;

namespace ServiceLookup.Models.ClientVM
{
    public class MyRequestsViewModel
    {
        public int Id { get; set; }
        public string? Description { get; set; }
        public string DateOfBooking { get; set; }
        public string StartOfBooking { get; set; }
        public string EndOfBooking { get; set; }

        public int? ConditionId { get; set; }
        public int ServiceId { get; set; }
        public string ServiceTitle { get; set; }
        public string? ServiceImage { get; set; }
        public PriceDTO? Price { get; set; }
    }
}

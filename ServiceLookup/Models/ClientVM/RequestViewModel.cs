using ServiceLookup.BL.DTO;

namespace ServiceLookup.Models.ClientVM
{
    public class RequestViewModel
    {
        public int Id { get; set; }
        public string? Description { get; set; }
        public DateTime DateOfBooking { get; set; }
        public DateTime StartOfBooking { get; set; }
        public DateTime EndOfBooking { get; set; }

        public int ServiceId { get; set; }
        public int? PriceId { get; set; }
    }
}

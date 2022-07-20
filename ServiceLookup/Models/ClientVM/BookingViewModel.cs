using ServiceLookup.BL.DTO;

namespace ServiceLookup.Models.ClientVM
{
    public class BookingViewModel
    {
        public ServiceDTO Service { get; set; }
        public PriceDTO? Price { get; set; }
        public RequestViewModel Request { get; set; }
    }
}

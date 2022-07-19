using ServiceLookup.BL.DTO;

namespace ServiceLookup.Models.ClientVM
{
    public class RequestViewModel
    {
        public ServiceDTO Service { get; set; }
        public RequestDTO Request { get; set; }
    }
}

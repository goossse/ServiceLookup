using ServiceLookup.BL.DTO;

namespace ServiceLookup.Models.ManageVM
{
    public class RequestsViewModel
    {
        public IEnumerable<RequestDTO>? Requests { get; set; }
        public PageViewModel? pageViewModel { get; set; }

}
}

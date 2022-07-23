using ServiceLookup.BL.DTO;

namespace ServiceLookup.Models.ManageVM
{
    public class ServiceViewModel
    {
        public int Id { get; set; }
        public string Title { get; set; }
        public string? Info { get; set; }
        public int? UserId { get; set; }
        public string Image { get; set; }
        public IFormFile? ImageFile { get; set; }
        public PriceDTO? Price { get; set; }

    }
}

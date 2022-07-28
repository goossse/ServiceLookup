using ServiceLookup.BL.DTO;
using System.ComponentModel.DataAnnotations;

namespace ServiceLookup.Models.ManageVM
{
    public class CreateServiceViewModel
    {
        public List<ServiceTypeDTO>? Types { get; set; }
        public int TypeId { get; set; }
        public int Id { get; set; }
        [Required]
        [StringLength(30, MinimumLength = 2)]
        public string? Title { get; set; }
        [Required]
        [StringLength(2000, MinimumLength = 2)]
        public string? Info { get; set; }
        public int? UserId { get; set; }
        public string? Image { get; set; }
        public IFormFile? ImageFile { get; set; }
        public PriceDTO? Price { get; set; }
    }
}

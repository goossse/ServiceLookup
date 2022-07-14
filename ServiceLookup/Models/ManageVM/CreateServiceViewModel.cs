using ServiceLookup.BL.DTO;

namespace ServiceLookup.Models.ManageVM
{
    public class CreateServiceViewModel
    {
        public int Id { get; set; }
        public string? Title { get; set; }
        public string? Info { get; set; }
        public int? UserId { get; set; }
        public IFormFile? Image { get; set; }

        //добавить цену!

    }
}

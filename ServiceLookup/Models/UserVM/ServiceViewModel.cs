using System.ComponentModel.DataAnnotations;

namespace ServiceLookup.Models.UserVM
{
    public class ServiceViewModel
    {
        public int Id { get; set; }
        [Required]
        [StringLength(30, MinimumLength = 2)]
        public string? Title { get; set; }

        [Required]
        [StringLength(2000, MinimumLength = 2)]
        public string? Info { get; set; }
        public string? Image { get; set; }
        public double? AverageRate { get; set; }
        public DateTime DateOfCreating { get; set; }
        public int? UserId { get; set; }
    }
}

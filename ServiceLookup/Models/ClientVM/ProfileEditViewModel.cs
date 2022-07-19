namespace ServiceLookup.Models.ClientVM
{
    public class ProfileEditViewModel
    {
        public string? Name { get; set; }
        public string? Surname { get; set; }
        public string? ShortDescription { get; set; }
        public DateOnly DateOfBirth { get; set; }
        public string? ContactDetails { get; set; }
        public IFormFile Image { get; set; }
    }
}

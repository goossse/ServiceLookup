namespace ServiceLookup.Models.ClientVM
{
    public class ProfileViewModel
    {
        public int Id { get; set; }
        public string UserName { get; set; }
        public string? Name { get; set; }
        public string? Surname { get; set; }
        public DateTime DateOfBirth { get; set; }
        public string? ContactDetails { get; set; }
        public double? AverageRate { get; set; }
        public string? Image { get; set; }
        public DateTime DateOfRegistration { get; set; }
        public string? ShortDescription { get; set; }

    }
}

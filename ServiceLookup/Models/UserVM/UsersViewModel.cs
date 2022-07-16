namespace ServiceLookup.Models.UserVM
{
    public class UsersViewModel
    {
        public int Id { get; set; }
        public string Name { get; set; }
        public string Surname { get; set; }
        public string? Image { get; set; }
        public double? AverageRate { get; set; }
        public string ShortDescription { get; set; }

    }
}

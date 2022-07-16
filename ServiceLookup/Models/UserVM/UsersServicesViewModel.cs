using ServiceLookup.BL.DTO;

namespace ServiceLookup.Models.UserVM
{
    public class UsersServicesViewModel
    {
        public List<ServiceDTO> Services { get; set; }
        public UserDTO User { get; set; }
    }
}

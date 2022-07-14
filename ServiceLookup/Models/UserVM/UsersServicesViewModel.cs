using ServiceLookup.BL.DTO;

namespace ServiceLookup.Models.UserVM
{
    public class UsersServicesViewModel
    {
        List<ServiceDTO> Services { get; set; }
        UserDTO User { get; set; }
    }
}

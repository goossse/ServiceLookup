using AutoMapper;
using ServiceLookup.BL.DTO;
using ServiceLookup.Models.ManageVM;
using ServiceLookup.Models.UserVM;

namespace ServiceLookup.Mapper
{
    public class AutoMapperView : Profile
    {
        public AutoMapperView()
        {
            /*CreateMap<CreateServiceViewModel, ServiceDTO>();*/
            CreateMap<UsersViewModel, UserDTO>();
        }
    }
}

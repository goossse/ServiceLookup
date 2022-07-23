using AutoMapper;
using ServiceLookup.BL.DTO;
using ServiceLookup.Models.ClientVM;
using ServiceLookup.Models.ManageVM;
using ServiceLookup.Models.UserVM;

namespace ServiceLookup.Mapper
{
    public class AutoMapperView : Profile
    {
        public AutoMapperView()
        {
            /*CreateMap<CreateServiceViewModel, ServiceDTO>();*/
            CreateMap<UserDTO, UsersViewModel>();
            CreateMap<ProfileViewModel, UserDTO>().ReverseMap();
            CreateMap<ServiceDTO, Models.UserVM.ServiceViewModel>().ReverseMap();
        }
    }
}

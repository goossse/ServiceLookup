using AutoMapper;
using ServiceLookup.BL.DTO;
using ServiceLookup.DAL.Entity;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace ServiceLookup.BL.Mapper
{
    public class AutoMapperData : Profile
    {
        public AutoMapperData()
        {
            CreateMap<Condition, ConditionDTO>().ReverseMap();
            CreateMap<Price, PriceDTO>().ReverseMap();
            CreateMap<Request, RequestDTO>().ReverseMap();
            CreateMap<ReviewCriteria, ReviewCriteriaDTO>().ReverseMap();
            CreateMap<Review, ReviewDTO>().ReverseMap();
            CreateMap<Service, ServiceDTO>().ReverseMap();
            CreateMap<ServiceType, ServiceTypeDTO>().ReverseMap();
            CreateMap<User, UserDTO>().ReverseMap(); // ne nado budet
            // servisy dlya vseh krome identity N
        }
    }
}

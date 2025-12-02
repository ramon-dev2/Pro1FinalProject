using AutoMapper;
using BusinessLogic.Dtos;
using DataAccess.Entities;

namespace BusinessLogic.Profiles
{
    public class StatusProfile : Profile
    {
        public StatusProfile()
        {
            CreateMap<StatusDto, Status>().ReverseMap();
            CreateMap<StatusCreateDto, Status>()
                .ForMember(dest => dest.Id, opt => opt.Ignore());
            CreateMap<StatusCreateDto, StatusDto>();
        }
    }
}


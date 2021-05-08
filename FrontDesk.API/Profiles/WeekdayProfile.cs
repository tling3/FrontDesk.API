using AutoMapper;
using FrontDesk.API.Models.Domain;
using FrontDesk.API.Models.DTOs;

namespace FrontDesk.API.Profiles
{
    public class WeekdayProfile : Profile
    {
        public WeekdayProfile()
        {
            CreateMap<WeekdayModel, WeekdayReadDto>();
            CreateMap<WeekdayInsertDto, WeekdayModel>();
            CreateMap<WeekdayModel, WeekdayUpdateDto>();
        }
    }
}

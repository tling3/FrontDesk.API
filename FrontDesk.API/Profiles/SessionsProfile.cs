using AutoMapper;
using FrontDesk.API.Models.Domain;
using FrontDesk.API.Models.DTOs;

namespace FrontDesk.API.Profiles
{
    public class SessionsProfile : Profile
    {
        public SessionsProfile()
        {
            CreateMap<SessionModel, SessionReadDto>();
            CreateMap<SessionInsertDto, SessionModel>();
            CreateMap<SessionModel, SessionUpdateDto>();
            CreateMap<SessionUpdateDto, SessionModel>();
            CreateMap<SessionReadDto, SessionModel>();
            CreateMap<SessionModel, SessionModel>();
        }
    }
}

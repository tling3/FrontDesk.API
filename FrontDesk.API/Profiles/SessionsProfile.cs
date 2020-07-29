using AutoMapper;
using FrontDesk.API.Models.Domain;
using FrontDesk.API.Models.DTOs;

namespace FrontDesk.API.Profiles
{
    public class SessionsProfile : Profile
    {
        public SessionsProfile()
        {
            CreateMap<Session, SessionReadDto>();
            CreateMap<SessionInsertDto, Session>();
            CreateMap<Session, SessionUpdateDto>();
        }
    }
}

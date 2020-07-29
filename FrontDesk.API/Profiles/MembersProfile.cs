using AutoMapper;
using FrontDesk.API.Models.Domain;
using FrontDesk.API.Models.DTOs;

namespace FrontDesk.API.Profiles
{
    public class MembersProfile : Profile
    {
        public MembersProfile()
        {
            CreateMap<Member, MemberReadDto>();
            CreateMap<MemberInsertDto, Member>();
            CreateMap<Member, MemberUpdateDto>();
        }
    }
}

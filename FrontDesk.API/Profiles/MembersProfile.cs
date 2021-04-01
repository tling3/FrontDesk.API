using AutoMapper;
using FrontDesk.API.Models.Domain;
using FrontDesk.API.Models.DTOs;

namespace FrontDesk.API.Profiles
{
    public class MembersProfile : Profile
    {
        public MembersProfile()
        {
            CreateMap<MemberModel, MemberReadDto>();
            CreateMap<MemberInsertDto, MemberModel>();
            CreateMap<MemberModel, MemberUpdateDto>();
        }
    }
}

using AutoMapper;
using FrontDesk.API.Models.Domain;
using FrontDesk.API.Models.DTOs;

namespace FrontDesk.API.Profiles
{
    public class MembershipTypeProfile : Profile
    {
        public MembershipTypeProfile()
        {
            CreateMap<MembershipTypeModel, MembershipTypeReadDto>();
            CreateMap<MembershipTypeInsertDto, MembershipTypeModel>();
            CreateMap<MembershipTypeModel, MembershipTypeUpdateDto>();
        }
    }
}

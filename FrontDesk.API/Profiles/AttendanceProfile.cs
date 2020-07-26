using AutoMapper;
using FrontDesk.API.Models.Domain;
using FrontDesk.API.Models.DTOs;

namespace FrontDesk.API.Profiles
{
    public class AttendanceProfile : Profile
    {
        public AttendanceProfile()
        {
            CreateMap<Attendance, AttendanceReadDto>();
            CreateMap<AttendanceInsertDto, Attendance>();
            CreateMap<Attendance, AttendanceUpdateDto>();
        }
    }
}

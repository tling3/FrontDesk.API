﻿using AutoMapper;
using FrontDesk.API.Models.Domain;
using FrontDesk.API.Models.DTOs;

namespace FrontDesk.API.Profiles
{
    public class AttendanceProfile : Profile
    {
        public AttendanceProfile()
        {
            CreateMap<AttendanceModel, AttendanceReadDto>();
            CreateMap<AttendanceInsertDto, AttendanceModel>();
            CreateMap<AttendanceModel, AttendanceUpdateDto>();
        }
    }
}

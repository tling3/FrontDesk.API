﻿using AutoMapper;
using FrontDesk.API.Data.Interfaces;
using FrontDesk.API.Models.Domain;
using FrontDesk.API.Models.DTOs;
using Microsoft.AspNetCore.Mvc;
using System.Collections.Generic;
using System.Threading.Tasks;

namespace FrontDesk.API.Controllers
{
    [Route("api/attendance")]
    [ApiController]

    public class AttendanceController : ControllerBase
    {
        private readonly IAttendanceRepo _repository;
        private readonly IMapper _mapper;

        public AttendanceController(
            IAttendanceRepo repository,
            IMapper mapper)
        {
            _repository = repository;
            _mapper = mapper;
        }

        [HttpGet]
        public async Task<ActionResult<IEnumerable<AttendanceReadDto>>> GetAllAttendance()
        {
            var attendanceModels = await _repository.GetAllAttendance();
            return Ok(_mapper.Map<IEnumerable<AttendanceReadDto>>(attendanceModels));
        }

        [HttpGet("memberid/{id}")]
        public async Task<ActionResult<IEnumerable<AttendanceReadDto>>> GetAttendanceByMemberId(int id)
        {
            var attendanceModels = await _repository.GetAttendanceByMemberId(id);
            if (attendanceModels.Count <= 0)
            {
                return NotFound();
            }
            return Ok(_mapper.Map<IEnumerable<AttendanceReadDto>>(attendanceModels));
        }

        [HttpGet("{id}", Name = "GetAttendanceById")]
        public async Task<ActionResult<AttendanceReadDto>> GetAttendanceById(int id)
        {
            var attendanceModel = await _repository.GetAttendanceById(id);
            if (attendanceModel == null)
            {
                return NotFound();
            }
            return Ok(_mapper.Map<AttendanceReadDto>(attendanceModel));
        }

        [HttpPost]
        public async Task<ActionResult<AttendanceReadDto>> InsertAttendance(AttendanceInsertDto attendanceInsertDto)
        {
            //  TODO: add validation of dto model
            var attendanceModel = _mapper.Map<Attendance>(attendanceInsertDto);
            await _repository.InsertAttendance(attendanceModel);
            _repository.SaveChanges();

            var attendanceReadDto = _mapper.Map<AttendanceReadDto>(attendanceModel);
            return CreatedAtRoute(nameof(GetAttendanceById), new { Id = attendanceReadDto.Id }, attendanceReadDto);
        }
    }
}

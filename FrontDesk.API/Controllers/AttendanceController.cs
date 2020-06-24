using AutoMapper;
using FrontDesk.API.Data.Interfaces;
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
            var attendanceItems = await _repository.GetAllAttendance();
            return Ok(_mapper.Map<IEnumerable<AttendanceReadDto>>(attendanceItems));
        }

        [HttpGet("memberid/{id}")]
        public async Task<ActionResult<IEnumerable<AttendanceReadDto>>> GetAttendanceByMemberId(int id)
        {
            var attendanceItems = await _repository.GetAttendanceByMemberId(id);
            if (attendanceItems.Count <= 0)
            {
                return NotFound();
            }
            return Ok(_mapper.Map<IEnumerable<AttendanceReadDto>>(attendanceItems));
        }
    }
}

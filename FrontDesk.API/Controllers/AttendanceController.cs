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

        [HttpGet("{id}")]
        public async Task<ActionResult<AttendanceReadDto>> GetAttendanceById(int id)
        {
            var attendanceItem = await _repository.GetAttendanceById(id);
            if (attendanceItem == null)
            {
                return NotFound();
            }
            return Ok(_mapper.Map<AttendanceReadDto>(attendanceItem));
        }
    }
}

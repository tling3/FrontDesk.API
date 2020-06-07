using FrontDesk.API.Data.Interfaces;
using FrontDesk.API.Models.Domain;
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

        public AttendanceController(IAttendanceRepo repository)
        {
            _repository = repository;
        }

        public async Task<ActionResult<List<Attendance>>> GetAllAttendance()
        {
            var attendanceItems = await _repository.GetAllAttendance();
            return Ok(attendanceItems);
        }
    }
}

using AutoMapper;
using FrontDesk.API.Data.Interfaces;
using FrontDesk.API.Models.Custom.Attendance;
using FrontDesk.API.Models.Domain;
using FrontDesk.API.Models.DTOs;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.JsonPatch;
using Microsoft.AspNetCore.Mvc;
using System;
using System.Collections.Generic;
using System.Threading.Tasks;

namespace FrontDesk.API.Controllers
{
    [Route("api/attendance")]
    [Produces("application/json")]
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

        /// <summary>
        /// Gets all Attendance items
        /// </summary>
        /// <returns>All Attendance items</returns>
        /// <response code="404">Items(s) not found</response>
        /// <response code="200">Attendance items successfully found</response>
        //  GET ALL: api/attendance
        [HttpGet]
        public async Task<ActionResult<IEnumerable<AttendanceReadDto>>> GetAllAttendanceAsync()
        {

            IEnumerable<AttendanceModel> attendanceModels = await _repository.GetAllAttendanceAsync();
            if (attendanceModels == null)
                return NotFound();

            return Ok(_mapper.Map<IEnumerable<AttendanceReadDto>>(attendanceModels));
        }

        /// <summary>
        /// Gets all Attendance items by Member Id
        /// </summary>
        /// <param name="id"></param>
        /// <returns>All Attendance items by Member Id</returns>
        /// <response code="404">Item(s) not found</response>
        /// <response code="200">Attendance items successfully found</response>
        //  GET ALL: api/attendance/memberid/{id}
        [HttpGet("memberid/{id}")]
        public async Task<ActionResult<IEnumerable<AttendanceReadDto>>> GetAttendanceByMemberIdAsync(int id)
        {
            IEnumerable<AttendanceModel> attendanceModels = await _repository.GetAttendanceByMemberIdAsync(id);
            if (attendanceModels == null)
                return NotFound();

            return Ok(_mapper.Map<IEnumerable<AttendanceReadDto>>(attendanceModels));
        }

        /// <summary>
        /// Get Attendance item by Session Id
        /// </summary>
        /// <param name="sessionId"></param>
        /// <returns>Attendance item</returns>
        /// <response code="404">Item not found</response>
        /// <response code="200">Attendance item successfully found</response>
        //  GET BY ID: api/attendance/{sessionId}
        [HttpGet("{sessionId}", Name = nameof(GetAttendanceBySessionIdAsync))]
        public async Task<ActionResult<AttendanceReadDto>> GetAttendanceBySessionIdAsync(int sessionId)
        {
            AttendanceModel attendanceModel = await _repository.GetAttendanceBySessionIdAsync(sessionId);
            if (attendanceModel == null)
                return NotFound();

            return Ok(_mapper.Map<AttendanceReadDto>(attendanceModel));
        }

        /// <summary>
        /// Get Attendance per Session
        /// </summary>
        /// <param name="sessionId"></param>
        /// <param name="date"></param>
        /// <returns>List of Attendance Items</returns>
        /// <response code="400">Item(s) not found</response>
        /// <response code="200">Attendance item(s) successfully found</response>
        // GET: api/attendance/session/{sessionId}/{memberId}/{date}
        [HttpGet("/api/attendance/session/{sessionId}/{date}")]
        public async Task<ActionResult<List<AttendancePerSessionDto>>> GetAttendancePerSessionAsync(int sessionId, DateTime date)
        {
            List<AttendancePerSessionDto> attendanceModels = await _repository.GetAttendancePerSessionAsync(sessionId, date);
            if (attendanceModels == null)
                return NotFound();

            return Ok(attendanceModels);
        }

        /// <summary>
        /// Insert Attendance item
        /// </summary>
        /// <param name="attendanceInsertDto"></param>
        /// <returns>Attendance item</returns>
        /// <response code="400">Item to be inserted is not valid</response>
        /// <response code="500">Item failed to be inserted</response>
        /// <response code="201">Attendance item  was successfully inserted</response>
        //  INSERT: api/attendance
        [HttpPost]
        [ProducesResponseType(StatusCodes.Status204NoContent)]
        public async Task<ActionResult<AttendanceReadDto>> InsertAttendanceAsync(AttendanceInsertDto attendanceInsertDto)
        {
            if (!ModelState.IsValid)
                return BadRequest();

            AttendanceModel attendanceModel = _mapper.Map<AttendanceModel>(attendanceInsertDto);
            bool isSuccessful = await _repository.InsertAttendanceAsync(attendanceModel);
            if (!isSuccessful)
                return StatusCode(StatusCodes.Status500InternalServerError);

            AttendanceReadDto attendanceReadDto = _mapper.Map<AttendanceReadDto>(attendanceModel);
            return CreatedAtRoute(nameof(GetAttendanceBySessionIdAsync), new { sessionId = attendanceReadDto.SessionId }, attendanceReadDto);
        }

        /// <summary>
        /// Update Attendance item
        /// </summary>
        /// <param name="attendanceUpdateDto"></param>
        /// <returns></returns>
        /// <response code="400">Updated item is not valid</response>
        /// <response code="404">Item to be updated not found</response>
        /// <response code="500">Item failed to be updated</response>
        /// <response code="204">Attendance item was successfully updated</response>
        //  INSERT: api/attendance/{sessionId}
        [HttpPut("{sessionId}")]
        [ProducesResponseType(StatusCodes.Status204NoContent)]
        public async Task<IActionResult> UpdateAttendanceAsync(AttendanceUpdateDto attendanceUpdateDto)
        {
            if (!ModelState.IsValid)
                return BadRequest();

            AttendanceModel attendanceModel = await _repository.GetAttendanceBySessionIdAsync(attendanceUpdateDto.SessionId);
            if (attendanceModel == null)
                return NotFound();

            _mapper.Map(attendanceUpdateDto, attendanceModel);
            _repository.UpdateAttendance(attendanceModel);

            bool isSuccessful = _repository.SaveChanges();
            if (!isSuccessful)
                return StatusCode(StatusCodes.Status500InternalServerError);

            return NoContent();
        }

        /// <summary>
        /// Patches Attendance item
        /// </summary>
        /// <param name="sessionId"></param>
        /// <param name="patchDocument"></param>
        /// <returns></returns>
        /// <response code="404">Item to be patched not found</response>
        /// <response code="400">Item failed validation after applying patch</response>
        /// <response code="500">Item failed to be patched</response>
        /// <response code="204">Attendance item was successfully patched</response>
        //  PATCH: api/attendance/{sessionId}
        [HttpPatch("{sessionId}")]
        [ProducesResponseType(StatusCodes.Status204NoContent)]
        public async Task<IActionResult> PatchAttendanceAsync(int sessionId, JsonPatchDocument<AttendanceUpdateDto> patchDocument)
        {
            AttendanceModel attendanceModel = await _repository.GetAttendanceBySessionIdAsync(sessionId);
            if (attendanceModel == null)
                return NotFound();

            var attendanceToPatch = _mapper.Map<AttendanceUpdateDto>(attendanceModel);

            patchDocument.ApplyTo(attendanceToPatch);
            if (!TryValidateModel(attendanceToPatch))
                return ValidationProblem();

            _mapper.Map(attendanceToPatch, attendanceModel);
            _repository.UpdateAttendance(attendanceModel);

            bool isSuccessful = _repository.SaveChanges();
            if (!isSuccessful)
                return StatusCode(StatusCodes.Status500InternalServerError);

            return NoContent();
        }

        /// <summary>
        /// Deletes Attendance item by Session Id
        /// </summary>
        /// <param name="sessionId"></param>
        /// <returns></returns>
        /// <response code="404">Item to be deleted is not found</response>
        /// <response code="500">Item failed to be deleted</response>
        /// <response code="204">Attendance item was successfully deleted</response>
        //  DELETE: api/attendance/{sessionId}
        [HttpDelete("{sessionId}")]
        [ProducesResponseType(StatusCodes.Status204NoContent)]
        public async Task<IActionResult> DeleteAttendanceByIdAsync(int sessionId)
        {
            AttendanceModel domainModel = await _repository.GetAttendanceBySessionIdAsync(sessionId);
            if (domainModel == null)
                return NotFound();

            bool isSuccessful = _repository.DeleteAttendanceById(domainModel);
            if (!isSuccessful)
                return StatusCode(StatusCodes.Status500InternalServerError);

            return NoContent();
        }
    }
}

using AutoMapper;
using FrontDesk.API.Data.Interfaces;
using FrontDesk.API.Models.Domain;
using FrontDesk.API.Models.DTOs;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.JsonPatch;
using Microsoft.AspNetCore.Mvc;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
#pragma warning disable 1591

namespace FrontDesk.API.Controllers
{
    [Route("api/session")]
    [ApiController]
    public class SessionsController : ControllerBase
    {
        private readonly ISessionRepo _repository;
        private readonly IMapper _mapper;
        public SessionsController(
            ISessionRepo repository,
            IMapper mapper)

        {
            _repository = repository;
            _mapper = mapper;
        }

        /// <summary>
        /// Gets all Sessions for today that have not ended
        /// </summary>
        /// <returns>All Sessions for today that have not ended</returns>
        /// <response code="404">Items not found</response>
        /// <response code="200">Sessions items successfully found</response>
        //  GET ALL: api/session
        [HttpGet]
        public async Task<ActionResult<IEnumerable<SessionReadDto>>> GetRemainingSessionsAsync()
        {
            IEnumerable<SessionModel> domainModels = await _repository.GetAllSessionsAsync();
            if (domainModels == null)
                return NotFound();

            IEnumerable<SessionReadDto> readDtos = GetWeekdayCustomMapper().Map<IEnumerable<SessionReadDto>>(domainModels);

            TimeSpan currentTime = DateTime.Now.TimeOfDay;
            string currentDay = DateTime.Now.DayOfWeek.ToString();
            readDtos = readDtos.Where(dto => dto.Weekday == currentDay && dto.EndTime.TimeOfDay >= currentTime).ToList();

            return Ok(readDtos);
        }

        /// <summary>
        /// Get Session by id
        /// </summary>
        /// <param name="id"></param>
        /// <returns>Session item</returns>
        /// <response code="404">Item not found</response>
        /// <response code="200">Sessions item successfully found</response>
        //  GET BY ID: api/session/{id}
        [HttpGet("{id}", Name = nameof(GetSessionByIdAsync))]
        public async Task<ActionResult<SessionReadDto>> GetSessionByIdAsync(int id)
        {
            SessionModel domainModel = await _repository.GetSessionByIdAsync(id);
            if (domainModel == null)
                return NotFound();

            return Ok(GetWeekdayCustomMapper().Map<SessionReadDto>(domainModel));
        }

        /// <summary>
        /// Inserts Sessions item
        /// </summary>
        /// <param name="insertDto"></param>
        /// <returns>Sessions item</returns>
        /// <response code="400">Item to be inserted is not valid</response>
        /// <response code="500">Item failed to be inserted</response>
        /// <response code="201">Sessions item was successfully inserted</response>
        //  INSERT: api/session
        [HttpPost]
        [ProducesResponseType(StatusCodes.Status201Created)]
        public async Task<ActionResult<SessionReadDto>> InsertSessionAsync(SessionInsertDto insertDto)
        {
            if (!ModelState.IsValid)
                return BadRequest();

            SessionModel domainModel = _mapper.Map<SessionModel>(insertDto);
            domainModel.WeekdayId = (int)(Weekdays)Enum.Parse(typeof(Weekdays), insertDto.Weekday, true);

            bool isSuccessful = await _repository.InsertSessionAsync(domainModel);
            if (!isSuccessful)
                return StatusCode(StatusCodes.Status500InternalServerError);

            SessionReadDto readDto = GetWeekdayCustomMapper().Map<SessionReadDto>(domainModel);

            return CreatedAtRoute(nameof(GetSessionByIdAsync), new { id = readDto.Id }, readDto);
        }

        /// <summary>
        /// Updates Sessions item
        /// </summary>
        /// <param name="updateDto"></param>
        /// <returns></returns>
        /// <response code="400">Updated item is not valid</response>
        /// <response code="404">Item to be updated is not valid</response>
        /// <response code="500">Item failed to be updated</response>
        /// <response code="204">Sessions item was successfully updated</response>
        //  UPDATE: api/session
        [HttpPut]
        [ProducesResponseType(StatusCodes.Status204NoContent)]
        public async Task<IActionResult> UpdateSessionAsync(SessionUpdateDto updateDto)
        {
            if (!ModelState.IsValid)
                return BadRequest();

            SessionModel domainModel = await _repository.GetSessionByIdAsync(updateDto.Id);
            if (domainModel == null)
                return NotFound();

            _mapper.Map(updateDto, domainModel);
            domainModel.WeekdayId = (int)(Weekdays)Enum.Parse(typeof(Weekdays), updateDto.Weekday, true);
            _repository.UpdateSession(domainModel);

            bool isSuccessful = _repository.SaveChanges();
            if (!isSuccessful)
                return StatusCode(StatusCodes.Status500InternalServerError);

            return NoContent();
        }

        /// <summary>
        /// Patches Session item
        /// </summary>
        /// <param name="id"></param>
        /// <param name="patchDocument"></param>
        /// <returns></returns>
        /// <response code="404">Item to be patched not found</response>
        /// <response code="400">Item failed validation after applying the patch</response>
        /// <response code="500">Item failed to be patched</response>
        /// <response code="204">Sessions item was successfully patched</response>
        //  PATCH: api/session/{id}
        [HttpPatch("{id}")]
        [ProducesResponseType(StatusCodes.Status204NoContent)]
        public async Task<IActionResult> PatchSessionAsync(int id, JsonPatchDocument<SessionUpdateDto> patchDocument)
        {
            SessionModel sessionModel = await _repository.GetSessionByIdAsync(id);
            if (sessionModel == null)
                return NotFound();

            MapperConfiguration config = new MapperConfiguration(cfg => cfg.CreateMap<SessionModel, SessionUpdateDto>()
                .ForMember(dto => dto.Weekday, opt => opt.MapFrom(src => Enum.GetName(typeof(Weekdays), src.WeekdayId))));
            IMapper customMapper = new Mapper(config);
            SessionUpdateDto sessionToPatch = customMapper.Map<SessionUpdateDto>(sessionModel);

            patchDocument.ApplyTo(sessionToPatch);
            if (!TryValidateModel(sessionToPatch))
                return ValidationProblem();

            _mapper.Map(sessionToPatch, sessionModel);
            sessionModel.WeekdayId = (int)(Weekdays)Enum.Parse(typeof(Weekdays), sessionToPatch.Weekday, true);

            _repository.UpdateSession(sessionModel);
            bool isSuccessful = _repository.SaveChanges();
            if (!isSuccessful)
                return StatusCode(StatusCodes.Status500InternalServerError);

            return NoContent();
        }

        /// <summary>
        /// Delete Session item
        /// </summary>
        /// <param name="id"></param>
        /// <returns></returns>
        /// <response code="400">Item to be deleted not found</response>
        /// <response code="500">Item failed to be deleted</response>
        /// <response code="204">Session item was successfully deleted</response>
        //  DELETE: api/session/{id}
        [HttpDelete("{id}")]
        [ProducesResponseType(StatusCodes.Status204NoContent)]
        public async Task<IActionResult> DeleteSessionAsync(int id)
        {
            SessionModel domainModel = await _repository.GetSessionByIdAsync(id);
            if (domainModel == null)
                return BadRequest();

            bool isSuccessful = _repository.DeleteSession(domainModel);
            if (!isSuccessful)
                return StatusCode(StatusCodes.Status500InternalServerError);

            return NoContent();
        }

        private Mapper GetWeekdayCustomMapper()
        {
            MapperConfiguration config = new MapperConfiguration(cfg => cfg.CreateMap<SessionModel, SessionReadDto>()
                .ForMember(dto => dto.Weekday, opt => opt.MapFrom(src => Enum.GetName(typeof(Weekdays), src.WeekdayId))));
            return new Mapper(config);
        }
    }

    public enum Weekdays
    {
        Monday = 1,
        Tuesday = 2,
        Wednesday = 3,
        Thursday = 4,
        Friday = 5,
        Saturday = 6,
        Sunday = 7
    }
}
#pragma warning restore 1591

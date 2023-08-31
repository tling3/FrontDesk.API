using AutoMapper;
using FrontDesk.API.Data.Interfaces;
using FrontDesk.API.Models.Domain;
using FrontDesk.API.Models.DTOs;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.JsonPatch;
using Microsoft.AspNetCore.Mvc;
using System.Collections.Generic;
using System.Threading.Tasks;

namespace FrontDesk.API.Controllers
{
    [Route("api/weekday")]
    [ApiController]
    public class WeekdayController : ControllerBase
    {
        private readonly IWeekdayRepo _repository;
        private readonly IMapper _mapper;

        public WeekdayController(
            IWeekdayRepo repository,
            IMapper mapper)
        {
            _repository = repository;
            _mapper = mapper;
        }


        /// <summary>
        /// Get all Weekday items
        /// </summary>
        /// <returns>All Weekday items</returns>
        /// <response code="404">Item(s) not found</response>
        /// <response code="200">Weekday items successfully found</response>
        //  GET ALL: api/weekday
        [HttpGet]
        public async Task<ActionResult<IEnumerable<WeekdayReadDto>>> GetAllWeekdaysAsync()
        {
            IEnumerable<WeekdayModel> domainModels = await _repository.GetAllWeekdaysAsync();
            if (domainModels == null)
                return NotFound();

            return Ok(_mapper.Map<IEnumerable<WeekdayReadDto>>(domainModels));
        }

        /// <summary>
        /// Get Weekday by id
        /// </summary>
        /// <param name="id"></param>
        /// <returns>Weekday item</returns>
        /// <response code="404">Item not found</response>
        /// <response code="200">Weekday item successfully found </response>
        //  GET BY ID: api/weekday/{id}
        [HttpGet("{id}", Name = nameof(GetWeekdayByIdAsync))]
        public async Task<ActionResult<WeekdayReadDto>> GetWeekdayByIdAsync(int id)
        {
            WeekdayModel weekdayModel = await _repository.GetWeekdayByIdAsync(id);
            if (weekdayModel == null)
                return NotFound();

            return Ok(_mapper.Map<WeekdayReadDto>(weekdayModel));
        }

        /// <summary>
        /// Insert Weekday item
        /// </summary>
        /// <param name="insertDto"></param>
        /// <returns>Weekday item</returns>
        /// <response code="400">Item to be inserted is not valid</response>
        /// <response code="500">Item failed to be inserted</response>
        /// <response code="201">Weekday item was successfully inserted</response>
        //  INSERT: api/weekday
        [HttpPost]
        [ProducesResponseType(StatusCodes.Status201Created)]
        public async Task<ActionResult<WeekdayReadDto>> InsertWeekdayAsync(WeekdayInsertDto insertDto)
        {
            if (!ModelState.IsValid)
                return BadRequest();

            WeekdayModel domainModel = _mapper.Map<WeekdayModel>(insertDto);
            bool isSuccessful = await _repository.InsertWeekdayAsync(domainModel);
            if (!isSuccessful)
                return StatusCode(StatusCodes.Status500InternalServerError);

            WeekdayReadDto readDto = _mapper.Map<WeekdayReadDto>(domainModel);

            return CreatedAtRoute(nameof(GetWeekdayByIdAsync), new { id = readDto.Id }, readDto);
        }

        /// <summary>
        /// Update Weekday item
        /// </summary>
        /// <param name="id"></param>
        /// <param name="updateDto"></param>
        /// <returns></returns>
        /// <response code="400">Updated item is not valid</response>
        /// <response code="404">Item to be updated not found</response>
        /// <response code="500">Item failed to be updated</response>
        /// <response code="204">Weekday item was successfully updated</response>
        //  UPDATE: api/weekday/{id}
        [HttpPut]
        [ProducesResponseType(StatusCodes.Status204NoContent)]
        public async Task<IActionResult> UpdateWeekdayAsync(WeekdayUpdateDto updateDto)
        {
            if (!ModelState.IsValid)
                return BadRequest();

            WeekdayModel domainModel = await _repository.GetWeekdayByIdAsync(updateDto.Id);
            if (domainModel == null)
                return NotFound();

            _mapper.Map(updateDto, domainModel);
            _repository.UpdateWeekday(domainModel);
            bool isSuccessful = _repository.SaveChanges();
            if (!isSuccessful)
                return StatusCode(StatusCodes.Status500InternalServerError);

            return NoContent();
        }

        /// <summary>
        /// Patch weekday item
        /// </summary>
        /// <param name="id"></param>
        /// <param name="patchDocument"></param>
        /// <returns></returns>
        /// <response code="404">Item to be patched not found</response>
        /// <response code="400">Item failed validation after applying patch</response>
        /// <response code="500">Item failed to be patched</response>
        /// <response code="204">Weekday item was successfully patched</response>
        //  PATCH: api/weekday/{id}
        [HttpPatch("{id}")]
        [ProducesResponseType(StatusCodes.Status204NoContent)]
        public async Task<IActionResult> PatchWeekdayAsync(int id, JsonPatchDocument<WeekdayUpdateDto> patchDocument)
        {
            WeekdayModel weekdayModel = await _repository.GetWeekdayByIdAsync(id);
            if (weekdayModel == null)
                return NotFound();

            WeekdayUpdateDto weekdayToPatch = _mapper.Map<WeekdayUpdateDto>(weekdayModel);

            patchDocument.ApplyTo(weekdayToPatch);
            if (!TryValidateModel(weekdayToPatch))
                return ValidationProblem();

            _mapper.Map(weekdayToPatch, weekdayModel);
            _repository.UpdateWeekday(weekdayModel);
            bool isSuccessful = _repository.SaveChanges();
            if (!isSuccessful)
                return StatusCode(StatusCodes.Status500InternalServerError);

            return NoContent();
        }

        /// <summary>
        /// Deletes Weeday item
        /// </summary>
        /// <param name="id"></param>
        /// <returns></returns>
        /// <response code="404">Item to be deleted not found</response>
        /// <response code="500">Item failed to be deleted</response>
        /// <response code="204">Weekday item was successfully deleted</response>
        [HttpDelete("{id}")]
        [ProducesResponseType(StatusCodes.Status204NoContent)]
        public async Task<IActionResult> DeleteWeekdayAsync(int id)
        {
            WeekdayModel domainModel = await _repository.GetWeekdayByIdAsync(id);
            if (domainModel == null)
                return NotFound();

            bool isSuccessful = _repository.DeleteWeekday(domainModel);
            if (!isSuccessful)
                return StatusCode(StatusCodes.Status500InternalServerError);

            return NoContent();
        }
    }
}

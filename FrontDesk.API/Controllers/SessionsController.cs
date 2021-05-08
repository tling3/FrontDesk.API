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
        /// Gets all Session items
        /// </summary>
        /// <returns>All Session items</returns>
        /// <response code="404">Items not found</response>
        /// <response code="200">Sessions items successfully found</response>
        //  GET ALL: api/session
        [HttpGet]
        public async Task<ActionResult<IEnumerable<SessionReadDto>>> GetAllSessions()
        {
            IEnumerable<SessionModel> domainModels = await _repository.GetAllSessions();
            if (domainModels == null)
                return NotFound();

            return Ok(_mapper.Map<IEnumerable<SessionReadDto>>(domainModels));
        }

        /// <summary>
        /// Get Session by id
        /// </summary>
        /// <param name="id"></param>
        /// <returns>Session item</returns>
        /// <response code="404">Item not found</response>
        /// <response code="200">Sessions item successfully found</response>
        //  GET BY ID: api/session/{id}
        [HttpGet("{id}", Name = nameof(GetSessionById))]
        public async Task<ActionResult<SessionReadDto>> GetSessionById(int id)
        {
            SessionModel domainModel = await _repository.GetSessionById(id);
            if (domainModel == null)
                return NotFound();

            return Ok(_mapper.Map<SessionReadDto>(domainModel));
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
        public async Task<ActionResult<SessionReadDto>> InsertSession(SessionInsertDto insertDto)
        {
            if (!ModelState.IsValid)
                return BadRequest();

            SessionModel domainModel = _mapper.Map<SessionModel>(insertDto);

            bool isSuccessful = await _repository.InsertSession(domainModel);
            if (!isSuccessful)
                return StatusCode(StatusCodes.Status500InternalServerError);

            SessionReadDto readDto = _mapper.Map<SessionReadDto>(domainModel);
            return CreatedAtRoute(nameof(GetSessionById), new { id = readDto.Id }, readDto);
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
        public async Task<ActionResult> UpdateSession(SessionUpdateDto updateDto)
        {
            if (!ModelState.IsValid)
                return BadRequest();

            SessionModel domainModel = await _repository.GetSessionById(updateDto.Id);

            if (domainModel == null)
                return NotFound();

            _mapper.Map(updateDto, domainModel);
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
        public async Task<ActionResult> PatchSession(int id, JsonPatchDocument<SessionUpdateDto> patchDocument)
        {
            SessionModel sessionModel = await _repository.GetSessionById(id);
            if (sessionModel == null)
                return NotFound();

            SessionUpdateDto sessionToPatch = _mapper.Map<SessionUpdateDto>(sessionModel);

            patchDocument.ApplyTo(sessionToPatch);
            if (!TryValidateModel(sessionToPatch))
                return ValidationProblem();

            _mapper.Map(sessionToPatch, sessionModel);
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
        public async Task<ActionResult> DeleteSession(int id)
        {
            SessionModel domainModel = await _repository.GetSessionById(id);
            if (domainModel == null)
                return BadRequest();

            bool isSuccessful = _repository.DeleteSession(domainModel);
            if (!isSuccessful)
                return StatusCode(StatusCodes.Status500InternalServerError);

            return NoContent();
        }
    }
}

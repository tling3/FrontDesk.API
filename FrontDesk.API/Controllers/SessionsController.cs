using AutoMapper;
using FrontDesk.API.Data.Interfaces;
using FrontDesk.API.Models.Domain;
using FrontDesk.API.Models.DTOs;
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

        [HttpGet]
        public async Task<ActionResult<IEnumerable<SessionReadDto>>> GetAllSessions()
        {
            var sessionModels = await _repository.GetAllSessions();
            return Ok(_mapper.Map<IEnumerable<SessionReadDto>>(sessionModels));
        }

        [HttpGet("{id}", Name = "GetSessionById")]
        public async Task<ActionResult<SessionReadDto>> GetSessionById(int id)
        {
            var sessionModel = await _repository.GetSessionById(id);
            if (sessionModel == null)
            {
                return NotFound();
            }
            return Ok(_mapper.Map<SessionReadDto>(sessionModel));
        }

        [HttpPost]
        public async Task<ActionResult<SessionReadDto>> InsertSession(SessionInsertDto sessionInsertDto)
        {
            //  TODO: add validation of dto model
            var sessionModel = _mapper.Map<Session>(sessionInsertDto);
            await _repository.InsertSession(sessionModel);
            _repository.SaveChanges();

            var sessionReadDto = _mapper.Map<SessionReadDto>(sessionModel);
            return CreatedAtRoute(nameof(GetSessionById), new { Id = sessionReadDto.Id }, sessionReadDto);
        }

        [HttpPut("{id}")]
        public async Task<ActionResult> UpdateSession(int id, SessionUpdateDto sessionUpdateDto)
        {
            //  TODO: add validation of dto model
            var sessionModel = await _repository.GetSessionById(id);

            if (sessionModel == null)
            {
                return NotFound();
            }

            _mapper.Map(sessionUpdateDto, sessionModel);
            _repository.UpdateSession(sessionModel);
            _repository.SaveChanges();

            //  TODO: Change status code
            return NoContent();
        }

        [HttpPatch("{id}")]
        public async Task<ActionResult> PartialUpdateSession(int id, JsonPatchDocument<SessionUpdateDto> patchDocument)
        {
            var sessionModel = await _repository.GetSessionById(id);
            if (sessionModel == null)
            {
                return NotFound();
            }

            var sessionToPatch = _mapper.Map<SessionUpdateDto>(sessionModel);

            patchDocument.ApplyTo(sessionToPatch);
            if (!TryValidateModel(sessionToPatch))
            {
                return ValidationProblem();
            }

            _mapper.Map(sessionToPatch, sessionModel);
            _repository.UpdateSession(sessionModel);
            _repository.SaveChanges();

            return NoContent();
        }
    }
}

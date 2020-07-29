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

        [HttpGet]
        public async Task<ActionResult<IEnumerable<WeekdayReadDto>>> GetAllWeekdays()
        {
            var weekdayModels = await _repository.GetAllWeekdays();
            return Ok(_mapper.Map<IEnumerable<WeekdayReadDto>>(weekdayModels));
        }

        [HttpGet("{id}", Name = "GetWeekdayById")]
        public async Task<ActionResult<WeekdayReadDto>> GetWeekdayById(int id)
        {
            var weekdayModel = await _repository.GetWeekdayById(id);
            if (weekdayModel == null)
            {
                return NotFound();
            }
            return Ok(_mapper.Map<WeekdayReadDto>(weekdayModel));
        }

        [HttpPost]
        public async Task<ActionResult<WeekdayReadDto>> InsertWeekday(WeekdayInsertDto weekdayInsertDto)
        {
            //  TODO: add validation of dto model
            var weekdayModel = _mapper.Map<Weekday>(weekdayInsertDto);
            await _repository.InsertWeekday(weekdayModel);
            _repository.SaveChanges();

            var weekdayReadDto = _mapper.Map<WeekdayReadDto>(weekdayModel);
            return CreatedAtRoute(nameof(GetWeekdayById), new { Id = weekdayReadDto.Id }, weekdayReadDto);
        }

        [HttpPut("{id}")]
        public async Task<ActionResult> UpdateWeekday(int id, WeekdayUpdateDto weekdayReadDto)
        {
            //  TODO: add validation of dto model
            var weekdayModel = await _repository.GetWeekdayById(id);

            if (weekdayReadDto == null)
            {
                return NotFound();
            }

            _mapper.Map(weekdayReadDto, weekdayModel);
            _repository.UpdateWeekday(weekdayModel);
            _repository.SaveChanges();

            //  TODO: Change status code
            return NoContent();
        }

        [HttpPatch("{id}")]
        public async Task<ActionResult> PartialUpdateWeekday(int id, JsonPatchDocument<WeekdayUpdateDto> patchDocument)
        {
            var weekdayModel = await _repository.GetWeekdayById(id);
            if (weekdayModel == null)
            {
                return NotFound();
            }

            var weekdayToPatch = _mapper.Map<WeekdayUpdateDto>(weekdayModel);

            patchDocument.ApplyTo(weekdayToPatch);
            if (!TryValidateModel(weekdayToPatch))
            {
                return ValidationProblem();
            }

            _mapper.Map(weekdayToPatch, weekdayModel);
            _repository.UpdateWeekday(weekdayModel);
            _repository.SaveChanges();

            return NoContent();
        }
    }
}

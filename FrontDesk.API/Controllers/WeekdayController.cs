using AutoMapper;
using FrontDesk.API.Data.Interfaces;
using FrontDesk.API.Models.Domain;
using FrontDesk.API.Models.DTOs;
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
    }
}

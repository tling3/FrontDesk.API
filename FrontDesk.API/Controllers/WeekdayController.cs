﻿using AutoMapper;
using FrontDesk.API.Data.Interfaces;
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
            var weekdayItems = await _repository.GetAllWeekdays();
            return Ok(_mapper.Map<IEnumerable<WeekdayReadDto>>(weekdayItems));
        }

        [HttpGet("{id}")]
        public async Task<ActionResult<WeekdayReadDto>> GetWeekdayById(int id)
        {
            var weekdayItem = await _repository.GetWeekdayById(id);
            if (weekdayItem == null)
            {
                return NotFound();
            }
            return Ok(_mapper.Map<WeekdayReadDto>(weekdayItem));
        }
    }
}

﻿using AutoMapper;
using FrontDesk.API.Data.Interfaces;
using FrontDesk.API.Models.DTOs;
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
            var sessionItems = await _repository.GetAllSessions();
            return Ok(_mapper.Map<IEnumerable<SessionReadDto>>(sessionItems));
        }

        [HttpGet("{id}")]
        public async Task<ActionResult<SessionReadDto>> GetSessionById(int id)
        {
            var sessionItem = await _repository.GetSessionById(id);
            if (sessionItem == null)
            {
                return NotFound();
            }
            return Ok(_mapper.Map<SessionReadDto>(sessionItem));
        }
    }
}

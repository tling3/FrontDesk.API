using AutoMapper;
using FrontDesk.API.Data.Interfaces;
using FrontDesk.API.Models.DTOs;
using Microsoft.AspNetCore.Mvc;
using System.Collections.Generic;

namespace FrontDesk.API.Controllers
{
    [Route("api/member")]
    [ApiController]

    public class MembersController : ControllerBase
    {
        private readonly IMemberRepo _repository;
        private readonly IMapper _mapper;

        public MembersController(
            IMemberRepo repository,
            IMapper mapper)
        {
            _repository = repository;
            _mapper = mapper;
        }

        [HttpGet]
        public ActionResult<IEnumerable<MemberReadDto>> GetAllMembers()
        {
            var memberItems = _repository.GetAllMembers();
            return Ok(_mapper.Map<IEnumerable<MemberReadDto>>(memberItems));
        }
    }
}

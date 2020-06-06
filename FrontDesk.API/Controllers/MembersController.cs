using AutoMapper;
using FrontDesk.API.Data.Interfaces;
using FrontDesk.API.Models.DTOs;
using Microsoft.AspNetCore.Mvc;
using System.Collections.Generic;
using System.Threading.Tasks;

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
        public async Task<ActionResult<IEnumerable<MemberReadDto>>> GetAllMembers()
        {
            var memberItems = await _repository.GetAllMembers();
            return Ok(_mapper.Map<IEnumerable<MemberReadDto>>(memberItems));
        }
    }
}

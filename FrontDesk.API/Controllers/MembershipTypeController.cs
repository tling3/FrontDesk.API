using AutoMapper;
using FrontDesk.API.Data.Interfaces;
using FrontDesk.API.Models.DTOs;
using Microsoft.AspNetCore.Mvc;
using System.Collections.Generic;
using System.Threading.Tasks;

namespace FrontDesk.API.Controllers
{
    [Route("api/membershiptype")]
    [ApiController]
    public class MembershipTypeController : ControllerBase
    {
        private readonly IMembershipTypeRepo _repository;
        private readonly IMapper _mapper;

        public MembershipTypeController(
            IMembershipTypeRepo repository,
            IMapper mapper)
        {
            _repository = repository;
            _mapper = mapper;
        }

        [HttpGet]
        public async Task<ActionResult<IEnumerable<MembershipTypeReadDto>>> GetAllMembershipTypes()
        {
            var membershipTypeItems = await _repository.GetAllMembershipTypes();
            return Ok(_mapper.Map<IEnumerable<MembershipTypeReadDto>>(membershipTypeItems));
        }

        [HttpGet("{id}")]
        public async Task<ActionResult<MembershipTypeReadDto>> GetMembershipTypeById(int id)
        {
            var membershipTypeItem = await _repository.GetMembershipTypeById(id);
            if (membershipTypeItem == null)
            {
                return NotFound();
            }
            return Ok(_mapper.Map<MembershipTypeReadDto>(membershipTypeItem));
        }
    }
}

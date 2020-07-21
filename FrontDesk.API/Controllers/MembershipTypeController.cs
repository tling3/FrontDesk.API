using AutoMapper;
using FrontDesk.API.Data.Interfaces;
using FrontDesk.API.Models.Domain;
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
            var membershipTypeModels = await _repository.GetAllMembershipTypes();
            return Ok(_mapper.Map<IEnumerable<MembershipTypeReadDto>>(membershipTypeModels));
        }

        [HttpGet("{id}", Name = "GetMembershipTypeById")]
        public async Task<ActionResult<MembershipTypeReadDto>> GetMembershipTypeById(int id)
        {
            var membershipTypeModel = await _repository.GetMembershipTypeById(id);
            if (membershipTypeModel == null)
            {
                return NotFound();
            }
            return Ok(_mapper.Map<MembershipTypeReadDto>(membershipTypeModel));
        }

        [HttpPost]
        public async Task<ActionResult<MembershipTypeReadDto>> InsertMembershipType(MembershipTypeInsertDto membershipTypeInsertDto)
        {
            //  TODO: add validation of dto model
            var membershipTypeModel = _mapper.Map<MembershipType>(membershipTypeInsertDto);
            await _repository.InsertMembershipType(membershipTypeModel);
            _repository.SaveChanges();

            var membershipTypeReadDto = _mapper.Map<MembershipTypeReadDto>(membershipTypeModel);
            return CreatedAtRoute(nameof(GetMembershipTypeById), new { Id = membershipTypeReadDto.Id }, membershipTypeReadDto);
        }

        [HttpPut("{id}")]
        public async Task<ActionResult> UpdateMembershipType(int id, MembershipTypeUpdateDto membershipTypeUpdateDto)
        {
            //  TODO: add validation of dto model
            var membershipTypeModel = await _repository.GetMembershipTypeById(id);
            if (membershipTypeModel == null)
            {
                return NotFound();
            }

            _mapper.Map(membershipTypeUpdateDto, membershipTypeModel);
            _repository.UpdateMembershipType(membershipTypeModel);
            _repository.SaveChanges();

            //  TODO: Change status code
            return NoContent();
        }
    }
}

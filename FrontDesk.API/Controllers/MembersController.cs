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
            var memberModels = await _repository.GetAllMembers();
            return Ok(_mapper.Map<IEnumerable<MemberReadDto>>(memberModels));
        }

        [HttpGet("{id}", Name = "GetMemberById")]
        public async Task<ActionResult<MemberReadDto>> GetMemberById(int id)
        {
            var memberModel = await _repository.GetMemberById(id);
            if (memberModel == null)
            {
                return NotFound();
            }
            return Ok(_mapper.Map<MemberReadDto>(memberModel));
        }

        [HttpPost]
        public async Task<ActionResult<MemberReadDto>> InsertMember(MemberInsertDto memberInsertDto)
        {
            //  TODO: add validation of dto model
            var memberModel = _mapper.Map<Member>(memberInsertDto);
            await _repository.InsertMember(memberModel);
            _repository.SaveChanges();

            var memberReadDto = _mapper.Map<MemberReadDto>(memberModel);
            return CreatedAtRoute(nameof(GetMemberById), new { Id = memberReadDto.Id }, memberReadDto);
        }

        [HttpPut("{id}")]
        public async Task<ActionResult> UpdateMember(int id, MemberUpdateDto memberUpdateDto)
        {
            //  TODO: add validation of dto model
            var memberModel = await _repository.GetMemberById(id);
            if (memberModel == null)
            {
                return NotFound();
            }

            _mapper.Map(memberUpdateDto, memberModel);
            _repository.UpdateMember(memberModel);
            _repository.SaveChanges();

            //  TODO: Change status code
            return NoContent();
        }

        [HttpPatch("{id}")]
        public async Task<ActionResult> PartialUpdateMember(int id, JsonPatchDocument<MemberUpdateDto> patchDocument)
        {
            var memberModel = await _repository.GetMemberById(id);
            if (memberModel == null)
            {
                return NotFound();
            }

            var memberToPatch = _mapper.Map<MemberUpdateDto>(memberModel);

            patchDocument.ApplyTo(memberToPatch);
            if (!TryValidateModel(memberToPatch))
            {
                return ValidationProblem();
            }

            _mapper.Map(memberToPatch, memberModel);
            _repository.UpdateMember(memberModel);
            _repository.SaveChanges();

            return NoContent();
        }
    }
}

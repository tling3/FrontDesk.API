using AutoMapper;
using FrontDesk.API.Data.Interfaces;
using FrontDesk.API.Models.Domain;
using FrontDesk.API.Models.DTOs;
using Microsoft.AspNetCore.Http;
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

        /// <summary>
        /// Get all Members items
        /// </summary>
        /// <returns></returns>
        /// <response code="404">Item(s) not found</response>
        /// <response code="200">Member items successfully found</response>
        //  GET ALL: api/member
        [HttpGet]
        public async Task<ActionResult<IEnumerable<MemberReadDto>>> GetAllMembers()
        {
            IEnumerable<MemberModel> memberModels = await _repository.GetAllMembers();
            if (memberModels == null)
                return NotFound();

            return Ok(_mapper.Map<IEnumerable<MemberReadDto>>(memberModels));
        }

        /// <summary>
        /// Get Members item by id
        /// </summary>
        /// <returns></returns>
        /// <response code="404">Item not found</response>
        /// <response code="200">Member item successfully found</response>
        //  GET ALL: api/member/{id}
        [HttpGet("{id}", Name = nameof(GetMemberById))]
        public async Task<ActionResult<MemberReadDto>> GetMemberById(int id)
        {
            MemberModel domainModel = await _repository.GetMemberById(id);
            if (domainModel == null)
                return NotFound();

            return Ok(_mapper.Map<MemberReadDto>(domainModel));
        }

        /// <summary>
        /// Insert Member item
        /// </summary>
        /// <param name="insertDto"></param>
        /// <returns>Member Item</returns>
        /// <response code="400">Item to be inserted is not valid</response>
        /// <response code="500">Item failed to be inserted</response>
        /// <response code="201">Member item was successfully inserted</response>
        // INSERT: api/member
        [HttpPost]
        [ProducesResponseType(StatusCodes.Status201Created)]
        public async Task<ActionResult<MemberReadDto>> InsertMember(MemberInsertDto insertDto)
        {
            if (!ModelState.IsValid)
                return BadRequest();

            MemberModel domainModel = _mapper.Map<MemberModel>(insertDto);

            bool isSuccessful = await _repository.InsertMember(domainModel);
            if (!isSuccessful)
                return StatusCode(StatusCodes.Status500InternalServerError);

            var memberReadDto = _mapper.Map<MemberReadDto>(domainModel);
            return CreatedAtRoute(nameof(GetMemberById), new { id = memberReadDto.Id }, memberReadDto);
        }


        /// <summary>
        /// Update Member item
        /// </summary>
        /// <param name="updateDto"></param>
        /// <returns></returns>
        /// <response code="400">Updated item is not valid</response>
        /// <response code="404">Item to be updated not found</response>
        /// <response code="500">Item failed to be updated</response>
        /// <response code="204">Member item was successfully updated</response>
        //  UPDATE: api/member
        [HttpPut]
        [ProducesResponseType(StatusCodes.Status204NoContent)]
        public async Task<ActionResult> UpdateMember(MemberUpdateDto updateDto)
        {
            if (!ModelState.IsValid)
                return BadRequest();

            MemberModel domainModel = await _repository.GetMemberById(updateDto.Id);
            if (domainModel == null)
                return NotFound();

            _mapper.Map(updateDto, domainModel);
            _repository.UpdateMember(domainModel);
            bool isSuccessful = _repository.SaveChanges();
            if (!isSuccessful)
                return StatusCode(StatusCodes.Status500InternalServerError);

            return NoContent();
        }

        /// <summary>
        /// Patches Member item
        /// </summary>
        /// <param name="id"></param>
        /// <param name="patchDocument"></param>
        /// <returns></returns>
        /// <response code="404">Item to be patched not found</response>
        /// <response code="400">Item failed validation after applying the patch</response>
        /// <response code="500">Item failed to be patched</response>
        /// <response code="204">Member item was successfully patched</response>
        //  PATCH: api/member/{id}
        [HttpPatch("{id}")]
        [ProducesResponseType(StatusCodes.Status204NoContent)]
        public async Task<ActionResult> PatchMember(int id, JsonPatchDocument<MemberUpdateDto> patchDocument)
        {
            MemberModel domainModel = await _repository.GetMemberById(id);
            if (domainModel == null)
                return NotFound();

            MemberUpdateDto memberToPatch = _mapper.Map<MemberUpdateDto>(domainModel);

            patchDocument.ApplyTo(memberToPatch);
            if (!TryValidateModel(memberToPatch))
                return ValidationProblem();

            _mapper.Map(memberToPatch, domainModel);
            _repository.UpdateMember(domainModel);
            bool isSuccessful = _repository.SaveChanges();
            if (!isSuccessful)
                return StatusCode(StatusCodes.Status500InternalServerError);

            return NoContent();
        }

        /// <summary>
        /// Deletes Member item
        /// </summary>
        /// <param name="id"></param>
        /// <returns></returns>
        /// <response code="404">Item to be deleted not found</response>
        /// <response code="500">Item failed to be deleted</response>
        /// <response code="204">Member item was successfully deleted</response>
        //  DELETE: api/member/{id}
        [HttpDelete]
        [ProducesResponseType(StatusCodes.Status204NoContent)]
        public async Task<ActionResult> DeleteMember(int id)
        {
            MemberModel domainModel = await _repository.GetMemberById(id);
            if (domainModel == null)
                return NotFound();

            bool isSuccessful = _repository.DeleteMember(domainModel);
            if (!isSuccessful)
                return StatusCode(StatusCodes.Status500InternalServerError);

            return NoContent();
        }
    }
}

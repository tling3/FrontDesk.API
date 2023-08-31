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

        /// <summary>
        /// Get all Membership Type items
        /// </summary>
        /// <returns>All Membership Type items</returns>
        /// <response code="404">Items not found</response>
        /// <response code="200">Membership Type items successfully found</response>
        //  GET ALL: api/membershiptype
        [HttpGet]
        public async Task<ActionResult<IEnumerable<MembershipTypeReadDto>>> GetAllMembershipTypesAsync()
        {
            IEnumerable<MembershipTypeModel> domainModel = await _repository.GetAllMembershipTypesAsync();
            if (domainModel == null)
                return NotFound();

            return Ok(_mapper.Map<IEnumerable<MembershipTypeReadDto>>(domainModel));
        }

        /// <summary>
        /// Get Membership Type item
        /// </summary>
        /// <returns>Membership Type item</returns>
        /// <response code="404">Item not found</response>
        /// <response code="200">Membership Type item successfully found</response>
        //  GET ALL: api/membershiptype
        [HttpGet("{id}", Name = nameof(GetMembershipTypeByIdAsync))]
        public async Task<ActionResult<MembershipTypeReadDto>> GetMembershipTypeByIdAsync(int id)
        {
            MembershipTypeModel domainModel = await _repository.GetMembershipTypeByIdAsync(id);
            if (domainModel == null)
                return NotFound();

            return Ok(_mapper.Map<MembershipTypeReadDto>(domainModel));
        }

        /// <summary>
        /// Insert Membership Type item
        /// </summary>
        /// <param name="insertDto"></param>
        /// <returns>Membership Type item</returns>
        /// <response code="400">Item to be inserted is not valid</response>
        /// <response code="500">Item failed to be inserted</response>
        /// <response code="201">Membership Type item was successfully inserted</response>
        //  INSERT: api/membershiptype
        [HttpPost]
        [ProducesResponseType(StatusCodes.Status201Created)]
        public async Task<ActionResult<MembershipTypeReadDto>> InsertMembershipTypeAsync(MembershipTypeInsertDto insertDto)
        {
            if (!ModelState.IsValid)
                return BadRequest();

            MembershipTypeModel domainModel = _mapper.Map<MembershipTypeModel>(insertDto);
            bool isSuccessfull = await _repository.InsertMembershipTypeAsync(domainModel);
            if (!isSuccessfull)
                return StatusCode(StatusCodes.Status500InternalServerError);

            MembershipTypeReadDto readDto = _mapper.Map<MembershipTypeReadDto>(domainModel);
            return CreatedAtRoute(nameof(GetMembershipTypeByIdAsync), new { id = readDto.Id }, readDto);
        }

        /// <summary>
        /// Updates Membership Type item
        /// </summary>
        /// <param name="updateDto"></param>
        /// <returns></returns>
        /// <response code="400">Updated item is not valid</response>
        /// <response code="404">Item to be updated not found</response>
        /// <response code="500">Item failed to be updated</response>
        /// <response code="204">Membership Type item was successfully updated</response>
        //  UPDATE: api/membershiptype/{id}
        [HttpPut]
        [ProducesResponseType(StatusCodes.Status500InternalServerError)]
        public async Task<IActionResult> UpdateMembershipTypeAsync(MembershipTypeUpdateDto updateDto)
        {
            if (!ModelState.IsValid)
                return BadRequest();

            MembershipTypeModel domainModel = await _repository.GetMembershipTypeByIdAsync(updateDto.Id);
            if (domainModel == null)
                return NotFound();

            _mapper.Map(updateDto, domainModel);
            _repository.UpdateMembershipType(domainModel);
            bool isSuccessful = _repository.SaveChanges();
            if (!isSuccessful)
                return StatusCode(StatusCodes.Status500InternalServerError);

            return NoContent();
        }

        /// <summary>
        /// Patches Membership Type item
        /// </summary>
        /// <param name="id"></param>
        /// <param name="patchDocument"></param>
        /// <returns></returns>
        /// <response code="404">Item to be patched not found</response>
        /// <response code="400">Item failed validation after applying patch</response>
        /// <response code="500">Item failed to be patched</response>
        /// <response code="204">Membership Type item was successfully patched</response>
        //  PATCH: api/membershiptype/{id}
        [HttpPatch("{id}")]
        [ProducesResponseType(StatusCodes.Status204NoContent)]
        public async Task<IActionResult> PatchMembershipTypeAsync(int id, JsonPatchDocument<MembershipTypeUpdateDto> patchDocument)
        {
            MembershipTypeModel domainModel = await _repository.GetMembershipTypeByIdAsync(id);
            if (domainModel == null)
                return NotFound();

            MembershipTypeUpdateDto membershipTypeToPatch = _mapper.Map<MembershipTypeUpdateDto>(domainModel);

            patchDocument.ApplyTo(membershipTypeToPatch);
            if (!TryValidateModel(membershipTypeToPatch))
                return ValidationProblem();

            _mapper.Map(membershipTypeToPatch, domainModel);
            _repository.UpdateMembershipType(domainModel);

            bool isSuccessful = _repository.SaveChanges();
            if (!isSuccessful)
                return StatusCode(StatusCodes.Status500InternalServerError);

            return NoContent();
        }

        /// <summary>
        /// Deletes Membership Type item
        /// </summary>
        /// <param name="id"></param>
        /// <returns></returns>
        /// <response code="400">Item to be deleted not found</response>
        /// <response code="500">Item failed to be deleted</response>
        /// <response code="204">Membership Type item was successfully deleted</response>
        //  DELETE: api/membershiptype/{id}
        [HttpDelete("{id}")]
        [ProducesResponseType(StatusCodes.Status204NoContent)]
        public async Task<IActionResult> DeleteMembershipTypeAsync(int id)
        {
            MembershipTypeModel domainModel = await _repository.GetMembershipTypeByIdAsync(id);
            if (domainModel == null)
                return BadRequest();

            bool isSuccessful = _repository.DeleteMembershipType(domainModel);
            if (!isSuccessful)
                return StatusCode(StatusCodes.Status500InternalServerError);

            return NoContent();
        }
    }
}

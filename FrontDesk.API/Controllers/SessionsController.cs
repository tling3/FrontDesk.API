using FrontDesk.API.Data.Interfaces;
using FrontDesk.API.Models.Domain;
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
        public SessionsController(ISessionRepo repository)
        {
            _repository = repository;
        }

        [HttpGet]
        public async Task<ActionResult<IEnumerable<Session>>> GetAllSessions()
        {
            var sessionItems = await _repository.GetAllSessions();
            return Ok(sessionItems);
        }
    }
}

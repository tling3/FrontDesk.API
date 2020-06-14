using FrontDesk.API.Data.Context;
using FrontDesk.API.Data.Interfaces;
using FrontDesk.API.Models.Domain;
using Microsoft.EntityFrameworkCore;
using System.Collections.Generic;
using System.Threading.Tasks;

namespace FrontDesk.API.Data.Repositories
{
    public class SqlSessionRepo : ISessionRepo
    {
        private readonly SessionContext _context;
        public SqlSessionRepo(SessionContext context)
        {
            _context = context;
        }
        public Task<List<Session>> GetAllSessions()
        {
            return _context.Session.ToListAsync();
        }
    }
}

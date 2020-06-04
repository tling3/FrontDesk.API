using FrontDesk.API.Data.Context;
using FrontDesk.API.Data.Interfaces;
using FrontDesk.API.Models.Domain;
using System.Collections.Generic;
using System.Linq;

namespace FrontDesk.API.Data.Repositories
{
    public class SqlSessionRepo : ISessionRepo
    {
        private readonly SessionContext _context;
        public SqlSessionRepo(SessionContext context)
        {
            _context = context;
        }
        public IEnumerable<Session> GetAllSessions()
        {
            return _context.Session.ToList();
        }
    }
}

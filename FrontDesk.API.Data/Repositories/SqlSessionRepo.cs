using FrontDesk.API.Data.Base;
using FrontDesk.API.Data.Context;
using FrontDesk.API.Data.Interfaces;
using FrontDesk.API.Models.Domain;
using Microsoft.EntityFrameworkCore;
using System;
using System.Collections.Generic;
using System.Threading.Tasks;

namespace FrontDesk.API.Data.Repositories
{
    public class SqlSessionRepo : BaseRepo<SessionContext>, ISessionRepo
    {
        private readonly SessionContext _context;

        public SqlSessionRepo(SessionContext context) : base(context)
        {
            _context = context;
        }

        public async Task<IEnumerable<Session>> GetAllSessions()
        {
            return await _context.Session.ToListAsync();
        }

        public async Task<Session> GetSessionById(int id)
        {
            return await _context.Session.FirstOrDefaultAsync(s => s.Id == id);
        }

        public async Task InsertSession(Session session)
        {
            if (session == null)
            {
                throw new ArgumentNullException(nameof(session));
            }
            await _context.AddAsync(session);
        }

        public void UpdateSession(Session session)
        {
            // no necessary work right now
        }
    }
}

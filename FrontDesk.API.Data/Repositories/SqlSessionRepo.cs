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
    public class SqlSessionRepo : BaseRepo<FrontDeskContext>, ISessionRepo
    {
        private readonly FrontDeskContext _context;

        public SqlSessionRepo(FrontDeskContext context) : base(context)
        {
            _context = context;
        }

        public async Task<IEnumerable<SessionModel>> GetAllSessions()
        {
            return await _context.Session.ToListAsync();
        }

        public async Task<SessionModel> GetSessionById(int id)
        {
            return await _context.Session.FirstOrDefaultAsync(s => s.Id == id);
        }

        public async Task<bool> InsertSession(SessionModel session)
        {
            if (session == null)
                throw new ArgumentNullException(nameof(session));

            await _context.AddAsync(session);
            return SaveChanges();
        }

        public void UpdateSession(SessionModel session)
        {
            // intentionally left blank
            // no necessary work right now
        }

        public bool DeleteSession(SessionModel domainModel)
        {
            if (domainModel == null)
                throw new ArgumentNullException();

            _context.Remove(domainModel);
            return SaveChanges();
        }
    }
}

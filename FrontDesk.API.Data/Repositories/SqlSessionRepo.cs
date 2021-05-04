using FrontDesk.API.Data.Base;
using FrontDesk.API.Data.Context;
using FrontDesk.API.Data.Interfaces;
using FrontDesk.API.Models.Domain;
using FrontDesk.API.Models.DTOs;
using Microsoft.EntityFrameworkCore;
using System;
using System.Collections.Generic;
using System.Linq;
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

        public async Task<IEnumerable<SessionReadDto>> GetAllSessions()
        {
            List<SessionReadDto> sessionList = await _context.Session
                .Join(
                    _context.Weekday,
                    session => session.WeekdayId,
                    weekday => weekday.Id,
                    (session, weekday) => new SessionReadDto
                    {
                        Id = session.Id,
                        AgeLevel = session.AgeLevel,
                        SessionType = session.SessionType,
                        SessionLevel = session.SessionLevel,
                        Instructor = session.Instructor,
                        StartTime = session.StartTime,
                        EndTime = session.EndTime,
                        Weekday = weekday.Weekday,
                        CreatedDate = session.CreatedDate,
                        ModifiedDate = session.ModifiedDate,
                        ModifiedBy = session.ModifiedBy
                    }
                )
                .OrderBy(session => session.EndTime).ToListAsync();

            TimeSpan currentTime = DateTime.Now.TimeOfDay;
            string currentDayOfTheWeek = DateTime.Now.DayOfWeek.ToString();
            IEnumerable<SessionReadDto> sessionEnum = sessionList
                .Where(session => session.Weekday == currentDayOfTheWeek && session.EndTime.TimeOfDay >= currentTime)
                .ToList();

            return sessionEnum;
        }

        public async Task<SessionReadDto> GetSessionById(int id)
        {
            //return await _context.Session.FirstOrDefaultAsync(s => s.Id == id);
            return await _context.Session
                .Join(
                    _context.Weekday,
                    session => session.WeekdayId,
                    weekday => weekday.Id,
                    (session, weekday) => new SessionReadDto
                    {
                        Id = session.Id,
                        AgeLevel = session.AgeLevel,
                        SessionType = session.SessionType,
                        SessionLevel = session.SessionLevel,
                        Instructor = session.Instructor,
                        StartTime = session.StartTime,
                        EndTime = session.EndTime,
                        Weekday = weekday.Weekday,
                        CreatedDate = session.CreatedDate,
                        ModifiedDate = session.ModifiedDate,
                        ModifiedBy = session.ModifiedBy
                    }
                )
                .FirstOrDefaultAsync(session => session.Id == id);
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

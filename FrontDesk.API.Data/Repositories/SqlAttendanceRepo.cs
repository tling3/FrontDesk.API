using FrontDesk.API.Data.Base;
using FrontDesk.API.Data.Context;
using FrontDesk.API.Data.Interfaces;
using FrontDesk.API.Models.Domain;
using Microsoft.EntityFrameworkCore;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace FrontDesk.API.Data.Repositories
{
    public class SqlAttendanceRepo : BaseRepo<FrontDeskContext>, IAttendanceRepo
    {
        private readonly FrontDeskContext _context;

        public SqlAttendanceRepo(FrontDeskContext context) : base(context)
        {
            _context = context;
        }

        public async Task<IEnumerable<AttendanceModel>> GetAllAttendance()
        {
            return await _context.Attendance.ToListAsync();
        }

        public async Task<List<AttendanceModel>> GetAttendanceByMemberId(int id)
        {
            return await _context.Attendance
                .Where(model => model.MemberId == id)
                .ToListAsync();
        }

        public async Task<AttendanceModel> GetAttendanceBySessionId(int sessionId)
        {
            return await _context.Attendance.FirstOrDefaultAsync(model => model.SessionId == sessionId);
        }

        public async Task<List<AttendanceModel>> GetAttendancePerSession(int sessionId, DateTime date)
        {
            return await _context.Attendance.Where(model => model.SessionId == sessionId && model.SessionDate == date).ToListAsync();
        }

        public async Task<bool> InsertAttendance(AttendanceModel attendance)
        {
            if (attendance == null)
                throw new ArgumentNullException(nameof(attendance));

            await _context.Attendance.AddAsync(attendance);

            return SaveChanges();
        }

        public void UpdateAttendance(AttendanceModel attendance)
        {
            // intentionally left blank
            // no necessary work right now
        }

        public bool DeleteAttendanceById(AttendanceModel domainModel)
        {
            if (domainModel == null)
                throw new ArgumentNullException();

            _context.Remove(domainModel);
            return SaveChanges();
        }
    }
}

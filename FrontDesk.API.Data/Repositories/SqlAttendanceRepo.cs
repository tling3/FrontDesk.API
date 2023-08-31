using FrontDesk.API.Data.Base;
using FrontDesk.API.Data.Context;
using FrontDesk.API.Data.Interfaces;
using FrontDesk.API.Models.Custom.Attendance;
using FrontDesk.API.Models.Domain;
using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Internal;
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

        public async Task<IEnumerable<AttendanceModel>> GetAllAttendanceAsync()
        {
            return await _context.Attendance.ToListAsync();
        }

        public async Task<List<AttendanceModel>> GetAttendanceByMemberIdAsync(int id)
        {
            return await _context.Attendance
                .Where(model => model.MemberId == id)
                .ToListAsync();
        }

        public async Task<AttendanceModel> GetAttendanceBySessionIdAsync(int sessionId)
        {
            return await _context.Attendance.FirstOrDefaultAsync(model => model.SessionId == sessionId);
        }

        public async Task<List<AttendancePerSessionDto>> GetAttendancePerSessionAsync(int sessionId, DateTime date)
        {
            List<AttendancePerSessionDto> attendancePerSession = await _context.Attendance
                .Join(
                    _context.Member,
                    attendance => attendance.MemberId,
                    member => member.Id,
                    (attendance, member) => new AttendancePerSessionDto
                    {
                        Id = attendance.Id,
                        SessionId = attendance.SessionId,
                        MemberId = attendance.MemberId,
                        SessionDate = attendance.SessionDate,
                        FirstName = member.FirstName,
                        LastName = member.LastName,
                        Email = member.Email
                    }
                )
                .Where(model => model.SessionId == sessionId && model.SessionDate == date)
                .ToListAsync();

            return attendancePerSession;
        }

        public async Task<bool> InsertAttendanceAsync(AttendanceModel attendance)
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

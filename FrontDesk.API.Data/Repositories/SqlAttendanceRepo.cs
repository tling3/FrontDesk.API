using FrontDesk.API.Data.Context;
using FrontDesk.API.Data.Interfaces;
using FrontDesk.API.Models.Domain;
using Microsoft.EntityFrameworkCore;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace FrontDesk.API.Data.Repositories
{
    public class SqlAttendanceRepo : IAttendanceRepo
    {
        private readonly AttendanceContext _context;

        public SqlAttendanceRepo(AttendanceContext context)
        {
            _context = context;
        }
        public Task<List<Attendance>> GetAllAttendance()
        {
            return _context.Attendance.ToListAsync();
        }
        public Task<List<Attendance>> GetAttendanceByMemberId(int id)
        {
            return _context.Attendance
                .Where(a => a.MemberId == id)
                .ToListAsync();
        }
    }
}

﻿using FrontDesk.API.Data.Base;
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
    public class SqlAttendanceRepo : BaseRepo<AttendanceContext>, IAttendanceRepo
    {
        private readonly AttendanceContext _context;

        public SqlAttendanceRepo(AttendanceContext context) : base(context)
        {
            _context = context;
        }

        public async Task<List<Attendance>> GetAllAttendance()
        {
            return await _context.Attendance.ToListAsync();
        }

        public async Task<List<Attendance>> GetAttendanceByMemberId(int id)
        {
            return await _context.Attendance
                .Where(a => a.MemberId == id)
                .ToListAsync();
        }

        public async Task<Attendance> GetAttendanceById(int id)
        {
            return await _context.Attendance.FirstOrDefaultAsync(a => a.Id == id);
        }

        public async Task InsertAttendance(Attendance attendance)
        {
            if (attendance == null)
            {
                throw new ArgumentNullException(nameof(attendance));
            }
            await _context.Attendance.AddAsync(attendance);
        }
    }
}

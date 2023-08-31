using FrontDesk.API.Models.Custom.Attendance;
using FrontDesk.API.Models.Domain;
using System;
using System.Collections.Generic;
using System.Threading.Tasks;

namespace FrontDesk.API.Data.Interfaces
{
    public interface IAttendanceRepo
    {
        bool SaveChanges();
        Task<IEnumerable<AttendanceModel>> GetAllAttendanceAsync();
        Task<List<AttendanceModel>> GetAttendanceByMemberIdAsync(int id);
        Task<AttendanceModel> GetAttendanceBySessionIdAsync(int sessionId);
        Task<List<AttendancePerSessionDto>> GetAttendancePerSessionAsync(int sessionId, DateTime date);
        Task<bool> InsertAttendanceAsync(AttendanceModel attendance);
        void UpdateAttendance(AttendanceModel attendance);
        bool DeleteAttendanceById(AttendanceModel domainModel);
    }
}

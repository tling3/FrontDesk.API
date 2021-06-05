using FrontDesk.API.Models.Domain;
using System;
using System.Collections.Generic;
using System.Threading.Tasks;

namespace FrontDesk.API.Data.Interfaces
{
    public interface IAttendanceRepo
    {
        bool SaveChanges();
        Task<IEnumerable<AttendanceModel>> GetAllAttendance();
        Task<List<AttendanceModel>> GetAttendanceByMemberId(int id);
        Task<AttendanceModel> GetAttendanceBySessionId(int sessionId);
        Task<List<AttendanceModel>> GetAttendancePerSession(int sessionId, DateTime date);
        Task<bool> InsertAttendance(AttendanceModel attendance);
        void UpdateAttendance(AttendanceModel attendance);
        bool DeleteAttendanceById(AttendanceModel domainModel);
    }
}

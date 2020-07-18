using FrontDesk.API.Models.Domain;
using System.Collections.Generic;
using System.Threading.Tasks;

namespace FrontDesk.API.Data.Interfaces
{
    public interface IAttendanceRepo
    {
        bool SaveChanges();
        Task<List<Attendance>> GetAllAttendance();
        Task<List<Attendance>> GetAttendanceByMemberId(int id);
        Task<Attendance> GetAttendanceById(int id);
        Task InsertAttendance(Attendance attendance);
    }
}

using FrontDesk.API.Models.Domain;
using System.Collections.Generic;
using System.Threading.Tasks;

namespace FrontDesk.API.Data.Interfaces
{
    public interface IAttendanceRepo
    {
        Task<List<Attendance>> GetAllAttendance();
        Task<List<Attendance>> GetAttendanceByMemberId(int id);
    }
}

using FrontDesk.API.Models.Domain;
using FrontDesk.API.Models.DTOs;
using System.Collections.Generic;
using System.Threading.Tasks;

namespace FrontDesk.API.Data.Interfaces
{
    public interface ISessionRepo
    {
        bool SaveChanges();
        Task<IEnumerable<SessionReadDto>> GetAllSessions();
        Task<SessionReadDto> GetSessionById(int id);
        Task<bool> InsertSession(SessionModel session);
        void UpdateSession(SessionModel session);
        bool DeleteSession(SessionModel domainModel);
    }
}

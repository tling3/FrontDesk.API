using FrontDesk.API.Models.Domain;
using System.Collections.Generic;
using System.Threading.Tasks;

namespace FrontDesk.API.Data.Interfaces
{
    public interface ISessionRepo
    {
        bool SaveChanges();
        Task<List<Session>> GetAllSessions();
        Task<Session> GetSessionById(int id);
        Task InsertSession(Session session);
        void UpdateSession(Session session);
    }
}

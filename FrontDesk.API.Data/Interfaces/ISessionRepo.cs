using FrontDesk.API.Models.Domain;
using System.Collections.Generic;
using System.Threading.Tasks;

namespace FrontDesk.API.Data.Interfaces
{
    public interface ISessionRepo
    {
        Task<List<Session>> GetAllSessions();
    }
}

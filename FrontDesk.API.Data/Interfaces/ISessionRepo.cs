using FrontDesk.API.Models.Domain;
using System.Collections.Generic;

namespace FrontDesk.API.Data.Interfaces
{
    public interface ISessionRepo
    {
        IEnumerable<Session> GetAllSessions();
    }
}

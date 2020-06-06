using FrontDesk.API.Models.Domain;
using System.Collections.Generic;
using System.Threading.Tasks;

namespace FrontDesk.API.Data.Interfaces
{
    public interface IMemberRepo
    {
        Task<List<Member>> GetAllMembers();
    }
}

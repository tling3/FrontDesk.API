using FrontDesk.API.Models.Domain;
using System.Collections.Generic;
using System.Threading.Tasks;

namespace FrontDesk.API.Data.Interfaces
{
    public interface IMembershipTypeRepo
    {
        Task<List<MembershipType>> GetAllMembershipTypes();
        Task<MembershipType> GetMembershipTypeById(int id);
    }
}

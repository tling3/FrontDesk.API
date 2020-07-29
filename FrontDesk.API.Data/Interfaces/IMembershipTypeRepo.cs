using FrontDesk.API.Models.Domain;
using System.Collections.Generic;
using System.Threading.Tasks;

namespace FrontDesk.API.Data.Interfaces
{
    public interface IMembershipTypeRepo
    {
        bool SaveChanges();
        Task<IEnumerable<MembershipType>> GetAllMembershipTypes();
        Task<MembershipType> GetMembershipTypeById(int id);
        Task InsertMembershipType(MembershipType membershipType);
        void UpdateMembershipType(MembershipType membershipType);
    }
}

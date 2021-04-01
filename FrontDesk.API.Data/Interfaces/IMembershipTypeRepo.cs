using FrontDesk.API.Models.Domain;
using System.Collections.Generic;
using System.Threading.Tasks;

namespace FrontDesk.API.Data.Interfaces
{
    public interface IMembershipTypeRepo
    {
        bool SaveChanges();
        Task<IEnumerable<MembershipTypeModel>> GetAllMembershipTypes();
        Task<MembershipTypeModel> GetMembershipTypeById(int id);
        Task<bool> InsertMembershipType(MembershipTypeModel membershipType);
        void UpdateMembershipType(MembershipTypeModel membershipType);
        bool DeleteMembershipType(MembershipTypeModel domainModel);
    }
}

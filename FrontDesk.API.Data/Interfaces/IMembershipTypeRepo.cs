using FrontDesk.API.Models.Domain;
using System.Collections.Generic;
using System.Threading.Tasks;

namespace FrontDesk.API.Data.Interfaces
{
    public interface IMembershipTypeRepo
    {
        bool SaveChanges();
        Task<IEnumerable<MembershipTypeModel>> GetAllMembershipTypesAsync();
        Task<MembershipTypeModel> GetMembershipTypeByIdAsync(int id);
        Task<bool> InsertMembershipTypeAsync(MembershipTypeModel membershipType);
        void UpdateMembershipType(MembershipTypeModel membershipType);
        bool DeleteMembershipType(MembershipTypeModel domainModel);
    }
}

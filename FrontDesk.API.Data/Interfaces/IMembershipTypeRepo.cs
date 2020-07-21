using FrontDesk.API.Models.Domain;
using FrontDesk.API.Models.DTOs;
using System.Collections.Generic;
using System.Threading.Tasks;

namespace FrontDesk.API.Data.Interfaces
{
    public interface IMembershipTypeRepo
    {
        bool SaveChanges();
        Task<List<MembershipType>> GetAllMembershipTypes();
        Task<MembershipType> GetMembershipTypeById(int id);
        Task InsertMembershipType(MembershipType membershipType);
        void UpdateMembershipType(MembershipType membershipType);
    }
}

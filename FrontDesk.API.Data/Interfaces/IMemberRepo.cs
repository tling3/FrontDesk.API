using FrontDesk.API.Models.Domain;
using System.Collections.Generic;
using System.Threading.Tasks;

namespace FrontDesk.API.Data.Interfaces
{
    public interface IMemberRepo
    {
        bool SaveChanges();
        Task<IEnumerable<MemberModel>> GetAllMembersAsync();
        Task<MemberModel> GetMemberByIdAsync(int id);
        Task<bool> InsertMemberAsync(MemberModel member);
        void UpdateMember(MemberModel memberModel);
        bool DeleteMember(MemberModel domainModel);
    }
}

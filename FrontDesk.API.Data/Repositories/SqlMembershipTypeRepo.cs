using FrontDesk.API.Data.Context;
using FrontDesk.API.Data.Interfaces;
using FrontDesk.API.Models.Domain;
using Microsoft.EntityFrameworkCore;
using System.Collections.Generic;
using System.Threading.Tasks;

namespace FrontDesk.API.Data.Repositories
{
    public class SqlMembershipTypeRepo : IMembershipTypeRepo
    {
        private readonly MembershipTypeContext _context;

        public SqlMembershipTypeRepo(MembershipTypeContext context)
        {
            _context = context;
        }
        public Task<List<MembershipType>> GetAllMembershipTypes()
        {
            return _context.MembershipType.ToListAsync();
        }
    }
}

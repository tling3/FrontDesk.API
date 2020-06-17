using FrontDesk.API.Data.Context;
using FrontDesk.API.Data.Interfaces;
using FrontDesk.API.Models.Domain;
using Microsoft.EntityFrameworkCore;
using System.Collections.Generic;
using System.Threading.Tasks;

namespace FrontDesk.API.Data.Repositories
{
    public class SqlMemberRepo : IMemberRepo
    {
        private readonly MemberContext _context;

        public SqlMemberRepo(MemberContext context)
        {
            _context = context;
        }

        public Task<List<Member>> GetAllMembers()
        {
            return _context.Member.ToListAsync();
        }

        public Task<Member> GetMemberById(int id)
        {
            return _context.Member.FirstOrDefaultAsync(m => m.Id == id);
        }
    }
}

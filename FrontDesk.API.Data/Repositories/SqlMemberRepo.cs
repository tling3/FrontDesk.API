using FrontDesk.API.Data.Context;
using FrontDesk.API.Data.Interfaces;
using FrontDesk.API.Models.Domain;
using System.Collections.Generic;
using System.Linq;

namespace FrontDesk.API.Data.Repositories
{
    public class SqlMemberRepo : IMemberRepo
    {
        private readonly MemberContext _context;

        public SqlMemberRepo(MemberContext context)
        {
            _context = context;
        }

        public IEnumerable<Member> GetAllMembers()
        {
            return _context.Member.ToList();
        }
    }
}

using FrontDesk.API.Data.Base;
using FrontDesk.API.Data.Context;
using FrontDesk.API.Data.Interfaces;
using FrontDesk.API.Models.Domain;
using Microsoft.EntityFrameworkCore;
using System;
using System.Collections.Generic;
using System.Threading.Tasks;

namespace FrontDesk.API.Data.Repositories
{
    public class SqlMemberRepo : BaseRepo<MemberContext>, IMemberRepo
    {
        private readonly MemberContext _context;

        public SqlMemberRepo(MemberContext context) : base(context)
        {
            _context = context;
        }

        public async Task<IEnumerable<Member>> GetAllMembers()
        {
            return await _context.Member.ToListAsync();
        }

        public async Task<Member> GetMemberById(int id)
        {
            return await _context.Member.FirstOrDefaultAsync(m => m.Id == id);
        }

        public async Task InsertMember(Member member)
        {
            if (member == null)
            {
                throw new ArgumentNullException(nameof(member));
            }
            await _context.Member.AddAsync(member);
        }

        public void UpdateMember(Member memberModel)
        {
            // no necessary work right now
        }
    }
}

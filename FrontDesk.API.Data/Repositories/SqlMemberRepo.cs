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
    public class SqlMemberRepo : BaseRepo<FrontDeskContext>, IMemberRepo
    {
        private readonly FrontDeskContext _context;

        public SqlMemberRepo(FrontDeskContext context) : base(context)
        {
            _context = context;
        }

        public async Task<IEnumerable<MemberModel>> GetAllMembersAsync()
        {
            return await _context.Member.ToListAsync();
        }

        public async Task<MemberModel> GetMemberByIdAsync(int id)
        {
            return await _context.Member.FirstOrDefaultAsync(m => m.Id == id);
        }

        public async Task<bool> InsertMemberAsync(MemberModel member)
        {
            if (member == null)
                throw new ArgumentNullException(nameof(member));

            await _context.Member.AddAsync(member);
            return SaveChanges();
        }

        public void UpdateMember(MemberModel memberModel)
        {
            // intentionally left blank
            // no necessary work right now
        }

        public bool DeleteMember(MemberModel domainModel)
        {
            if (domainModel == null)
                throw new ArgumentNullException();

            _context.Remove(domainModel);
            return SaveChanges();
        }
    }
}

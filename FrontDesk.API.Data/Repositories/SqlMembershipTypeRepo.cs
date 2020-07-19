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
    public class SqlMembershipTypeRepo : BaseRepo<MembershipTypeContext>, IMembershipTypeRepo
    {
        private readonly MembershipTypeContext _context;

        public SqlMembershipTypeRepo(MembershipTypeContext context) : base(context)
        {
            _context = context;
        }

        public async Task<List<MembershipType>> GetAllMembershipTypes()
        {
            return await _context.MembershipType.ToListAsync();
        }

        public async Task<MembershipType> GetMembershipTypeById(int id)
        {
            return await _context.MembershipType.FirstOrDefaultAsync(m => m.Id == id);
        }

        public async Task InsertMembershipType(MembershipType membershipType)
        {
            if (membershipType == null)
            {
                throw new ArgumentNullException(nameof(membershipType));
            }
            await _context.MembershipType.AddAsync(membershipType);
        }

        public void UpdateMembershipType(MembershipType membershipType)
        {
            // no necessary work right now
        }
    }
}

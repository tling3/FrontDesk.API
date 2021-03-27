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
    public class SqlMembershipTypeRepo : BaseRepo<FrontDeskContext>, IMembershipTypeRepo
    {
        private readonly FrontDeskContext _context;

        public SqlMembershipTypeRepo(FrontDeskContext context) : base(context)
        {
            _context = context;
        }

        public async Task<IEnumerable<MembershipTypeModel>> GetAllMembershipTypes()
        {
            return await _context.MembershipType.ToListAsync();
        }

        public async Task<MembershipTypeModel> GetMembershipTypeById(int id)
        {
            return await _context.MembershipType.FirstOrDefaultAsync(m => m.Id == id);
        }

        public async Task<bool> InsertMembershipType(MembershipTypeModel domainModel)
        {
            if (domainModel == null)
                throw new ArgumentNullException(nameof(domainModel));

            await _context.MembershipType.AddAsync(domainModel);
            return SaveChanges();
        }

        public void UpdateMembershipType(MembershipTypeModel membershipType)
        {
            // intentionally left blank
            // no necessary work right now
        }

        public bool DeleteMembershipType(MembershipTypeModel domainModel)
        {
            if (domainModel == null)
                throw new ArgumentNullException();

            _context.Remove(domainModel);
            return SaveChanges();
        }
    }
}

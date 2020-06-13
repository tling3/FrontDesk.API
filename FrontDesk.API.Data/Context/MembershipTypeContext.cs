using FrontDesk.API.Models.Domain;
using Microsoft.EntityFrameworkCore;

namespace FrontDesk.API.Data.Context
{
    public class MembershipTypeContext : DbContext
    {
        public MembershipTypeContext(DbContextOptions<MembershipTypeContext> options) : base(options)
        {

        }

        public DbSet<MembershipType> MembershipType { get; set; }
    }
}

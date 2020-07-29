using FrontDesk.API.Models.Domain;
using Microsoft.EntityFrameworkCore;

namespace FrontDesk.API.Data.Context
{
    public class FrontDeskContext : DbContext
    {
        public FrontDeskContext(DbContextOptions<FrontDeskContext> options) : base(options)
        {

        }

        public DbSet<Attendance> Attendance { get; set; }
        public DbSet<Member> Member { get; set; }
        public DbSet<MembershipType> MembershipType { get; set; }
        public DbSet<Session> Session { get; set; }
        public DbSet<Weekday> Weekday { get; set; }
    }
}

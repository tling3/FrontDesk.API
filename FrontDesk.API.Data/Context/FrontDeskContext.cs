using FrontDesk.API.Models.Domain;
using Microsoft.EntityFrameworkCore;

namespace FrontDesk.API.Data.Context
{
    public class FrontDeskContext : DbContext
    {
        public FrontDeskContext(DbContextOptions<FrontDeskContext> options) : base(options)
        {

        }
        public DbSet<AttendanceModel> Attendance { get; set; }
        public DbSet<MemberModel> Member { get; set; }
        public DbSet<MembershipTypeModel> MembershipType { get; set; }
        public DbSet<Session> Session { get; set; }
        public DbSet<Weekday> Weekday { get; set; }
    }
}

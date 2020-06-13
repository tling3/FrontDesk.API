using FrontDesk.API.Models.Domain;
using Microsoft.EntityFrameworkCore;

namespace FrontDesk.API.Data.Context
{
    public class AttendanceContext : DbContext
    {
        public AttendanceContext(DbContextOptions<AttendanceContext> options) : base(options)
        {

        }

        public DbSet<Attendance> Attendance { get; set; }
    }
}

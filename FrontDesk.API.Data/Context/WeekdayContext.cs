using FrontDesk.API.Models.Domain;
using Microsoft.EntityFrameworkCore;

namespace FrontDesk.API.Data.Context
{
    public class WeekdayContext : DbContext
    {
        public WeekdayContext(DbContextOptions<WeekdayContext> options) : base(options)
        {

        }

        public DbSet<Weekday> Weekday { get; set; }
    }
}

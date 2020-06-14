using FrontDesk.API.Models.Domain;
using Microsoft.EntityFrameworkCore;

namespace FrontDesk.API.Data.Context
{
    public class SessionContext : DbContext
    {
        public SessionContext(DbContextOptions<SessionContext> options) : base(options)
        {

        }

        public DbSet<Session> Session { get; set; }
    }
}

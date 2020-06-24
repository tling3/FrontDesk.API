using FrontDesk.API.Data.Context;
using FrontDesk.API.Data.Interfaces;
using FrontDesk.API.Models.Domain;
using Microsoft.EntityFrameworkCore;
using System.Collections.Generic;
using System.Threading.Tasks;

namespace FrontDesk.API.Data.Repositories
{
    public class SqlWeekdayRepo : IWeekdayRepo
    {
        private readonly WeekdayContext _context;

        public SqlWeekdayRepo(WeekdayContext context)
        {
            _context = context;
        }
        public async Task<List<Weekday>> GetAllWeekdays()
        {
            return await _context.Weekday.ToListAsync();
        }

        public async Task<Weekday> GetWeekdayById(int id)
        {
            return await _context.Weekday.FirstOrDefaultAsync(w => w.Id == id);
        }
    }
}

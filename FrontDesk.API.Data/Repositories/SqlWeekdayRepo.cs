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
    public class SqlWeekdayRepo : BaseRepo<WeekdayContext>, IWeekdayRepo
    {
        private readonly WeekdayContext _context;

        public SqlWeekdayRepo(WeekdayContext context) : base(context)
        {
            _context = context;
        }

        public async Task<IEnumerable<Weekday>> GetAllWeekdays()
        {
            return await _context.Weekday.ToListAsync();
        }

        public async Task<Weekday> GetWeekdayById(int id)
        {
            return await _context.Weekday.FirstOrDefaultAsync(w => w.Id == id);
        }

        public async Task InsertWeekday(Weekday weekdayInsertModel)
        {
            if (weekdayInsertModel == null)
            {
                throw new ArgumentNullException(nameof(weekdayInsertModel));
            }
            await _context.Weekday.AddAsync(weekdayInsertModel);
        }

        public void UpdateWeekday(Weekday weekday)
        {
            // no necessary work right now
        }
    }
}

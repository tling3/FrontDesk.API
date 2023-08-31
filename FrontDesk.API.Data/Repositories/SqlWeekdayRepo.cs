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
    public class SqlWeekdayRepo : BaseRepo<FrontDeskContext>, IWeekdayRepo
    {
        private readonly FrontDeskContext _context;

        public SqlWeekdayRepo(FrontDeskContext context) : base(context)
        {
            _context = context;
        }

        public async Task<IEnumerable<WeekdayModel>> GetAllWeekdaysAsync()
        {
            return await _context.Weekday.ToListAsync();
        }

        public async Task<WeekdayModel> GetWeekdayByIdAsync(int id)
        {
            return await _context.Weekday.FirstOrDefaultAsync(w => w.Id == id);
        }

        public async Task<bool> InsertWeekdayAsync(WeekdayModel weekdayInsertModel)
        {
            if (weekdayInsertModel == null)
                throw new ArgumentNullException(nameof(weekdayInsertModel));

            await _context.Weekday.AddAsync(weekdayInsertModel);
            return SaveChanges();
        }

        public void UpdateWeekday(WeekdayModel weekday)
        {
            // intentionally left blank
            // no necessary work right now
        }

        public bool DeleteWeekday(WeekdayModel domainModel)
        {
            if (domainModel == null)
                throw new ArgumentNullException();

            _context.Remove(domainModel);

            return SaveChanges();
        }
    }
}

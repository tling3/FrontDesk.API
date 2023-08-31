using FrontDesk.API.Models.Domain;
using System.Collections.Generic;
using System.Threading.Tasks;

namespace FrontDesk.API.Data.Interfaces
{
    public interface IWeekdayRepo
    {
        bool SaveChanges();
        Task<IEnumerable<WeekdayModel>> GetAllWeekdaysAsync();
        Task<WeekdayModel> GetWeekdayByIdAsync(int id);
        Task<bool> InsertWeekdayAsync(WeekdayModel weekdayInsertDto);
        void UpdateWeekday(WeekdayModel weekday);
        bool DeleteWeekday(WeekdayModel domainModel);
    }
}

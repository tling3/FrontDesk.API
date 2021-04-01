using FrontDesk.API.Models.Domain;
using System.Collections.Generic;
using System.Threading.Tasks;

namespace FrontDesk.API.Data.Interfaces
{
    public interface IWeekdayRepo
    {
        bool SaveChanges();
        Task<IEnumerable<WeekdayModel>> GetAllWeekdays();
        Task<WeekdayModel> GetWeekdayById(int id);
        Task<bool> InsertWeekday(WeekdayModel weekdayInsertDto);
        void UpdateWeekday(WeekdayModel weekday);
        bool DeleteWeekday(WeekdayModel domainModel);
    }
}

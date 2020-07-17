using FrontDesk.API.Models.Domain;
using System.Collections.Generic;
using System.Threading.Tasks;

namespace FrontDesk.API.Data.Interfaces
{
    public interface IWeekdayRepo
    {
        bool SaveChanges();
        Task<List<Weekday>> GetAllWeekdays();
        Task<Weekday> GetWeekdayById(int id);
        Task InsertWeekday(Weekday weekdayInsertDto);
    }
}

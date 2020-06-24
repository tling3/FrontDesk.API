using FrontDesk.API.Models.Domain;
using System.Collections.Generic;
using System.Threading.Tasks;

namespace FrontDesk.API.Data.Interfaces
{
    public interface IWeekdayRepo
    {
        Task<List<Weekday>> GetAllWeekdays();

        Task<Weekday> GetWeekdayById(int id);
    }
}

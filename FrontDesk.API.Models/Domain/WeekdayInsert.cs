using System.ComponentModel.DataAnnotations;

namespace FrontDesk.API.Models.Domain
{
    public class WeekdayInsert
    {
        [Required]
        public int Id { get; set; }
        [Required]
        public string Weekday { get; set; }
        [Required]
        public string ModifiedBy { get; set; }
    }
}

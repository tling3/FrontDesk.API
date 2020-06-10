using System.ComponentModel.DataAnnotations;

namespace FrontDesk.API.Models.DTOs
{
    public class WeekdayInsertDto
    {
        [Required]
        public int Id { get; set; }
        [Required]
        public string Weekday { get; set; }
        [Required]
        public string ModifiedBy { get; set; }
    }
}

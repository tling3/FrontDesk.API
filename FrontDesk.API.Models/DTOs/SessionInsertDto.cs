using System;
using System.ComponentModel.DataAnnotations;

namespace FrontDesk.API.Models.DTOs
{
    public class SessionInsertDto
    {
        [Required]
        public string AgeLevel { get; set; }
        [Required]
        public string SessionType { get; set; }
        [Required]
        public string SessionLevel { get; set; }
        [Required]
        public string Instructor { get; set; }
        [Required]
        public DateTime StartTime { get; set; }
        [Required]
        public DateTime EndTime { get; set; }
        [Required]
        public int WeekdayId { get; set; }
        [Required]
        public string ModifiedBy { get; set; }
    }
}

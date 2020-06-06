using System;
using System.ComponentModel.DataAnnotations;

namespace FrontDesk.API.Models.Domain
{
    public class SessionInsert
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
        public TimeSpan StartTime { get; set; }
        [Required]
        public TimeSpan EndTime { get; set; }
        [Required]
        public int WeekdayId { get; set; }
        [Required]
        public string ModifiedBy { get; set; }
    }
}

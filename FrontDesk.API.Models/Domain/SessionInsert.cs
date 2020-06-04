using System;
using System.ComponentModel.DataAnnotations;

namespace FrontDesk.API.Models.Domain
{
    public class SessionInsert
    {
        [Required]
        public string AgeLevel { get; set; }
        [Required]
        public string ClassType { get; set; }
        [Required]
        public string ClassLevel { get; set; }
        [Required]
        public string Instructor { get; set; }
        [Required]
        public DateTime StartTime { get; set; }
        [Required]
        public DateTime EndTime { get; set; }
        [Required]
        public int WeekdayNameId { get; set; }
        [Required]
        public string ModifiedBy { get; set; }
    }
}

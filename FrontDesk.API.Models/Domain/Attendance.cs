using System;
using System.ComponentModel.DataAnnotations;

namespace FrontDesk.API.Models.Domain
{
    public class Attendance : AttendanceUpdate
    {
        [Required]
        public DateTime CreatedDate { get; set; }
        [Required]
        public DateTime ModifiedDate { get; set; }
    }
}

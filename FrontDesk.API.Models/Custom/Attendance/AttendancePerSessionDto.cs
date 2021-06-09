using System;
using System.ComponentModel.DataAnnotations;

namespace FrontDesk.API.Models.Custom.Attendance
{
    public class AttendancePerSessionDto
    {
        [Key]
        public int Id { get; set; }
        [Required]
        public int SessionId { get; set; }
        [Required]
        public int MemberId { get; set; }
        [Required]
        public DateTime SessionDate { get; set; }
        [Required]
        public string FirstName { get; set; }
        [Required]
        public string LastName { get; set; }
        [Required]
        public string Email { get; set; }
    }
}

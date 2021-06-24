using System;
using System.ComponentModel.DataAnnotations;

namespace FrontDesk.API.Models.DTOs
{
    public class AttendanceInsertDto
    {
        [Required]
        public int SessionId { get; set; }
        [Required]
        public int MemberId { get; set; }
        [Required]
        public DateTime SessionDate { get; set; }
        [Required]
        public string ModifiedBy { get; set; }
    }
}

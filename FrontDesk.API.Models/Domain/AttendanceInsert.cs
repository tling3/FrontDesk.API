using System;
using System.ComponentModel.DataAnnotations;

namespace FrontDesk.API.Models.Domain
{
    public class AttendanceInsert
    {
        [Required]
        public int MemberId { get; set; }
        [Required]
        public DateTime SessionDate { get; set; }
        [Required]
        public string ModifiedBy { get; set; }
    }
}

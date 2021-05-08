using FrontDesk.API.Models.Base;
using System;
using System.ComponentModel.DataAnnotations;

namespace FrontDesk.API.Models.Domain
{
    public class AttendanceModel : BaseDomain
    {
        [Key]
        public int Id { get; set; }
        [Required]
        public int MemberId { get; set; }
        [Required]
        public DateTime SessionDate { get; set; }
        [Required]
        public string ModifiedBy { get; set; }
    }
}

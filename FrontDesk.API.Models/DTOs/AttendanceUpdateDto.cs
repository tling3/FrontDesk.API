using System.ComponentModel.DataAnnotations;

namespace FrontDesk.API.Models.DTOs
{
    public class AttendanceUpdateDto : AttendanceInsertDto
    {
        [Key]
        public int Id { get; set; }
    }
}

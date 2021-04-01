using System;

namespace FrontDesk.API.Models.DTOs
{
    public class AttendanceReadDto : AttendanceUpdateDto
    {
        public DateTime CreatedDate { get; set; }
        public DateTime ModifiedDate { get; set; }
    }
}

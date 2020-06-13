using System;

namespace FrontDesk.API.Models.DTOs
{
    public class WeekdayReadDto : WeekdayUpdateDto
    {
        public DateTime CreatedDate { get; set; }
        public DateTime ModifiedDate { get; set; }
    }
}

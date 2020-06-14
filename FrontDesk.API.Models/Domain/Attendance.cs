using System;

namespace FrontDesk.API.Models.Domain
{
    public class Attendance : AttendanceUpdate
    {
        public DateTime CreatedDate { get; set; }
        public DateTime ModifiedDate { get; set; }
    }
}

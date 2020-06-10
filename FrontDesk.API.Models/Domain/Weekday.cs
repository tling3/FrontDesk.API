using System;

namespace FrontDesk.API.Models.Domain
{
    public class Weekday : WeekdayUpdate
    {
        public DateTime CreatedDate { get; set; }
        public DateTime ModifiedDate { get; set; }
    }
}

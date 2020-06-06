using System;

namespace FrontDesk.API.Models.Domain
{
    public class Session : SessionUpdate
    {
        public DateTime CreatedDate { get; set; }
        public DateTime ModifiedDate { get; set; }
    }
}

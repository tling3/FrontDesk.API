using System;

namespace FrontDesk.API.Models.Domain
{
    public class Member : MemberUpdate
    {
        public DateTime CreatedDate { get; set; }
        public DateTime ModifiedDate { get; set; }
    }
}

using System;

namespace FrontDesk.API.Models.Domain
{
    public class MembershipType : MembershipTypeUpdate
    {
        public DateTime CreatedDate { get; set; }
        public DateTime ModifiedDate { get; set; }
    }
}

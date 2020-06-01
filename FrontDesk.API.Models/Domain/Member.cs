using FrontDesk.API.Models.Request;
using System;

namespace FrontDesk.API.Models.Domain
{
    public class Member : MemberUpdateRequest
    {
        public DateTime CreatedDate { get; set; }
        public DateTime ModifiedDate { get; set; }
    }
}

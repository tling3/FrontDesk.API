using System;

namespace FrontDesk.API.Models.DTOs
{
    public class MembershipTypeReadDto : MembershipTypeUpdateDto
    {
        public int Id { get; set; }
        public DateTime CreatedDate { get; set; }
        public DateTime ModifiedDate { get; set; }
    }
}

using System;

namespace FrontDesk.API.Models.DTOs
{
    public class MemberReadDto : MemberUpdateDto
    {
        public DateTime CreatedDate { get; set; }
        public DateTime ModifiedDate { get; set; }
    }
}

using System;

namespace FrontDesk.API.Models.DTOs
{
    public class MemberReadDto : MemberUpdateDto
    {
        public int Id { get; set; }
        public DateTime CreatedDate { get; set; }
        public DateTime ModifiedDate { get; set; }
    }
}

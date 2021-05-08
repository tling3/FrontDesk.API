using System;

namespace FrontDesk.API.Models.DTOs
{
    public class SessionReadDto : SessionUpdateDto
    {
        public DateTime CreatedDate { get; set; }
        public DateTime ModifiedDate { get; set; }
    }
}

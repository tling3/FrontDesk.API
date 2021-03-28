using System.ComponentModel.DataAnnotations;

namespace FrontDesk.API.Models.DTOs
{
    public class SessionUpdateDto : SessionInsertDto
    {
        [Key]
        public int Id { get; set; }
    }
}

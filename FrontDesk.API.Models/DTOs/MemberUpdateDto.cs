using System.ComponentModel.DataAnnotations;

namespace FrontDesk.API.Models.DTOs
{
    public class MemberUpdateDto : MemberInsertDto
    {
        [Key]
        public int Id { get; set; }
    }
}

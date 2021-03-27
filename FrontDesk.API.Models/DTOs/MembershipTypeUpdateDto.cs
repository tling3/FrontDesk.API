using System.ComponentModel.DataAnnotations;

namespace FrontDesk.API.Models.DTOs
{
    public class MembershipTypeUpdateDto : MembershipTypeInsertDto
    {
        [Key]
        public int Id { get; set; }
    }
}

using System.ComponentModel.DataAnnotations;

namespace FrontDesk.API.Models.DTOs
{
    public class MembershipTypeInsertDto
    {
        [Required]
        public string MembershipType { get; set; }
        [Required]
        public int SessionsPerMonth { get; set; }
        [Required]
        public string ModifiedBy { get; set; }
    }
}

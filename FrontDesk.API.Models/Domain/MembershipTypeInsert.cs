using System.ComponentModel.DataAnnotations;

namespace FrontDesk.API.Models.Domain
{
    public class MembershipTypeInsert
    {
        [Required]
        public string MembershipType { get; set; }
        [Required]
        public int SessionsPerMonth { get; set; }
        [Required]
        public string ModifiedBy { get; set; }
    }
}

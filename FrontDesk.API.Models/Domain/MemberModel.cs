using FrontDesk.API.Models.Base;
using System;
using System.ComponentModel.DataAnnotations;

namespace FrontDesk.API.Models.Domain
{
    public class MemberModel : BaseDomain
    {
        [Key]
        public int Id { get; set; }
        [Required]
        public string FirstName { get; set; }
        [Required]
        public string LastName { get; set; }
        [Required]
        public string Email { get; set; }
        [Required]
        public string AgeBracket { get; set; }
        [Required]
        public DateTime StartDate { get; set; }
        [Required]
        public string MembershipStatus { get; set; }
        [Required]
        public int MembershipId { get; set; }
        [Required]
        public string ModifiedBy { get; set; }
    }
}

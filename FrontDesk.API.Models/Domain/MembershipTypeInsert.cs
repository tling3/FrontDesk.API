﻿using FrontDesk.API.Models.Base;
using System.ComponentModel.DataAnnotations;

namespace FrontDesk.API.Models.Domain
{
    public class MembershipTypeInsert : BaseDomain
    {
        [Required]
        public string MembershipType { get; set; }
        [Required]
        public int SessionsPerMonth { get; set; }
        [Required]
        public string ModifiedBy { get; set; }
    }
}
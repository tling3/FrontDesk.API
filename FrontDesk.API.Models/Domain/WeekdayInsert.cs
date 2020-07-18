﻿using FrontDesk.API.Models.Base;
using System.ComponentModel.DataAnnotations;

namespace FrontDesk.API.Models.Domain
{
    public class WeekdayInsert : BaseDomain
    {
        [Required]
        public int Id { get; set; }
        [Required]
        public string Weekday { get; set; }
        [Required]
        public string ModifiedBy { get; set; }
    }
}

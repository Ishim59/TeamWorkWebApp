﻿using System.ComponentModel.DataAnnotations;

namespace TeamWorkWebApp.Models
{
    public class Group
    {
        [Key]
        public int Id { get; set; }
        [Required]
        public string TeamLead { get; set; }
        [Required]
        public string Title { get; set; }
        [Required]
        public string Description { get; set; }
        [Required]
        public string MembersJson { get; set; }
    }
}

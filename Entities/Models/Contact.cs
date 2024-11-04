﻿using System.ComponentModel.DataAnnotations;

namespace Entities.Models
{
    public class Contact
    {
        public Guid Id { get; set; }
        [Required]
        public string Name { get; set; }
        [Required]
        [EmailAddress]
        public string Email { get; set; }
        [Required]
        public string PhoneNumber { get; set; }
        public bool? Status { get; set; }
        [Required]
        public string Message { get; set; }

        public DateTime CreatedAt { get; set; } = DateTime.Now;
        public DateTime UpdatedAt { get; set; } = DateTime.Now;
    }
}

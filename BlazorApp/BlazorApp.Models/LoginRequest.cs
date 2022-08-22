﻿using System.ComponentModel.DataAnnotations;

namespace BlazorApp.Models
{
    public class LoginRequest
    {
        [Required]
        public string UserName { get; set; }

        [Required]
        public string Password { get; set; }
    }
}
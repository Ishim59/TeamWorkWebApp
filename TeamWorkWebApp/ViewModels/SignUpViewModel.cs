﻿using System.ComponentModel.DataAnnotations;

namespace TeamWorkWebApp.ViewModels
{
    public class SignUpViewModel
    {
        public string Email { get; set; }
        public string Password { get; set; }
        public string PasswordConfirmation { get; set; }
        public string Name { get; set; }
    }
}

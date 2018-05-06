using System;
using System.ComponentModel.DataAnnotations;

namespace Blog.ViewModel.Authefication{
    public class LoginViewModel
    {
        [Required]
        public string UserName { get; set; }
        [Required]
        public string Password { get; set; }
        public string Email { get; set; }
        public bool RememberMe { get; set; }
    }
}
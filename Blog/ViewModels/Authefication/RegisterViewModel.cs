using System;
using System.ComponentModel.DataAnnotations;

namespace Blog.ViewModel.Authefication{
    public class RegisterViewModel
    {
        [Required ( ErrorMessage = "Not Password")]
        [MinLength(4)]
        public string UserName { get; set; }
        [Required]
        [DataType(DataType.Password)]
        public string Password { get; set; }
        [Required]
        public string Email { get; set; }
    }
}
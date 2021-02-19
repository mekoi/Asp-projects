using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using System.ComponentModel.DataAnnotations;

namespace Irisi_Bruno_lab3.Models.ViewModels
{
    public class UserViewModel
    {
        [Required]
        public string UserName { get; set; }
        [Required(ErrorMessage = "Password is required.")]
        [UIHint("password")]
        public string UserPassword { get; set; }
        [Required(ErrorMessage = "Confirmation Password is required.")]
        [Compare("UserPassword", ErrorMessage = "Password and Confirmation Password must match.")]
        public string ConfirmPassword { get; set; }
    }
}


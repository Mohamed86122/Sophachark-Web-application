﻿using System.ComponentModel.DataAnnotations;

namespace SophaTemp.Viewmodel
{
    public class LoginVm
    {

        [Required]
        [EmailAddress]
        public string Email { get; set; }

        [Required]
        [DataType(DataType.Password)]
        public string MotDePasse { get; set; }

        [Display(Name = "Remember Me")]
        public bool RememberMe { get; set; }
    }
}

﻿using System.ComponentModel.DataAnnotations;

namespace IdentityServer.Models
{

    public class LoginModel
    {
        [Required]
        public string Username { get; set; }

        [Required]
        public string Password { get; set; }

    }
    public class SignUpModel
    {

    
    }
}

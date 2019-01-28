using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Web;

namespace WhatsUpV2.Models
{
    public class LogIn
    {
        [Required]
        public string Username;

        [Required]
        public string Password;
    }
}
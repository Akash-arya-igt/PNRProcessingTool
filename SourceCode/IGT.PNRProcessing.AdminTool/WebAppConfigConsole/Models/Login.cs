using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Linq;
using System.Web;

namespace WebAppConfigConsole.Models
{
    public class Login
    {
        [DisplayName("User ID")]
        public string Name { get; set; }
        [DisplayName("Password")]
        public string Password { get; set; }
        public string Email { get; set; }
    }
}
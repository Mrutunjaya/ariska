using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Web;

namespace Aeriksa.Models
{
    public class loginModel
    {
        [Required(ErrorMessage = "UserName is required")]
        public string userid { get; set; }
        [Required(ErrorMessage = "Password is required")]
        [DataType(DataType.Password)]
        public string password { get; set; }
    }
}
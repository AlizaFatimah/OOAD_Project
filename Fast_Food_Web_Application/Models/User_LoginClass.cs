using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;

using System.ComponentModel.DataAnnotations;
namespace Fast_Food_Web_Application.Models
{
    public class User_LoginClass
    {
        public int Login_id { get; set; }
        public int Reg_id { get; set; }
        [Display(Name = "Email")]
        [Required(ErrorMessage = "*This field is required")]
        [EmailAddress(ErrorMessage = "*Invalid Email Address")]
        public string Email { get; set; }

        [Display(Name = "Password")]
        [Required(ErrorMessage = "*This field is required")]
        [DataType(DataType.Password)]

        public string Password { get; set; }
        public string LoginErrorMessage { get; set; }

    }
}
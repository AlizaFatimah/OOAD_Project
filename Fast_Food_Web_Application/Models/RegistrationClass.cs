using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;

using System.ComponentModel.DataAnnotations;
namespace Fast_Food_Web_Application.Models
{
    public class RegistrationClass
    {
        public int Reg_id { get; set; }

        [Required(ErrorMessage = "*UserName required")]
        [Display(Name = "User Name")]
        [StringLength(maximumLength: 8, MinimumLength = 5, ErrorMessage = " UserName Length must be  mini 5 & max 8 ")]
        public string UserName { get; set; }

        [Required(ErrorMessage = "*Email-Address required")]
        [Display(Name = "Email")]
        [EmailAddress(ErrorMessage = "*Invalid Email Address")]
        public string Email { get; set; }

        [Required(ErrorMessage = "*Password required")]
        [Display(Name = "Password")]
        [DataType(DataType.Password)]
        [StringLength(maximumLength: 8, MinimumLength = 5, ErrorMessage = " Password Length must be  mini 5 & max 8 ")]

        public string Password { get; set; }
        [Required(ErrorMessage = "*RePassword required")]
        [Display(Name = "Re-Password")]
        [DataType(DataType.Password)]
        [Compare("Password")]



        public string Repassword { get; set; }
        [Required(ErrorMessage = "*Select the Gender")]
        [Display(Name = "Gender")]
        public string Gender { get; set; }



    }
}
using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Web;

namespace User_Web_Portal.Models.Account
{
    public class RegisterModel
    {
        [Display(Name = "Full Name:")]
        [Required(ErrorMessage ="Full Name is required")]
        public string FullName { get; set; }

        [Display(Name = "User Name:")]
        [Required(ErrorMessage = "User Name is required")]
        public string UserName { get; set; }

        [Display(Name = "Password:")]
        [Required(ErrorMessage = "Password is required")]
        [DataType(DataType.Password)]
        public string Password { get; set; }

        [Display(Name = "Confirm  Password:")]
        [Required(ErrorMessage = "Confirm Password is required")]
        [Compare(otherProperty:"Password", ErrorMessage = "Password does not match.")]
        [DataType(DataType.Password)]
        public string ConfirmPassword { get; set; }

        [Display(Name = "Email:")]
        [Required(ErrorMessage = "Email is required")]
        public string Email { get; set; }

        [Display(Name = "Role:")]
        [Required(ErrorMessage = "Role is required")]
        [UIHint("RolesComboBox")]
        public string Role { get; set; }

    }
}
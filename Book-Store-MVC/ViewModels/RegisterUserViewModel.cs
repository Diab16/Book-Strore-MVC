using Book_Store_MVC.Models;
using Book_Store_MVC.Validation;
using System.ComponentModel.DataAnnotations;
using Microsoft.EntityFrameworkCore;

namespace Day2.ViewModels
{
    public class RegisterUserViewModel
    {        
        public string Username { get; set; }
        [Display(Name ="Email")]
        [UniqueEmail]
        [RegularExpression("^[a-zA-Z0-9]+@[a-z]+.com$",ErrorMessage ="Your Email Must Have '@' and end with '.com'")]
        public string UserEmail { get; set; }
        [DataType(DataType.Password)]
        public string Password { get; set; }
        [DataType(DataType.Password)]
        [Compare("Password")]
        public string ConfirmPassword { get; set; }
        public string Address { get; set; }
    }
}

using System.ComponentModel.DataAnnotations;

namespace Day2.ViewModels
{
    public class RegisterUserViewModel
    {
        public string Username { get; set; }
        [Display(Name ="Email")]
        [RegularExpression("^[a-z0-9]+@[a-z]+.com$",ErrorMessage ="Your Email Must Have '@' and end with '.com'")]
        public string UserEmail { get; set; }
        [DataType(DataType.Password)]
        public string Password { get; set; }
        [DataType(DataType.Password)]
        [Compare("Password")]
        public string ConfirmPassword { get; set; }
        public string Address { get; set; }
    }
}

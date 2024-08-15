using Microsoft.AspNetCore.Identity;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;

namespace Book_Store_MVC.ViewModels
{
    public class UserManagerViewModel
    {
        public string Username { get; set; }
        [Display(Name = "Email")]
        [RegularExpression("^[a-z0-9]+@[a-z]+.com$", ErrorMessage = "Your Email Must Have '@' and end with '.com'")]
        public string UserEmail { get; set; }
        [DataType(DataType.Password)]
        public string Password { get; set; }
        [DataType(DataType.Password)]
        [Compare("Password")]
        public string ConfirmPassword { get; set; }
        public string Address { get; set; }
        [ForeignKey("Role")]
        public string roleId { get; set; }
        public string roleName { get; set; }
        public IdentityRole? Role { get; set; }
        public List<IdentityRole>? Roles { get; set; }
    }
}

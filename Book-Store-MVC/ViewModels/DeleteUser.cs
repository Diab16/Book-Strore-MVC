using Microsoft.AspNetCore.Identity;
using System.ComponentModel.DataAnnotations.Schema;
using System.ComponentModel.DataAnnotations;

namespace Book_Store_MVC.ViewModels
{
	public class DeleteUser
	{
		public string? Username { get; set; }
		[Display(Name = "Email")]
		public string? UserEmail { get; set; }

		public string? Password { get; set; }
		public string? ConfirmPassword { get; set; }
		public string? Address { get; set; }
		[ForeignKey("Role")]
		public string? roleId { get; set; }
		public string? roleName { get; set; }
		public IdentityRole? Role { get; set; }
		public List<IdentityRole>? Roles { get; set; }
	}
}

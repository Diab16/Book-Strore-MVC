using System.ComponentModel.DataAnnotations;

namespace Day2.ViewModels
{
    public class LoginViewModel
    {
        
        public string UserEmail { get; set; }
        [DataType(DataType.Password)]
        public string Password { get; set; }
        [Display(Name ="Remember Me ?")]
        public bool Remember_Me { get; set; }
    }
}

using System.ComponentModel.DataAnnotations;

namespace ApiInventoryControl.ViewModels
{
    public class LoginViewModel
    {
        [Required(ErrorMessage = "Enter the email")]
        [EmailAddress(ErrorMessage = "Invalid email")]
        public string Email { get; set; }

        [Required(ErrorMessage = "Enter the password")]
        public string Password { get; set; }
    }
}

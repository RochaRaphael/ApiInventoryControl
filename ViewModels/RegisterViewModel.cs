using System.ComponentModel.DataAnnotations;

namespace ApiInventoryControl.ViewModels
{
    public class RegisterViewModel
    {
        [Required(ErrorMessage = "Name is required")]
        public string Name { get; set; }

        [Required(ErrorMessage = "E-mail is required")]
        [EmailAddress(ErrorMessage = "E-mail is required")]
        public string Email { get; set; }
    }
}

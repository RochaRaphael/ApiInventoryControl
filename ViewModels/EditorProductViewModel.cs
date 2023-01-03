using ApiInventoryControl.Models;
using System.ComponentModel.DataAnnotations;

namespace ApiInventoryControl.ViewModels
{
    public class EditorProductViewModel
    {
        [Required(ErrorMessage = "Name is required")]
        [StringLength(50, MinimumLength = 3, ErrorMessage = "This field must contain between 3 and 50 characters")]
        public string Name { get; set; }

        [Required(ErrorMessage = "Price is required")]
        public double Price { get; set; }

        [Required(ErrorMessage = "Quantity is required")]
        public int Quantity { get; set; }

        [Required(ErrorMessage = "Category is required")]
        public string Category { get; set; }
    }
}

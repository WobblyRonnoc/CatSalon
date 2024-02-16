using System.ComponentModel.DataAnnotations;
using CatSalon.Models.DataAccess;

namespace CatSalon.Models.ViewModels
{
    public class OwnerLogin
    {
        [Required(ErrorMessage = "Email is required")]
        [MaxLength(100, ErrorMessage = "Email exceeds 100 character limit")]
        [RegularExpression("^[\\w\\-.]+@[\\w\\-.]+\\.+[a-zA-Z]{2,4}$", ErrorMessage = "Invalid email address")]
        public string Email { get; set; }

        [Required(ErrorMessage = "Password is required")]
        [RegularExpression("^(?=.*\\d)(?=.*[a-z])(?=.*[A-Z])(?=.*[a-zA-Z]).{6,}$", ErrorMessage = "Invalid Password")]
        public string Password { get; set; } = null!;
    }
}

using System.ComponentModel.DataAnnotations;

namespace CatSalon.Models.MetaDataAccess
{
    public class OwnerMetaData
    {

        [Required(ErrorMessage = "Name is required")]
        [MaxLength(50, ErrorMessage = "Name exceeds 50 character limit")]
        [MinLength(2, ErrorMessage = "Name must be at least 2 characters long")]
        [RegularExpression("^[a-zA-Z]+(([',. -][a-zA-Z ])?[a-zA-Z]*)*$", ErrorMessage = "Name cannot contain numbers or isolated/consecutive dashes")]
        public string Name { get; set; } = null!;

        [Required(ErrorMessage = "Phone number is required")]
        [RegularExpression("^(\\d{3}-){2}[\\d]{4}$", ErrorMessage = "Invalid phone number. use format: 123-456-7890")]
        public string Phone { get; set; } = null!;

        [Required(ErrorMessage = "Email is required")]
        [MaxLength(100, ErrorMessage = "Email exceeds 100 character limit")]
        [RegularExpression("^[\\w\\-.]+@[\\w\\-.]+\\.+[a-zA-Z]{2,4}$", ErrorMessage = "Invalid email address")]
        public string Email { get; set; }

        [Required(ErrorMessage = "Password is required")]
        [RegularExpression("^(?=.*\\d)(?=.*[a-z])(?=.*[A-Z])(?=.*[a-zA-Z]).{6,}$",ErrorMessage = "Password must be at least 6 characters long, containing: one upper-case character, one lower-case, and one digit")]
        public string Password { get; set; } = null!;
    }
}

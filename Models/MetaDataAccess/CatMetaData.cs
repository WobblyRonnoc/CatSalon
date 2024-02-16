using CatSalon.Models.DataAccess;
using System.ComponentModel.DataAnnotations;

namespace CatSalon.Models.MetaDataAccess
{
    public partial class CatMetaData
    {
        [Required(ErrorMessage = "Name is required")]
        [MinLength(2, ErrorMessage = "Name must be at least 2 characters long")]
        [RegularExpression("^[a-zA-Z]+(([',. -][a-zA-Z ])?[a-zA-Z]*)*$", ErrorMessage = "Name cannot contain numbers or isolated/consecutive dashes")]
        public string Name { get; set; } = null!;

        [Required(ErrorMessage = "Breed is required")]
        [MinLength(3, ErrorMessage = "Breed must be at least 3 characters long")]
        [RegularExpression("^[a-zA-Z]+(([',. -][a-zA-Z ])?[a-zA-Z]*)*$", ErrorMessage = "Breed cannot contain numbers or isolated/consecutive dashes")]
        public string Breed { get; set; } = null!;

        [Required(ErrorMessage = "Birth date is required")]
        [DataType(DataType.Date)]
        public DateTime BirthDate { get; set; }

        [MaxLength(250,ErrorMessage = "250 character limit exceeded")]
        public string? HealthCondition { get; set; }
    }
}

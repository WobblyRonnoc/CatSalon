using CatSalon.Models.MetaDataAccess;
using Microsoft.AspNetCore.Mvc;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;


namespace CatSalon.Models.DataAccess
{
    [ModelMetadataType(typeof(CatMetaData))]
    public partial class Cat
    {
        [DatabaseGenerated(DatabaseGeneratedOption.Identity)]
        public int Id { get; set; }
        public int OwnerId { get; set; }
        public string Name { get; set; } = null!;
        public string Breed { get; set; } = null!;
        [DataType(DataType.Date)]
        public DateTime BirthDate { get; set; } 
        public string? HealthCondition { get; set; }
        public bool Fixed { get; set; }
        public bool Declawed { get; set; }

    }

}

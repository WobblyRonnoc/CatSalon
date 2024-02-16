using CatSalon.Models.DataAccess;
using CatSalon.Models.MetaDataAccess;
using Microsoft.AspNetCore.Mvc;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;

namespace CatSalon.Models.ViewModels
{
    [ModelMetadataType(typeof(CatMetaData))]

    public partial class OwnerCat
    {
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


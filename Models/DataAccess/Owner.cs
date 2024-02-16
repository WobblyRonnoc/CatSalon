using CatSalon.Models.MetaDataAccess;
using Microsoft.AspNetCore.Mvc;
using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;

namespace CatSalon.Models.DataAccess
{
    [ModelMetadataType(typeof(OwnerMetaData))]
    public partial class Owner
    {
        public Owner()
        {
            Cats = new HashSet<Cat>();
        }

        public Owner(int id)
        {
            Id = id;
            Cats = new HashSet<Cat>();
        }


        [NotMapped]
        public List<Cat> Cat
        {
            get
            {
                List<Cat> Cats = new List<Cat>();
                using (CatSalonContext context = new CatSalonContext())
                {
                    Cats = (from c in context.Cats
                            where c.OwnerId == Id
                            select c).ToList<Cat>();
                }
                return Cats;
            }
        }

        public int Id { get; set; }
        public string Name { get; set; } = null!;
        public string Phone { get; set; } = null!;
        public string Email { get; set; }
        public string Password { get; set; } = null!;

        public virtual ICollection<Cat> Cats { get; set; }
    }
}

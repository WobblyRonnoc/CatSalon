using CatSalon.Models.MetaDataAccess;
using Microsoft.AspNetCore.Mvc;
using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;

namespace CatSalon.Models.DataAccess
{
    [ModelMetadataType(typeof(AppointmentMetaData))]
    public partial class Appointment
    {
        public Appointment()
        {
            Services = new HashSet<Service>();
            Random r = new Random();
            using (CatSalonContext context = new CatSalonContext())
            {
                List<int> Ids = context.Employees.Select(x => x.Id).ToList<int>();
                int min = Ids.Min();
                int max = Ids.Max();
                this.EmployeeId = r.Next(min, max);
            }
                
        }
        [DatabaseGenerated(DatabaseGeneratedOption.Identity)]
        public int Id { get; set; }
        public int CatId { get; set; }

        [NotMapped]
        public Cat Cat
        {
            get
            {
                using (CatSalonContext context = new CatSalonContext())
                {
                    return context.Cats.Find(CatId);
                }
            }
        }

        public int EmployeeId { get; set; }  
        [DataType(DataType.DateTime)]
        public DateTime ScheduledDate { get; set; }

        public virtual ICollection<Service> Services { get; set; }
        public List<AppointmentService>? AppointmentService { get; set; }
    }
}

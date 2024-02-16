using System;
using System.Collections.Generic;

namespace CatSalon.Models.DataAccess
{
    public partial class Service
    {
        public Service()
        {
            Appointments = new HashSet<Appointment>();
        }

        public int Id { get; set; }
        public string Title { get; set; } = null!;
        public string? Description { get; set; }
        public decimal? Duration { get; set; }
        public decimal? Price { get; set; }

        public virtual ICollection<Appointment> Appointments { get; set; }
        public List<AppointmentService>? AppointmentsService { get; set; }
    }
}

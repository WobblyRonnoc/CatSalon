using CatSalon.Models.DataAccess;

namespace CatSalon.Models.ViewModels
{
    public class AppointmentDetails
    {
        public int? AppointmentId { get; set; }
        public Appointment Appointment { get; set; } 
        public List<int> ServiceIds { get; set; }

        public List<AppointmentService> appointmentServices { get; set; }
        public List<Service> Services { get; set; }

        public AppointmentDetails(int? id)
        {
            if (id == null)
            {
                Console.WriteLine("error: no ID supplied");
                return;
            }

            AppointmentId = id;
            Services = new List<Service>();
            
            using (CatSalonContext context = new CatSalonContext())
            {
                //find the appointment using passed Id
                this.Appointment = context.Appointments.Find(AppointmentId);

                //gather list of services
                List<AppointmentService> aps = (from x in context.AppointmentServices
                                                where x.AppointmentId == AppointmentId
                                                select x).ToList<AppointmentService>();

                foreach (var item in aps)
                {
                    Service s = context.Services.Find(item.ServiceId);
                    Services.Add(s);
                }
            }
        }
    }
}

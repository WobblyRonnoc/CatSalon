using CatSalon.Models.DataAccess;

namespace CatSalon.Models.ViewModels
{
    public class AppointmentServiceSelections
    {
        public Appointment Appointment { get; set; }

        public List<ServiceSelection> ServiceSelections { get; set; }
        public AppointmentServiceSelections()
        {
            Appointment = new Appointment();
            ServiceSelections = new List<ServiceSelection>();
            CatSalonContext context = new CatSalonContext();
            foreach (Service s in context.Services)
            {
                ServiceSelection serviceSelection = new ServiceSelection(s);
                this.ServiceSelections.Add(serviceSelection);
            }
        }

        // *Disclaimer*
        //*HERE* suddenly calls empty constructor of ServiceSelection -> nuking all service info prepared after Post
        // Removed Service details from appointment booking form to avoid broken interface* 

        public AppointmentServiceSelections(Appointment appointment)
        {
            Appointment = appointment;
            ServiceSelections = new List<ServiceSelection>();
            CatSalonContext context = new CatSalonContext();
            
            var optionService = context.Services;
            var chosenServices = appointment.Services;

            foreach (Service s in context.Services)
            {
                bool check = false;
                if (chosenServices.Select(x => x.Id).Contains(s.Id))
                {
                    check = true;
                }

                ServiceSelection serviceSelection = new ServiceSelection(s, check);
                ServiceSelections.Add(serviceSelection);

            }
        }
    }
}

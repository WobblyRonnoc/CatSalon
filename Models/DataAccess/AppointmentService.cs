using System.ComponentModel.DataAnnotations.Schema;

namespace CatSalon.Models.DataAccess
{
    public class AppointmentService
    {
        [DatabaseGenerated(DatabaseGeneratedOption.Identity)]
        public int Id { get; set; }
        public int AppointmentId { get; set; }
        public int ServiceId { get; set; }

        public AppointmentService()
        {
           
        }
       
    }
}

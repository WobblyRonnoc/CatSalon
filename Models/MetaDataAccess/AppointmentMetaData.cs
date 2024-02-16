using System.ComponentModel.DataAnnotations;

namespace CatSalon.Models.MetaDataAccess
{
    public class AppointmentMetaData
    {

        private DateTime Min { get; set; } = DateTime.Now.AddDays(1).Date;
        private DateTime Max { get; set; } = DateTime.Now.AddYears(1).Date;


       
        [Required]
        [DataType(DataType.DateTime)]
        public DateTime ScheduledDate { get; set; }





    }
}

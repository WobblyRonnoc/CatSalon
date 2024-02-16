using CatSalon.Models.DataAccess;

namespace CatSalon.Models.ViewModels
{
    public class ServiceSelection
    {
        public Service Service { get; set; }
        public bool Selected { get; set; }
        public ServiceSelection()
        {
        }
        public ServiceSelection(Service service, bool selected = false)
        {
            this.Service = service;
            Selected = selected;
        }
    }
}

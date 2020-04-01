using System.ComponentModel.DataAnnotations;

namespace Application.WebApp.ViewModels
{
    public class CreateVmViewModel
    {
        [Required]
        public string Name { get; set; }
        
        [Required(AllowEmptyStrings = false)]
        public string Type { get; set; }
        
        public string Comment { get; set; }
    }
}
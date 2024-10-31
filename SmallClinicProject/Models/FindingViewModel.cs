using System.ComponentModel.DataAnnotations;

namespace SmallClinicProject.Models
{
    public class FindingViewModel
    {
        public int FindingId { get; set; }  
        public int AdmissionId { get; set; }

        [Required(ErrorMessage = "Mišljenje ljekara je obavezno polje.")]
        public string Description { get; set; }
        public DateTime DateTimeOfFinding { get; set; }
    }
}

namespace SmallClinicProject.Models
{
    public class Patient
    {
        public int PatientId { get; set; }
        public string FirstName { get; set; }
        public string LastName { get; set; }
        public string UniqueIdentificationNumber { get; set; }
        public DateTime DateOfBirth { get; set; }
        public string Address { get; set; }
        public string PhoneNumber { get; set; }
        public int GenderId { get; set; }
        public CGender Gender { get; set; }
        public ICollection<PatientsAdmission> PatientsAdmissions { get; set; } = new List<PatientsAdmission>();
    }
}


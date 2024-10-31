using SmallClinicProject.Models;

public class PatientsAdmission
{
    public int PatientsAdmissionId { get; set; }
    public int PatientId { get; set; }
    public int DoctorId { get; set; }
    public DateTime PatientsAdmissionDateTime { get; set; }
    public bool EmergencyAdmission { get; set; }
    public Patient Patient { get; set; }
    public Doctor Doctor { get; set; }
    public ICollection<Finding> Findings { get; set; } = new List<Finding>(); // Initialize as an empty list
}

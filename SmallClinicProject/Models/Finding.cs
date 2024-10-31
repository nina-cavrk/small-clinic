public class Finding
{
    public int FindingId { get; set; }
    public int AdmissionId { get; set; } 
    public string Description { get; set; }
    public DateTime DateTimeOfFinding { get; set; }
    public PatientsAdmission Admission { get; set; }
}

using System;
using System.Collections.Generic;

namespace SmallClinicProject.Models
{
    public class AdmissionFilterViewModel
    {
        public IEnumerable<PatientsAdmission> Admissions { get; set; }
        public DateTime? FromDate { get; set; }
        public DateTime? ToDate { get; set; }
        public string SortOrder { get; set; }
        public string PatientName { get; set; }
        public string DoctorName { get; set; }
    }
}

using System;
using System.Collections.Generic;
using SmallClinicProject.Models;

namespace SmallClinicProject.Models
{
    public class Doctor
    {
        public int DoctorId { get; set; } 
        public string FirstName { get; set; }
        public string LastName { get; set; }
        public string UniqueIdentificationNumber { get; set; }
        public DateTime? DateOfBirth { get; set; }
        public string Address { get; set; }
        public string PhoneNumber { get; set; }
        public string DoctorCode { get; set; }

        // Foreign keys and relationships
        public int GenderId { get; set; }
        public CGender Gender { get; set; }
        public int TitleId { get; set; }
        public CTitle Title { get; set; }
        public ICollection<PatientsAdmission> PatientsAdmissions { get; set; } = new List<PatientsAdmission>();
    }
}

using System;
using System.ComponentModel.DataAnnotations;
using Microsoft.AspNetCore.Mvc.Rendering;
using System.Collections.Generic;

namespace SmallClinicProject.Models
{
    public class AdmissionViewModel
    {
        [Required(ErrorMessage = "Morate odabrati pacijenta.")]
        public int PatientId { get; set; }

        [Required(ErrorMessage = "Morate odabrati ljekara.")]
        public int DoctorId { get; set; }

        [Required(ErrorMessage = "Datum prijema je obavezan podataka")]
        [DataType(DataType.DateTime)]
        public DateTime ReceptionDateTime { get; set; }
        public bool EmergencyAdmission { get; set; }
        public IEnumerable<SelectListItem> Patients { get; set; } = new List<SelectListItem>();
        public IEnumerable<SelectListItem> Doctors { get; set; } = new List<SelectListItem>();
    }
}

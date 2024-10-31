using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.Rendering;
using Microsoft.EntityFrameworkCore;
using SmallClinicProject.Models;
using System;
using System.Linq;

namespace SmallClinicProject.Controllers
{
    public class AdmissionsController : Controller
    {
        private readonly SmallClinicDB _context;

        public AdmissionsController(SmallClinicDB context)
        {
            _context = context;
        }

        // GET: Admissions/Index
        [HttpGet]
        public IActionResult Index(string sortOrder, DateTime? fromDate, DateTime? toDate, string patientName, string doctorName)
        {
            fromDate ??= DateTime.Today;
            toDate ??= DateTime.Today.AddDays(7);
            // Date validation
            if (fromDate > toDate)
            {
                ModelState.AddModelError("DateRange", "Datum do ne može biti stariji od datuma od.");
            }

           var admissions = _context.PatientsAdmissions
                .Include(a => a.Patient)
                .Include(a => a.Doctor)
                .Include(a => a.Findings)
                .Where(a => a.PatientsAdmissionDateTime >= fromDate && a.PatientsAdmissionDateTime <= toDate);

            // Filter
            if (!string.IsNullOrEmpty(patientName))
            {
                admissions = admissions.Where(a => (a.Patient.FirstName + " " + a.Patient.LastName).Contains(patientName));
            }

            if (!string.IsNullOrEmpty(doctorName))
            {
                admissions = admissions.Where(a => (a.Doctor.FirstName + " " + a.Doctor.LastName).Contains(doctorName));
            }

            // Sort
            admissions = sortOrder switch
            {
                "patient_asc" => admissions.OrderBy(a => a.Patient.LastName + " " + a.Patient.FirstName),
                "patient_desc" => admissions.OrderByDescending(a => a.Patient.LastName + " " + a.Patient.FirstName),
                "doctor_asc" => admissions.OrderBy(a => a.Doctor.LastName + " " + a.Doctor.FirstName),
                "doctor_desc" => admissions.OrderByDescending(a => a.Doctor.LastName + " " + a.Doctor.FirstName),
                "date_asc" => admissions.OrderBy(a => a.PatientsAdmissionDateTime),
                "date_desc" => admissions.OrderByDescending(a => a.PatientsAdmissionDateTime),
                "hitan_asc" => admissions.OrderBy(a => a.EmergencyAdmission).ThenBy(a => a.PatientsAdmissionDateTime),
                "hitan_desc" => admissions.OrderByDescending(a => a.EmergencyAdmission).ThenBy(a => a.PatientsAdmissionDateTime),
                _ => admissions.OrderBy(a => a.PatientsAdmissionDateTime)
            };

            var model = new AdmissionFilterViewModel
            {
                Admissions = admissions.ToList(),
                FromDate = fromDate,
                ToDate = toDate,
                SortOrder = sortOrder,
                PatientName = patientName,
                DoctorName = doctorName
            };

            
            if (Request.Headers["X-Requested-With"] == "XMLHttpRequest")
            {
                return PartialView("_AdmissionsListPartial", model); // Ensure you have a partial view named _AdmissionsListPartial
            }

            return View(model);
        }

        // GET: Admissions/Create
        [HttpGet]
        public IActionResult Create()
        {
            var model = new AdmissionViewModel
            {
                // Show just doctors Specialist
                Patients = _context.Patients.Select(p => new SelectListItem
                {
                    Value = p.PatientId.ToString(),
                    Text = p.FirstName + " " + p.LastName
                }).ToList(),
                Doctors = _context.Doctors
                    .Where(d => d.TitleId == 1) 
                    .Select(d => new SelectListItem
                    {
                        Value = d.DoctorId.ToString(),
                        Text = d.LastName + " " + d.FirstName + " - " + d.DoctorCode
                    }).ToList()
            };

            return View(model);
        }

        // POST: Admissions/Create
        [HttpPost]
        [ValidateAntiForgeryToken]
        public IActionResult Create(AdmissionViewModel model)
        {
            if (model.ReceptionDateTime < DateTime.Now.AddMinutes(-2))
            {
                ModelState.AddModelError("ReceptionDateTime", "Reception date and time cannot be in the past.");
            }

            if (ModelState.IsValid)
            {
                var admission = new PatientsAdmission
                {
                    PatientId = model.PatientId,
                    DoctorId = model.DoctorId,
                    PatientsAdmissionDateTime = model.ReceptionDateTime,
                    EmergencyAdmission = model.EmergencyAdmission
                };

                _context.PatientsAdmissions.Add(admission);
                _context.SaveChanges();

                return RedirectToAction("Index");
            }

            model.Patients = _context.Patients.Select(p => new SelectListItem
            {
                Value = p.PatientId.ToString(),
                Text = p.FirstName + " " + p.LastName
            }).ToList();

            model.Doctors = _context.Doctors.Select(d => new SelectListItem
            {
                Value = d.DoctorId.ToString(),
                Text = d.LastName + " " + d.FirstName + " - " + d.DoctorCode
            }).ToList();

            return View(model);
        }

        // GET: Fetch doctors 
        [HttpGet]
        public IActionResult GetDoctorsByPatient(int patientId)
        {
            try
            {
                var doctors = _context.PatientsAdmissions
                    .Where(pa => pa.PatientId == patientId)
                    .Select(pa => pa.Doctor)
                    .Where(d => d.TitleId == 1)
                    .Select(d => new SelectListItem
                    {
                        Value = d.DoctorId.ToString(),
                        Text = d.LastName + " " + d.FirstName + " - " + d.DoctorCode
                    })
                    .ToList();

                if (!doctors.Any())
                {
                    doctors = _context.Doctors
                        .Where(d => d.TitleId == 1)
                        .Select(d => new SelectListItem
                        {
                            Value = d.DoctorId.ToString(),
                            Text = d.LastName + " " + d.FirstName + " - " + d.DoctorCode
                        })
                        .ToList();
                }

                return Json(doctors);
            }
            catch (Exception ex)
            {
                Console.WriteLine("Error retrieving doctors: " + ex.Message);
                return StatusCode(500, "Internal server error while retrieving doctors.");
            }
        }

        // GET: Add Finding for a specific admission
        [HttpGet]
        public IActionResult AddFinding(int admissionId)
        {
            // default value current datetime
            var model = new FindingViewModel
            {
                AdmissionId = admissionId,
                DateTimeOfFinding = DateTime.Now 
            };
            return View(model);
        }

        [HttpPost]
        [ValidateAntiForgeryToken]
        public IActionResult AddFinding(FindingViewModel model)
        {
            var admissionExists = _context.PatientsAdmissions.Any(pa => pa.PatientsAdmissionId == model.AdmissionId);

            if (!admissionExists)
            {
                ModelState.AddModelError("AdmissionId", "The specified Admission ID does not exist.");
                return View("~/Views/Admissions/AddFinding.cshtml", model); // Return the view with an error
            }

           if (ModelState.IsValid)
            {
                var finding = new Finding
                {
                    AdmissionId = model.AdmissionId,
                    Description = model.Description,
                    DateTimeOfFinding = DateTime.Now
                };

                _context.Findings.Add(finding);
                _context.SaveChanges();

                return RedirectToAction("Index");
            }

          
            var errors = ModelState.Values.SelectMany(v => v.Errors);
            foreach (var error in errors)
            {
                Console.WriteLine(error.ErrorMessage); 
            }

            return View("~/Views/Admissions/AddFinding.cshtml", model);
        }

        // GET: Edit Finding for a specific admission
        [HttpGet]
        public IActionResult EditFinding(int findingId)
        {
            try
            {
                var finding = _context.Findings.FirstOrDefault(f => f.FindingId == findingId);
                if (finding == null) return NotFound();

                var model = new FindingViewModel
                {
                    FindingId = finding.FindingId,
                    AdmissionId = finding.AdmissionId,
                    Description = finding.Description,
                    DateTimeOfFinding = finding.DateTimeOfFinding
                };
                return View(model);
            }
            catch (Exception ex)
            {
                Console.WriteLine("Error in EditFinding (GET): " + ex.ToString());
                return StatusCode(500, "Internal server error.");
            }
        }

        // POST: Edit Finding
        [HttpPost]
        [ValidateAntiForgeryToken]
        public IActionResult EditFinding(FindingViewModel model)
        {
            if (ModelState.IsValid)
            {
                try
                {
                    var finding = _context.Findings.FirstOrDefault(f => f.FindingId == model.FindingId);
                    if (finding == null) return NotFound();

                    finding.Description = model.Description;
                    finding.DateTimeOfFinding = model.DateTimeOfFinding; // Update date if needed
                    _context.SaveChanges();

                    return RedirectToAction("Index");
                }
                catch (Exception ex)
                {
                    Console.WriteLine("Error in EditFinding (POST): " + ex.ToString());
                    return StatusCode(500, "Internal server error.");
                }
            }

            return View("~/Views/Admissions/EditFinding.cshtml", model);
        }

        // POST: Delete Finding
        [HttpPost]
        [ValidateAntiForgeryToken]
        public IActionResult DeleteFinding(int findingId)
        {
            var finding = _context.Findings.Find(findingId);
            if (finding == null) return NotFound();

            _context.Findings.Remove(finding);
            _context.SaveChanges();

            return RedirectToAction("Index");
        }
    }
}

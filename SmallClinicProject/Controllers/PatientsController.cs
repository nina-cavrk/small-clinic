using Microsoft.AspNetCore.Mvc;
using SmallClinicProject.Models;
using System.Linq;

namespace SmallClinicProject.Controllers
{
    public class PatientsController : Controller
    {
        private readonly SmallClinicDB _context;
        public PatientsController(SmallClinicDB context)
        {
            _context = context;
        }
        public IActionResult List()
        {
            var patients = _context.Patients.ToList();
            return View(patients); 
        }

        // AJAX 
        [HttpGet]
        public IActionResult Search(string searchQuery)
        {
            var patients = _context.Patients.AsQueryable();
           if (!string.IsNullOrWhiteSpace(searchQuery))
            {
                patients = patients.Where(p =>
                    p.FirstName.Contains(searchQuery) || p.LastName.Contains(searchQuery));
            }
            return PartialView("_PatientListPartial", patients.ToList());
        }
    }
}

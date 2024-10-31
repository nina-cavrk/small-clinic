using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;
using SmallClinicProject.Models;
using System.Linq;

namespace SmallClinicProject.Controllers
{
    public class DoctorsController : Controller
    {
        private readonly SmallClinicDB _context;
        public DoctorsController(SmallClinicDB context)
        {
            _context = context;
        }

        // GET: Doctors
        public IActionResult List()
        {
            var doctors = _context.Doctors
                .Include(d => d.Title) 
                .Include(d => d.Gender) 
                .ToList();
            return View(doctors);
        }
    }
}

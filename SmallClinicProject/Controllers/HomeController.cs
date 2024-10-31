using Microsoft.AspNetCore.Authentication;
using Microsoft.AspNetCore.Mvc;
using Microsoft.Extensions.Logging;
using SmallClinicProject.Models;
using System.Diagnostics;
using System.Security.Claims;
using System.Threading.Tasks;
using System.Collections.Generic;
using Microsoft.AspNetCore.Authorization;

namespace SmallClinicProject.Controllers
{
    public class HomeController : Controller
    {
        private readonly ILogger<HomeController> _logger;
        public HomeController(ILogger<HomeController> logger)
        {
            _logger = logger;
        }
        [Authorize]
        public IActionResult Index()
        {
            return View();
        }
        public IActionResult Privacy()
        {
            return View();
        }
        // GET: Login page
        [HttpGet]
        [AllowAnonymous]
        public IActionResult Login()
        {
           if (User.Identity.IsAuthenticated)
            {
                return RedirectToAction("Index", "Admissions");
            }
         return View(new LoginViewModel());
        }

        [HttpPost]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> Login(LoginViewModel model)
        {
            if (ModelState.IsValid)
            {
                bool isValidUser = ValidateUserCredentials(model.Username, model.Password);

                if (isValidUser)
                {
                    var claims = new List<Claim>
                    {
                        new Claim(ClaimTypes.Name, model.Username)
                    };
                    var claimsIdentity = new ClaimsIdentity(claims, "CookieAuth");
                    await HttpContext.SignInAsync("CookieAuth", new ClaimsPrincipal(claimsIdentity));

                   return RedirectToAction("Index", "Admissions");
                }

               ModelState.AddModelError(string.Empty, "Invalid username or password.");
            }
            return View(model);
        }



        public async Task<IActionResult> Logout()
        {
            await HttpContext.SignOutAsync("CookieAuth");
            return RedirectToAction("Login");
        }

       private bool ValidateUserCredentials(string username, string password)
        {
           return username == "test" && password == "test";
        }

       [ResponseCache(Duration = 0, Location = ResponseCacheLocation.None, NoStore = true)]
        public IActionResult Error()
        {
            return View(new ErrorViewModel { RequestId = Activity.Current?.Id ?? HttpContext.TraceIdentifier });
        }
    }
}

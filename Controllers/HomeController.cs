using Library.Models;
using Library.Services.SessionService;
using Microsoft.AspNetCore.Mvc;
using System.Diagnostics;

namespace Library.Controllers {

    public class HomeController : Controller {


        private readonly ISessionService _sessionService;


        public HomeController(ISessionService sessionService) {
            _sessionService = sessionService;
        }


        [HttpGet]
        public IActionResult About() {

            if (!_sessionService.IsSessionActive()) {
                return RedirectToAction("Login", "Login");
            }

            return View();
        }


        [HttpGet]
        public IActionResult Help() {
            var filePath = Path.Combine(Directory.GetCurrentDirectory(), "wwwroot", "help", "index.html");
            return PhysicalFile(filePath, "text/html");
        }


        [HttpGet]
        public IActionResult HelpFirstAccess() {
            var filePath = Path.Combine(Directory.GetCurrentDirectory(), "wwwroot", "help", "user", "register-adm.html");
            return PhysicalFile(filePath, "text/html");
        }


        [HttpGet]
        public IActionResult HelpByIndex(int index) {
            
            var filePath = "";

            switch (index) {
                case 10: filePath = Path.Combine(Directory.GetCurrentDirectory(), "wwwroot", "help", "book-details", "book-details.html"); break;
                default: filePath = Path.Combine(Directory.GetCurrentDirectory(), "wwwroot", "help", "index.html"); break;
            }

            return PhysicalFile(filePath, "text/html");

        }


        [ResponseCache(Duration = 0, Location = ResponseCacheLocation.None, NoStore = true)]
        public IActionResult Error() {
            return View(new ErrorViewModel { RequestId = Activity.Current?.Id ?? HttpContext.TraceIdentifier });
        }

    }

}
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

            if (_sessionService.IsTheSessionActive()) {
                _sessionService.SetLayout(this);
            }

            return View();
        }


        [ResponseCache(Duration = 0, Location = ResponseCacheLocation.None, NoStore = true)]
        public IActionResult Error() {
            return View(new ErrorViewModel { RequestId = Activity.Current?.Id ?? HttpContext.TraceIdentifier });
        }

    }

}
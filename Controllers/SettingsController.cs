using Library.Dto;
using Library.Services.SessionService;
using Library.Services.SettingsService;
using Microsoft.AspNetCore.Mvc;

namespace Library.Controllers {


    public class SettingsController : Controller {


        private readonly ISettingsService _settingsService;

        private readonly ISessionService _sessionService;


        public SettingsController(ISettingsService settingsService, ISessionService sessionService) {
            _settingsService = settingsService;
            _sessionService = sessionService;
        }


        [HttpGet]
        public IActionResult Manage() {

            if (!_sessionService.IsSessionActive()) {
                return RedirectToAction("Login", "Login");
            }

            return View(new SettingsDto(_settingsService));

        }


        [HttpPost]
        public IActionResult Reset() {

            if (!_sessionService.IsSessionActive()) {
                return RedirectToAction("Login", "Login");
            }

            _settingsService.Reset();

            return RedirectToAction("Manage", "Settings");

        }


    }


}
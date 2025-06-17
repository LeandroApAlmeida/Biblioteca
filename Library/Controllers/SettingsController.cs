using Library.Services.Model.Dto;
using Microsoft.AspNetCore.Mvc;
using Library.Utils;
using Library.Services.Session;
using Library.Services.User;

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
        public IActionResult DefaultPageBackgroundColor() {

            if (!_sessionService.IsSessionActive()) {
                return RedirectToAction("Login", "Login");
            }

            _settingsService.SetString(
                Constants.PAGE_BACKGROUND_COLOR_KEY,
                Constants.DEFAULT_PAGE_BACKGROUND_COLOR
            );

            return RedirectToAction("Manage", "Settings");

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
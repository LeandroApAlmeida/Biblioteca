using Library.Dto;
using Library.Services.SettingsService;
using Microsoft.AspNetCore.Mvc;

namespace Library.Controllers {


    public class SettingsController : Controller {


        private readonly ISettingsService _settingsService;


        public SettingsController(ISettingsService settingsService) {
            _settingsService = settingsService;            
        }


        [HttpGet]
        public IActionResult Manage() {

            return View(new SettingsDto(_settingsService));

        }


    }


}
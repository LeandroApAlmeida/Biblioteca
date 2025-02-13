using Microsoft.AspNetCore.Mvc;

namespace Library.Controllers {


    public class SettingsController : Controller {


        [HttpGet]
        public IActionResult Manage() {
            return View();
        }


    }


}
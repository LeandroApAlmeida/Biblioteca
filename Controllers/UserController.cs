using Microsoft.AspNetCore.Mvc;

namespace Library.Controllers {
    public class UserController : Controller {
        public IActionResult Manage() {
            return View();
        }
    }
}

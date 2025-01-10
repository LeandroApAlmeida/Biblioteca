using System.Diagnostics;
using Library.Data;
using Library.Models;
using Microsoft.AspNetCore.Mvc;

namespace Library.Controllers {

    public class HomeController : Controller {


        private readonly ApplicationDbContext _db;


        public HomeController(ApplicationDbContext db) {
            _db = db;
        }


        public IActionResult About() {
            return View();
        }


        [ResponseCache(Duration = 0, Location = ResponseCacheLocation.None, NoStore = true)]
        public IActionResult Error() {
            return View(new ErrorViewModel { RequestId = Activity.Current?.Id ?? HttpContext.TraceIdentifier });
        }

    }

}
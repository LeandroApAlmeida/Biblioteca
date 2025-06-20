using Library.Db.Models;
using Library.Services.Collection;
using Library.Services.Model.Dto;
using Library.Services.Session;
using Library.Services.User;
using Microsoft.AspNetCore.Mvc;

namespace Library.Controllers {

    public class HomeController : Controller {


        private readonly ISessionService _sessionService;

        private readonly ICollectionService _collectionService;

        private readonly ISettingsService _settingsService;


        public HomeController(ISessionService sessionService, ICollectionService collectionService, 
        ISettingsService settingsService) {
            _sessionService = sessionService;
            _collectionService = collectionService;
            _settingsService = settingsService;
        }


        [HttpGet]
        public async Task<IActionResult> Index() {

            if (!_sessionService.IsSessionActive()) {
                return RedirectToAction("Login", "Login");
            }

            var booksResp = await _collectionService.GetCollectionBooksWithThumbnails();

            if (booksResp.Successful) {

                ViewBag.Settings = new SettingsDto(_settingsService);

                return View(booksResp.Data);

            } else {

                return BadRequest(booksResp.Message);

            }

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


    }

}
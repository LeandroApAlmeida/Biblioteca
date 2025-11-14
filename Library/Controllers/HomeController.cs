using Library.Services.Collection;
using Library.Services.Model.Dto;
using Library.Services.Session;
using Library.Services.User;
using Microsoft.AspNetCore.Mvc;

namespace Library.Controllers {


    /// <summary>
    /// Controlador principal.
    /// </summary>
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


        /// <summary>
        /// Retornar a página principal do site, que contém a lista de livros do acervo. É a primeira
        /// página exibida ao realizar o login no site.
        /// </summary>
        /// <returns>Página principal do site.</returns>
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


        /// <summary>
        /// Retornar a página de créditos do site.
        /// </summary>
        /// <returns>Página de créditos do site.</returns>
        [HttpGet]
        public IActionResult About() {

            if (!_sessionService.IsSessionActive()) {
                return RedirectToAction("Login", "Login");
            }

            return View();
        }


    }


}
using Library.Services.Collection;
using Library.Services.Model.Dto;
using Library.Services.Session;
using Library.Services.User;
using Microsoft.AspNetCore.Mvc;

namespace Library.Controllers {


    /// <summary>
    /// Controlador da página principal.
    /// </summary>
    public class HomeController(ISessionService sessionService, ICollectionService collectionService,
    ISettingsService settingsService) : Controller {


        /// <summary> Objeto para gerenciamento de sessão do usuário. </summary>
        private readonly ISessionService _sessionService = sessionService;

        /// <summary> Objeto para gerenciamento do acervo. </summary>
        private readonly ICollectionService _collectionService = collectionService;

        /// <summary> Objeto para acesso às configurações do usuário. </summary>
        private readonly ISettingsService _settingsService = settingsService;


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


    }


}
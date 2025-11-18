using Library.Services.Model.Dto;
using Microsoft.AspNetCore.Mvc;
using Library.Utils;
using Library.Services.Session;
using Library.Services.User;

namespace Library.Controllers {


    /// <summary>
    /// Controlador para gerenciamento das configurações do usuário.
    /// </summary>
    public class SettingsController(ISettingsService settingsService, ISessionService sessionService) : Controller {


        /// <summary> Objeto para acesso às configurações do usuário. </summary>
        private readonly ISettingsService _settingsService = settingsService;

        /// <summary> Objeto para gerenciamento de sessão do usuário. </summary>
        private readonly ISessionService _sessionService = sessionService;


        /// <summary>
        /// Retornar a página para configuração do ambiente.
        /// </summary>
        /// <returns>Página para configuração do ambiente.</returns>
        [HttpGet]
        public IActionResult Manage() {

            if (!_sessionService.IsSessionActive()) {
                return RedirectToAction("Login", "Login");
            }

            return View(new SettingsDto(_settingsService));

        }


        /// <summary>
        /// Definir a cor de fundo default da página.
        /// </summary>
        /// <returns>Página de redirecionamento.</returns>
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


        /// <summary>
        /// Definir todas as configurações como default.
        /// </summary>
        /// <returns>Página de redirecionamento.</returns>
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
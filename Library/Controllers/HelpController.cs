using Library.Services.Session;
using Microsoft.AspNetCore.Mvc;

namespace Library.Controllers {


    /// <summary>
    /// Controler para exibição da ajuda da aplicação.
    /// </summary>
    public class HelpController(ISessionService sessionService) : Controller {


        /// <summary> Objeto para gerenciamento de sessão do usuário. </summary>
        private readonly ISessionService _sessionService = sessionService;


        /// <summary>
        /// Retorna a página de ajuda com o menu à esquerda e conteúdo de ajuda à direita.
        /// </summary>
        /// <returns>Página de ajuda.</returns>
        public IActionResult Index() {

            if (!_sessionService.IsSessionActive()) {
                return RedirectToAction("Login", "Login");
            }

            var filePath = Path.Combine(Directory.GetCurrentDirectory(), "wwwroot", "help", "index.html");
            
            return PhysicalFile(filePath, "text/html");
        
        }


        /// <summary>
        /// Retornar a página de ajuda no primeiro acesso ao sistema.
        /// </summary>
        /// <returns>Página de ajuda.</returns>
        [HttpGet]
        public IActionResult HelpFirstAccess() {
        
            var filePath = Path.Combine(Directory.GetCurrentDirectory(), "wwwroot", "help", "user", "register-adm.html");
            
            return PhysicalFile(filePath, "text/html");
        
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
using Microsoft.AspNetCore.Mvc;

namespace Library.Controllers {


    public class HelpController : Controller {


        /// <summary>
        /// Retorna a página de ajuda com o menu à esquerda e conteúdo de ajuda à direita.
        /// </summary>
        /// <returns>Página de ajuda.</returns>
        public IActionResult Index() {
            var filePath = Path.Combine(Directory.GetCurrentDirectory(), "wwwroot", "help", "index.html");
            return PhysicalFile(filePath, "text/html");
        }


        /// <summary>
        /// Retornar a página de ajuda no primeiro acesso ao sistema.
        /// </summary>
        /// <returns>Página de ajuda no primeiro acesso ao sistema.</returns>
        [HttpGet]
        public IActionResult HelpFirstAccess() {
            var filePath = Path.Combine(Directory.GetCurrentDirectory(), "wwwroot", "help", "user", "register-adm.html");
            return PhysicalFile(filePath, "text/html");
        }


        /// <summary>
        /// Retornar a página de ajuda, de acordo com o índice.
        /// </summary>
        /// <param name="index">Índice da página de ajuda.</param>
        /// <returns>Página de ajuda, de acordo com o índice.</returns>
        [HttpGet]
        public IActionResult HelpByIndex(int index) {

            var filePath = "";

            switch (index) {
                case 10:
                filePath = Path.Combine(Directory.GetCurrentDirectory(), "wwwroot", "help", "book-details", "book-details.html");
                break;
                default:
                filePath = Path.Combine(Directory.GetCurrentDirectory(), "wwwroot", "help", "index.html");
                break;
            }

            return PhysicalFile(filePath, "text/html");

        }

    }


}
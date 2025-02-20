using Library.Services.BookService;
using Library.Services.SessionService;
using Microsoft.AspNetCore.Mvc;

namespace Library.Controllers {


    /// <summary>
    /// Api para manutenção do acervo.
    /// </summary>
    [ApiController]
    [Route("api/[controller]")]
    public class CollectionApiController : Controller {


        private readonly IBookService _bookService;

        private readonly ISessionService _sessionService;


        public CollectionApiController(IBookService bookService, ISessionService sessionService) {
            _bookService = bookService;
            _sessionService = sessionService;
        }


        /// <summary>
        /// Obter a capa de um livro que está gravada no banco de dados no formato string base64.
        /// </summary>
        /// <param name="id">Identificador do livro</param>
        /// <returns>String base64 que representa a capa do livro.</returns>
        [HttpGet("GetBookCover/{id}")]
        public async Task<ActionResult<string>> GetBookCover(Guid id) {

            if (!_sessionService.IsSessionActive()) return BadRequest(
                new { message = "Erro", error = "Sessão inativa." }
            );

            var bookResp = await _bookService.GetBook(id);

            if (bookResp.Successful) {

                if (bookResp.Data != null) {
                    return new JsonResult(bookResp.Data.Cover);
                } else {
                    return new JsonResult("");
                }

            } else {

                return new JsonResult("");

            }

        }


    }


}
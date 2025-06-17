using Library.Services.Collection;
using Library.Services.Session;
using Microsoft.AspNetCore.Mvc;
using System.Text.Json;

namespace Library.Controllers {


    /// <summary>
    /// Api para manutenção do acervo.
    /// </summary>
    [ApiController]
    [Route("Api/[controller]")]
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
        public async Task<ActionResult> GetBookCover(Guid id) {

            if (!_sessionService.IsSessionActive()) return BadRequest(
                new { message = "Erro", error = "Sessão inativa." }
            );

            var bookResp = await _bookService.GetBook(id);

            if (bookResp.Successful) {

                if (bookResp.Data != null) {
                    return new JsonResult(
                        new { Id = bookResp.Data.Cover.Id, Data = bookResp.Data.Cover.Data },
                        new JsonSerializerOptions { PropertyNamingPolicy = null }
                    );
                } else {
                    return NotFound(new { message = "Livro não encontrado." });
                }

            } else {

                return StatusCode(500, new { message = "Erro interno ao buscar o livro." });

            }

        }


    }

}
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
    public class CollectionApiController(IBookService bookService, ISessionService sessionService) : Controller {


        /// <summary> Objeto para manutenção de livros. </summary>
        private readonly IBookService _bookService = bookService;

        /// <summary> Objeto para gerenciamento de sessão do usuário. </summary>
        private readonly ISessionService _sessionService = sessionService;


        /// <summary>
        /// Obter a capa de um livro que está gravada no banco de dados no formato string base64.
        /// </summary>
        /// <param name="id">Identificador chave primária do livro</param>
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

                return StatusCode(500, new { message = "Erro ao buscar o livro." });

            }

        }


    }

}
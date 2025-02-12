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


        public CollectionApiController(IBookService bookService) {
            _bookService = bookService;
        }


        /// <summary>
        /// Obter a capa de um livro que está gravada no banco de dados no formato string base64.
        /// </summary>
        /// <param name="id">Identificador do livro</param>
        /// <returns></returns>
        [HttpGet("cover/{id}")]
        public async Task<ActionResult<string>> GetCover(Guid id) {

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
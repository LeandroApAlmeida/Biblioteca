using Library.Models;
using Library.Services.BookService;
using Library.Services.CollectionService;
using Library.Utils;
using Microsoft.AspNetCore.Mvc;

namespace Library.Controllers {


    /// <summary>
    /// Controller para a manutenção de livros do acervo.
    /// </summary>
    public class BookController : Controller {


        private readonly IBookService _bookService;

        private readonly ICollectionService _collectionService;


        public BookController(IBookService bookService, ICollectionService collectionService) {
            _collectionService = collectionService;
            _bookService = bookService;
        }


        /// <summary>
        /// Retornar a página de manutenção do acervo.
        /// </summary>
        /// <returns>Página de manutenção do acervo.</returns>
        [HttpGet]
        public async Task<IActionResult> Manage() {

            var booksResp = await _bookService.GetBooks();

            if (booksResp.Successful) {
                
                return View(booksResp.Data);
            
            } else {
                
                return BadRequest(booksResp.Message);
            
            }

        }


        /// <summary>
        /// Retornar a página de detalhes do livro, sem dados.
        /// </summary>
        /// <returns>Página de detalhes do livro, sem dados.</returns>
        [HttpGet]
        public IActionResult Empty() { 
            return View();
        }


        /// <summary>
        /// Retornar a página com os detalhes de um livro.
        /// </summary>
        /// <param name="id">Identificador do livro.</param>
        /// <returns>Página com os detalhes de um livro.</returns>
        [HttpGet]
        public async Task<IActionResult> Details(Guid id) {

            var booksIdsResp = await _collectionService.GetBooksIds();

            if (!booksIdsResp.Successful) return BadRequest(booksIdsResp.Message);

            List<Guid> booksIds = booksIdsResp.Data!;

            if (booksIds != null && booksIds.Count != 0) {

                BookModel? book;

                List<Boolean> navInfo = [];

                if (id == Guid.Empty) {

                    var firstBookResp = await _bookService.GetBook(booksIds.First());

                    book = firstBookResp.Data;

                    navInfo.Add(true);
                    navInfo.Add(booksIds.First() == booksIds.Last());

                } else {

                    var bookResp = await _bookService.GetBook(id);

                    book = bookResp.Data;

                    navInfo.Add(id == booksIds.First());
                    navInfo.Add(id == booksIds.Last());

                }

                ViewBag.NavInfo = navInfo;

                if (book != null) {
                    
                    return View(book);
                
                } else {
                    
                    return NotFound();
                
                }

            } else {

                return RedirectToAction("Empty");

            }

        }


        /// <summary>
        /// Retornar a página com os detalhes do livro adiante ao atual na consulta.
        /// </summary>
        /// <param name="id">Identificador do livro atual</param>
        /// <returns>Página com os detalhes do livro.</returns>
        [HttpGet]
        public async Task<IActionResult> NextBook(Guid id) {

            var nextBookResp = await _bookService.NextBookId(id);

            if (nextBookResp.Successful) {
                
                return RedirectToAction("Details", new { id = nextBookResp.Data });
            
            } else {
                
                return BadRequest(nextBookResp.Message);
            
            }

        }


        /// <summary>
        /// Retornar a página com os detalhes do livro anterior ao atual na consulta.
        /// </summary>
        /// <param name="id">Identificador do livro atual</param>
        /// <returns>Página com os detalhes do livro.</returns>
        [HttpGet]
        public async Task<IActionResult> PreviousBook(Guid id) {

            var previousBookResp = await _bookService.PreviousBookId(id);

            if (previousBookResp.Successful) {
                
                return RedirectToAction("Details", new { id = previousBookResp.Data });
            
            } else {
                
                return BadRequest(previousBookResp.Message);
            
            }

        }


        /// <summary>
        /// Retornar a página com os detalhes do primeiro livro recuperado na consulta.
        /// </summary>
        /// <returns>Página com os detalhes do livro.</returns>
        [HttpGet]
        public async Task<IActionResult> FirstBook() {

            var firstBookResp = await _bookService.FirstBookId();

            if (firstBookResp.Successful) {
                
                return RedirectToAction("Details", new { id = firstBookResp.Data });
            
            } else {
                
                return BadRequest(firstBookResp.Message);
            
            }

        }


        /// <summary>
        /// Retornar a página com os detalhes do último livro recuperado na consulta.
        /// </summary>
        /// <returns>Página com os detalhes do livro.</returns>
        [HttpGet]
        public async Task<IActionResult> LastBook() {

            var lastBookResp = await _bookService.LastBookId();

            if (lastBookResp.Successful) {
                
                return RedirectToAction("Details", new { id = lastBookResp.Data });
            
            } else {
                
                return BadRequest(lastBookResp.Message);
            
            }

        }


        /// <summary>
        /// Retornar a página para cadastro de um novo livro.
        /// </summary>
        /// <returns>Página de cadastro de um novo livro.</returns>
        [HttpGet]
        public IActionResult Register() {

            return View();

        }


        /// <summary>
        /// Cadastrar um novo livro.
        /// </summary>
        /// <param name="book">Novo livro</param>
        /// <returns>Página de redirecionamento.</returns>
        [HttpPost]
        public async Task<IActionResult> Register(BookModel book) {

            if (ModelState.IsValid) {

                var registerBookResp = await _bookService.RegisterBook(book);

                if (registerBookResp.Successful) {
                    
                    TempData[Constants.success_message] = registerBookResp.Message;
                    
                    return RedirectToAction("Manage");
                
                } else {
                    
                    TempData[Constants.error_message] = registerBookResp.Message;
                    
                    return View(book);
                
                }

            } else {

                TempData[Constants.error_message] = "Dados do livro incorretos!";

                return View(book);

            }

        }


        /// <summary>
        /// Retornar a página para edição de um livro.
        /// </summary>
        /// <param name="id">Identificador do livro</param>
        /// <returns>Página para edição de um livro.</returns>
        [HttpGet]
        public async Task<IActionResult> Edit(Guid id) {

            var bookResp = await _bookService.GetBook(id);

            if (bookResp.Successful) {

                return View(bookResp.Data);

            } else {

                return BadRequest(bookResp.Message);

            }

        }


        /// <summary>
        /// Alterar o cadastro de um livro.
        /// </summary>
        /// <param name="book">Livro a ser alterado</param>
        /// <returns>Página de redirecionamento.</returns>
        [HttpPost]
        public async Task<IActionResult> Edit(BookModel book) {

            if (ModelState.IsValid) {

                var editBookResp = await _bookService.EditBook(book);

                if (editBookResp.Successful) {
                    
                    TempData[Constants.success_message] = editBookResp.Message;
                    
                    return RedirectToAction("Manage");
                
                } else {

                    TempData[Constants.error_message] = editBookResp.Message;

                    return View(book);
                
                }

            } else {

                TempData[Constants.error_message] = "Dados do livro incorretos!";

                return View(book);

            }

        }


        /// <summary>
        /// Retornar a página para a exclusão de um livro.
        /// </summary>
        /// <param name="id">Identificador do livro</param>
        /// <returns>Página para a exclusão de um livro.</returns>
        [HttpGet]
        public async Task<IActionResult> Delete(Guid id) {

            var isBorrowedBookResp = await _collectionService.IsBorrowedBook(id);

            if (!isBorrowedBookResp.Successful) return BadRequest(isBorrowedBookResp.Message);

            Boolean isBorrowed = isBorrowedBookResp.Data;

            if (!isBorrowed) {

                var bookResp = await _bookService.GetBook(id);

                BookModel? book = bookResp.Data;

                if (book != null) {

                    return View(book);

                } else {

                    return NotFound();

                }

            } else {

                TempData[Constants.error_message] = "O livro consta como emprestado. Faça a devolução!";

                return RedirectToAction("Manage");

            }

        }


        /// <summary>
        /// Excluir o cadastro de um livro.
        /// </summary>
        /// <param name="book">Livro a ser excluído</param>
        /// <returns>Página de redirecionamento.</returns>
        [HttpPost]
        public async Task<IActionResult> Delete(BookModel book) {

            if (ModelState.IsValid) {

                var deleteBookResp = await _bookService.DeleteBook(book);

                if (deleteBookResp.Successful) {

                    TempData[Constants.success_message] = deleteBookResp.Message;

                    return RedirectToAction("Manage");

                } else {

                    TempData[Constants.error_message] = deleteBookResp.Message;

                    return View(book);

                }

            } else {

                TempData[Constants.error_message] = "Dados do livro incorretos!";

                return View(book);

            }

        }


    }


}
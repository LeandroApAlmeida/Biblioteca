using Library.Models;
using Library.Services.BookService;
using Library.Services.CollectionService;
using Library.Services.DiscardService;
using Library.Utils;
using Microsoft.AspNetCore.Mvc;

namespace Library.Controllers {


    /// <summary>
    /// Controlador para a manutenção de livros descartados.
    /// </summary>
    public class DiscardController : Controller {


        private readonly ICollectionService _collectionService;

        private readonly IDiscardService _discardService;

        private readonly IBookService _bookService;


        public DiscardController(IDiscardService discardService, IBookService bookService,
        ICollectionService collectionService) {
            _collectionService = collectionService;
            _discardService = discardService;
            _bookService = bookService;
        }


        /// <summary>
        /// Retornar a página de manutenção de livros descartados.
        /// </summary>
        /// <returns>Página de manutenção de livros descartados.</returns>
        [HttpGet]
        public async Task<IActionResult> Manage() {

            var discardedBooksResp = await _discardService.GetDiscardedBooks();

            if (discardedBooksResp.Successful) {

                return View(discardedBooksResp.Data);

            } else {

                return BadRequest(discardedBooksResp.Message);

            }

        }


        /// <summary>
        /// Retornar a página para descarte de um livro selecionado na página de manutenção do acervo.
        /// </summary>
        /// <param name="id">Identificador do livro</param>
        /// <returns>Página para descarte de um livro.</returns>
        [HttpGet]
        public async Task<IActionResult> Register(Guid id) {

            var isBorrowedBookResp = await _collectionService.IsBorrowedBook(id);

            if (!isBorrowedBookResp.Successful) return BadRequest(isBorrowedBookResp.Message);

            Boolean isBorrowed = isBorrowedBookResp.Data;

            if (!isBorrowed) {

                var bookResp = await _bookService.GetBook(id);

                if (bookResp.Successful) {

                    ViewBag.Books = bookResp.Data;

                    return View();

                } else {

                    TempData[Constants.error_message] = bookResp.Message;

                    return RedirectToAction("Manage", "Book");

                }

            } else {

                TempData[Constants.error_message] = "O livro consta como emprestado. Faça a devolução!";

                return RedirectToAction("Manage", "Book");

            }

        }


        /// <summary>
        /// Descartar o livro selecionado na página de manutenção do acervo.
        /// </summary>
        /// <param name="discardedBook">Livro a ser descartado</param>
        /// <returns>Página de redirecionamento.</returns>
        [HttpPost]
        public async Task<IActionResult> Register(DiscardedBookModel discardedBook) {

            if (ModelState.IsValid) {

                var registerDiscardedBookResp = await _discardService.RegisterDiscardedBook(discardedBook);

                if (registerDiscardedBookResp.Successful) {

                    TempData[Constants.success_message] = registerDiscardedBookResp.Message;

                    return RedirectToAction("Manage", "Book");

                } else {

                    TempData[Constants.error_message] = registerDiscardedBookResp.Message;

                    return RedirectToAction("Register", new { id = discardedBook.Book.Id });

                }

            } else {

                TempData[Constants.error_message] = "Dados do descarte incorretos!";

                return RedirectToAction("Register", new { id = discardedBook.Book.Id });

            }

        }


        /// <summary>
        /// Retornar a página para descarte de um livro a partir da página manutenção de livros descartados. Esta
        /// página contém uma lista de livros a selecionar.
        /// </summary>
        /// <returns>Página para descarte de um livro.</returns>
        [HttpGet]
        public async Task <IActionResult> Register2() {

            var availableBooksResp = await _collectionService.GetAvailableBooks();

            if (availableBooksResp.Successful) {

                ViewBag.Books = availableBooksResp.Data;

                return View();

            } else {

                return BadRequest(availableBooksResp.Message);

            }

        }


        /// <summary>
        /// Descartar o livro selecionado na página de manutenção de livros descartados.
        /// </summary>
        /// <param name="discardedBook">Livro a ser descartado</param>
        /// <returns>Página de redirecionamento.</returns>
        [HttpPost]
        public async Task<IActionResult> Register2(DiscardedBookModel discardedBook) {

            if (ModelState.IsValid) {

                var registerDiscardedBookResp = await _discardService.RegisterDiscardedBook(discardedBook);

                if (registerDiscardedBookResp.Successful) {

                    TempData[Constants.success_message] = registerDiscardedBookResp.Message;

                    return RedirectToAction("Manage");

                } else {

                    TempData[Constants.error_message] = registerDiscardedBookResp.Message;

                    return RedirectToAction("Register2");

                }

            } else {

                TempData[Constants.error_message] = "Dados do descarte incorretos!";

                return RedirectToAction("Register2");

            }

        }


        /// <summary>
        /// Retornar a página para edição de um livro descartado.
        /// </summary>
        /// <param name="id">Identificador do livro</param>
        /// <returns>Página para edição de um livro descartado.</returns>
        [HttpGet]
        public async Task<IActionResult> Edit(Guid id) {

            var discardedBookResp = await _discardService.GetDiscardedBook(id);

            if (discardedBookResp.Successful) {

                if (discardedBookResp.Data != null) {
                    return View(discardedBookResp.Data);
                } else {
                    return NotFound();
                }

            } else {

                return BadRequest(discardedBookResp.Message);

            }

        }


        /// <summary>
        /// Alterar o cadastro de um livro descartado.
        /// </summary>
        /// <param name="book">Livro a ser alterado</param>
        /// <returns>Página de redirecionamento.</returns>
        [HttpPost]
        public async Task<IActionResult> Edit(DiscardedBookModel discardedBook) {

            if (ModelState.IsValid) {

                var editDiscardedBookResp = await _discardService.EditDiscardedBook(discardedBook);

                if (editDiscardedBookResp.Successful) {

                    TempData[Constants.success_message] = editDiscardedBookResp.Message;

                    return RedirectToAction("Manage");

                } else {

                    TempData[Constants.error_message] = editDiscardedBookResp.Message;

                    return View(discardedBook);

                }

            } else {

                TempData[Constants.error_message] = "Dados do descarte incorretos!";

                return View(discardedBook);

            }

        }


        /// <summary>
        /// Retornar a página para a exclusão de um livro descartado.
        /// </summary>
        /// <param name="id">Identificador do livro</param>
        /// <returns>Página para a exclusão de um livro descartado.</returns>
        [HttpGet]
        public async Task<IActionResult> Delete(Guid id) {

            var discardedBookResp = await _discardService.GetDiscardedBook(id);

            if (discardedBookResp.Successful) {

                if (discardedBookResp.Data != null) {
                    return View(discardedBookResp.Data);
                } else {
                    return NotFound();
                }

            } else {

                return BadRequest(discardedBookResp.Message);

            }

        }


        /// <summary>
        /// Excluir o cadastro de um livro descartado.
        /// </summary>
        /// <param name="book">Livro a ser excluído</param>
        /// <returns>Página de redirecionamento.</returns>
        [HttpPost]
        public async Task<IActionResult> Delete(DiscardedBookModel discardedBook) {

            if (ModelState.IsValid) {

                var deleteDiscardedBooksResp = await _discardService.DeleteDiscardedBook(discardedBook);

                if (deleteDiscardedBooksResp.Successful) {

                    TempData[Constants.success_message] = deleteDiscardedBooksResp.Message;

                    return RedirectToAction("Manage");

                } else {

                    TempData[Constants.error_message] = deleteDiscardedBooksResp.Message;

                    return View(discardedBook);

                }

            } else {

                TempData[Constants.error_message] = "Dados do descarte incorretos!";

                return View(discardedBook);

            }

        }


    }


}
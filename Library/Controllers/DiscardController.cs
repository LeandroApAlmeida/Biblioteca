using Library.Services.Model.Dto;
using Library.Services.Collection;
using Library.Services.Session;
using Library.Services.User;
using Library.Utils;
using Microsoft.AspNetCore.Mvc;
using Library.Db.Models;

namespace Library.Controllers {


    /// <summary>
    /// Controlador para a manutenção de livros descartados.
    /// </summary>
    public class DiscardController : Controller {


        private readonly ICollectionService _collectionService;

        private readonly IDiscardService _discardService;

        private readonly IBookService _bookService;

        private readonly ISessionService _sessionService;

        private readonly ISettingsService _settingsService;


        public DiscardController(IDiscardService discardService, IBookService bookService,
        ICollectionService collectionService, ISessionService sessionService, ISettingsService settingsService) {
            _collectionService = collectionService;
            _discardService = discardService;
            _bookService = bookService;
            _sessionService = sessionService;
            _settingsService = settingsService;
        }


        /// <summary>
        /// Retornar a página de manutenção de livros descartados.
        /// </summary>
        /// <returns>Página de manutenção de livros descartados.</returns>
        [HttpGet]
        public async Task<IActionResult> Manage() {

            if (!_sessionService.IsSessionActive()) {
                return RedirectToAction("Login", "Login");
            }

            var discardedBooksResp = await _discardService.GetDiscardedBooks();

            if (discardedBooksResp.Successful) {

                ViewBag.Settings = new SettingsDto(_settingsService);

                return View(discardedBooksResp.Data);

            } else {

                return BadRequest(discardedBooksResp.Message);

            }

        }


        /// <summary>
        /// Retornar a página para descarte de um livro selecionado na página de
        /// manutenção do acervo. Caso o livro esteja emprestado, não permite o 
        /// descarte antes que seja devolvido.
        /// </summary>
        /// <param name="id">Identificador do livro</param>
        /// <returns>Página para descarte de um livro.</returns>
        [HttpGet]
        public async Task<IActionResult> Register(Guid id) {

            if (!_sessionService.IsSessionActive()) {
                return RedirectToAction("Login", "Login");
            }

            var isBorrowedBookResp = await _collectionService.IsBorrowedBook(id);

            if (!isBorrowedBookResp.Successful) return BadRequest(isBorrowedBookResp.Message);

            Boolean isBorrowed = isBorrowedBookResp.Data;

            if (!isBorrowed) {

                // A página de descarte obtida a partir da página de manutenção do acervo
                // deve listar o livro selecionado e os campos para informação do descarte.
                // O livro selecionado é retornado em ViewBag.Book.

                var bookResp = await _bookService.GetBook(id);

                if (bookResp.Successful) {

                    ViewBag.Book = bookResp.Data;

                    return View();

                } else {

                    TempData[Constants.ERROR_MESSAGE] = bookResp.Message;

                    return RedirectToAction("Manage", "Book");

                }

            } else {

                TempData[Constants.ERROR_MESSAGE] = "O livro consta como emprestado. Faça a devolução!";

                return RedirectToAction("Manage", "Book");

            }

        }


        /// <summary>
        /// Descartar o livro selecionado na página de manutenção do acervo.
        /// </summary>
        /// <param name="discardedBook">Livro a ser descartado</param>
        /// <returns>Página de redirecionamento.</returns>
        [HttpPost]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> Register(DiscardedBookModel discardedBook) {

            if (!_sessionService.IsSessionActive()) {
                return RedirectToAction("Login", "Login");
            }

            if (ModelState.IsValid) {

                var registerDiscardedBookResp = await _discardService.RegisterDiscardedBook(discardedBook);

                if (registerDiscardedBookResp.Successful) {

                    TempData[Constants.SUCCESS_MESSAGE] = "Livro descartado com sucesso!";

                    return RedirectToAction("Manage", "Book");

                } else {

                    TempData[Constants.ERROR_MESSAGE] = registerDiscardedBookResp.Message;

                    return RedirectToAction("Register", new { id = discardedBook.Book.Id });

                }

            } else {

                TempData[Constants.ERROR_MESSAGE] = "Dados do descarte incorretos!";

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

            if (!_sessionService.IsSessionActive()) {
                return RedirectToAction("Login", "Login");
            }

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
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> Register2(DiscardedBookModel discardedBook) {

            if (!_sessionService.IsSessionActive()) {
                return RedirectToAction("Login", "Login");
            }

            if (ModelState.IsValid) {

                var registerDiscardedBookResp = await _discardService.RegisterDiscardedBook(discardedBook);

                if (registerDiscardedBookResp.Successful) {

                    TempData[Constants.SUCCESS_MESSAGE] = registerDiscardedBookResp.Message;

                    return RedirectToAction("Manage");

                } else {

                    TempData[Constants.ERROR_MESSAGE] = registerDiscardedBookResp.Message;

                    return RedirectToAction("Register2");

                }

            } else {

                TempData[Constants.ERROR_MESSAGE] = "Dados do descarte incorretos!";

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

            if (!_sessionService.IsSessionActive()) {
                return RedirectToAction("Login", "Login");
            }

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
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> Edit(DiscardedBookModel discardedBook) {

            if (!_sessionService.IsSessionActive()) {
                return RedirectToAction("Login", "Login");
            }

            if (ModelState.IsValid) {

                var editDiscardedBookResp = await _discardService.EditDiscardedBook(discardedBook);

                if (editDiscardedBookResp.Successful) {

                    TempData[Constants.SUCCESS_MESSAGE] = editDiscardedBookResp.Message;

                    return RedirectToAction("Manage");

                } else {

                    TempData[Constants.ERROR_MESSAGE] = editDiscardedBookResp.Message;

                    return View(discardedBook);

                }

            } else {

                TempData[Constants.ERROR_MESSAGE] = "Dados do descarte incorretos!";

                return View(discardedBook);

            }

        }


        /// <summary>
        /// Excluir o cadastro de um livro descartado.
        /// </summary>
        /// <param name="book">Livro a ser excluído</param>
        /// <returns>Página de redirecionamento.</returns>
        [HttpPost]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> Delete(Guid id) {

            if (!_sessionService.IsSessionActive()) {
                return RedirectToAction("Login", "Login");
            }

            var deleteDiscardedBooksResp = await _discardService.DeleteDiscardedBook(id);

            if (deleteDiscardedBooksResp.Successful) {

                TempData[Constants.SUCCESS_MESSAGE] = deleteDiscardedBooksResp.Message;

                return RedirectToAction("Manage");

            } else {

                TempData[Constants.ERROR_MESSAGE] = deleteDiscardedBooksResp.Message;

                return RedirectToAction("Manage");

            }

        }


    }


}
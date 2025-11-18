using Library.Services.Model.Dto;
using Library.Services.Collection;
using Library.Services.Session;
using Library.Services.User;
using Library.Utils;
using Microsoft.AspNetCore.Mvc;
using Library.Db.Models;

namespace Library.Controllers {


    /// <summary>
    /// Controlador para a manutenção de livros doados.
    /// </summary>
    public class DonationController(IDonationService donationService, IBookService bookService,
    IPersonService personService, ICollectionService collectionService, ISessionService sessionService,
    ISettingsService settingsService, ILoanService loanService) : Controller {


        /// <summary> Objeto para manutenção de livros doados. </summary>
        private readonly IDonationService _donationService = donationService;

        /// <summary> Objeto para manutenção do acervo. </summary>
        private readonly ICollectionService _collectionService = collectionService;

        /// <summary> Objeto para manutenção de livros. </summary>
        private readonly IBookService _bookService = bookService;

        /// <summary> Objeto para manutenção de empréstimos. </summary>
        private readonly ILoanService _loanService = loanService;

        /// <summary> Objeto para manutenção de pessoas. </summary>
        private readonly IPersonService _personService = personService;

        /// <summary> Objeto para gerenciamento de sessão do usuário. </summary>
        private readonly ISessionService _sessionService = sessionService;

        /// <summary> Objeto para acesso às configurações do usuário. </summary>
        private readonly ISettingsService _settingsService = settingsService;


        /// <summary>
        /// Retornar a página para manutenção de livros doados.
        /// </summary>
        /// <returns>Página para manutenção de livros doados.</returns>
        [HttpGet]
        public async Task<IActionResult> Manage() {

            if (!_sessionService.IsSessionActive()) {
                return RedirectToAction("Login", "Login");
            }

            var donatedBooksResp = await _donationService.GetDonatedBooks();

            if (donatedBooksResp.Successful) {

                ViewBag.Settings = new SettingsDto(_settingsService);

                return View(donatedBooksResp.Data);

            } else {

                return BadRequest(donatedBooksResp.Message);

            }

        }


        /// <summary>
        /// Retornar a página para doação de um livro selecionado na página de
        /// manutenção do acervo. Caso o livro esteja emprestado, não permite a 
        /// doação antes que seja devolvido.
        /// </summary>
        /// <param name="id">Identificador chave primária do livro.</param>
        /// <returns>Página para doação de um livro.</returns>
        [HttpGet]
        public async Task<IActionResult> Register(Guid id) {

            if (!_sessionService.IsSessionActive()) {
                return RedirectToAction("Login", "Login");
            }

            var isBorrowedBookResp = await _loanService.IsBorrowedBook(id);

            if (!isBorrowedBookResp.Successful) return BadRequest(isBorrowedBookResp.Message);

            Boolean isBorrowed = isBorrowedBookResp.Data;

            if (!isBorrowed) {

                var bookResp = await _bookService.GetBook(id);
                var personsResp = await _personService.GetPersons();

                if (bookResp.Successful && personsResp.Successful) {

                    ViewBag.Book = bookResp.Data;
                    ViewBag.Persons = personsResp.Data;

                    return View();

                } else {

                    if (!bookResp.Successful) {
                        TempData[Constants.ERROR_MESSAGE] = bookResp.Message;
                    } else {
                        TempData[Constants.ERROR_MESSAGE] = personsResp.Message;
                    }

                    return RedirectToAction("Manage", "Book");

                }

            } else {

                TempData[Constants.ERROR_MESSAGE] = "O livro consta como emprestado. Faça a devolução!";

                return RedirectToAction("Manage", "Book");

            }

        }


        /// <summary>
        /// Doar o livro selecionado na página de manutenção do acervo.
        /// </summary>
        /// <param name="donatedBook">Livro a ser doado.</param>
        /// <returns>Página de redirecionamento.</returns>
        [HttpPost]
        [ValidateAntiForgeryToken]
        public async Task <IActionResult> Register(DonatedBookModel donatedBook) {

            if (!_sessionService.IsSessionActive()) {
                return RedirectToAction("Login", "Login");
            }

            if (ModelState.IsValid) {

                var registerDonatedBookResp = await _donationService.RegisterDonatedBook(donatedBook);

                if (registerDonatedBookResp.Successful) {

                    TempData[Constants.SUCCESS_MESSAGE] = "Livro doado com sucesso!";

                    return RedirectToAction("Manage", "Book");

                } else {

                    TempData[Constants.ERROR_MESSAGE] = registerDonatedBookResp.Message;

                    return RedirectToAction("Register", new {id = donatedBook.Id});

                }

            } else {

                TempData[Constants.ERROR_MESSAGE] = "Dados da doação incorretos!";

                return RedirectToAction("Register", new {id = donatedBook.Id});

            }

        }


        /// <summary>
        /// Retornar a página para doação de um livro a partir da página manutenção de livros doados. Esta
        /// página contém uma lista de livros a selecionar.
        /// </summary>
        /// <returns>Página para doação de um livro.</returns>
        [HttpGet]
        public async Task<IActionResult> Register2() {

            if (!_sessionService.IsSessionActive()) {
                return RedirectToAction("Login", "Login");
            }

            var availableBooksResp = await _collectionService.GetAvailableBooks();

            var personsResp = await _personService.GetPersons();

            if (availableBooksResp.Successful && personsResp.Successful) {

                ViewBag.Books = availableBooksResp.Data;

                ViewBag.Persons = personsResp.Data;

                return View();

            } else {

                if (!availableBooksResp.Successful) {
                    TempData[Constants.ERROR_MESSAGE] = availableBooksResp.Message;
                } else {
                    TempData[Constants.ERROR_MESSAGE] = personsResp.Message;
                }

                return RedirectToAction("Manage");

            }

        }


        /// <summary>
        /// Doar o livro selecionado na página para manutenção de livros doados.
        /// </summary>
        /// <param name="donatedBook">Livro a ser doado.</param>
        /// <returns>Página de redirecionamento.</returns>
        [HttpPost]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> Register2(DonatedBookModel donatedBook) {

            if (!_sessionService.IsSessionActive()) {
                return RedirectToAction("Login", "Login");
            }

            if (ModelState.IsValid) {

                var registerDonatedBookResp = await _donationService.RegisterDonatedBook(donatedBook);

                if (registerDonatedBookResp.Successful) {

                    TempData[Constants.SUCCESS_MESSAGE] = registerDonatedBookResp.Message;

                    return RedirectToAction("Manage");

                } else {

                    TempData[Constants.ERROR_MESSAGE] = registerDonatedBookResp.Message;

                    return RedirectToAction("Register2");

                }

            } else {

                TempData[Constants.ERROR_MESSAGE] = "Dados da doação incorretos!";

                return RedirectToAction("Register2");

            }

        }


        /// <summary>
        /// Retornar a página para edição de um livro doado.
        /// </summary>
        /// <param name="id">Identificador chave primária do livro.</param>
        /// <returns>Página para edição de um livro doado.</returns>
        [HttpGet]
        public async Task<IActionResult> Edit(Guid id) {

            if (!_sessionService.IsSessionActive()) {
                return RedirectToAction("Login", "Login");
            }

            var donatedBookResp = await _donationService.GetDonatedBook(id);

            var personsResp = await _personService.GetPersons();

            if (donatedBookResp.Successful && personsResp.Successful) {

                ViewBag.Persons = personsResp.Data;

                return View(donatedBookResp.Data);

            } else {

                if (!donatedBookResp.Successful) {
                    TempData[Constants.ERROR_MESSAGE] = donatedBookResp.Message;
                } else {
                    TempData[Constants.ERROR_MESSAGE] = personsResp.Message;
                }

                return RedirectToAction("Manage");

            }

        }


        /// <summary>
        /// Alterar o cadastro de um livro doado.
        /// </summary>
        /// <param name="donatedBook">Livro a ser alterado.</param>
        /// <returns>Página de redirecionamento.</returns>
        [HttpPost]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> Edit(DonatedBookModel donatedBook) {

            if (!_sessionService.IsSessionActive()) {
                return RedirectToAction("Login", "Login");
            }

            if (ModelState.IsValid) {

                var editDonatedBookResp = await _donationService.EditDonatedBook(donatedBook);

                if (editDonatedBookResp.Successful) {

                    TempData[Constants.SUCCESS_MESSAGE] = editDonatedBookResp.Message;

                    return RedirectToAction("Manage");

                } else {

                    TempData[Constants.ERROR_MESSAGE] = editDonatedBookResp.Message;

                    return RedirectToAction("Edit", new {id = donatedBook.Id});

                }

            } else {

                TempData[Constants.ERROR_MESSAGE] = "Dados da doação incorretos!";

                return RedirectToAction("Edit", new {id = donatedBook.Id});

            }

        }


        /// <summary>
        /// Excluir o cadastro de um livro doado.
        /// </summary>
        /// <param name="id">Identificador chave primária do livro.</param>
        /// <returns>Página de redirecionamento.</returns>
        [HttpPost]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> Delete(Guid id) {

            if (!_sessionService.IsSessionActive()) {
                return RedirectToAction("Login", "Login");
            }

            var deleteDonatedBookResp = await _donationService.DeleteDonatedBook(id);

            if (deleteDonatedBookResp.Successful) {

                TempData[Constants.SUCCESS_MESSAGE] = deleteDonatedBookResp.Message;

                return RedirectToAction("Manage");

            } else {

                TempData[Constants.ERROR_MESSAGE] = deleteDonatedBookResp.Message;

                return RedirectToAction("Manage");

            }

        }


    }


}

using Library.Services.Model.Dto;
using Library.Services.Collection;
using Library.Services.Session;
using Library.Services.User;
using Library.Utils;
using Microsoft.AspNetCore.Mvc;
using Library.Db.Models;

namespace Library.Controllers {


    /// <summary>
    /// Controlador para a manutenção de empréstimos.
    /// </summary>
    public class LoanController(ILoanService loanService, IPersonService personService,
    IBookService bookService, ICollectionService collectionService, ISessionService sessionService,
    ISettingsService settingsService) : Controller {


        /// <summary> Objeto para manutenção de empréstimos. </summary>
        private readonly ILoanService _loanService = loanService;

        /// <summary> Objeto para gerenciamento do acervo. </summary>
        private readonly ICollectionService _collectionService = collectionService;

        /// <summary> Objeto para manutenção de livros. </summary>
        private readonly IBookService _bookService = bookService;

        /// <summary> Objeto para manutenção de pessoas. </summary>
        private readonly IPersonService _personService = personService;

        /// <summary> Objeto para gerenciamento de sessão do usuário. </summary>
        private readonly ISessionService _sessionService = sessionService;

        /// <summary> Objeto para acesso às configurações do usuário. </summary>
        private readonly ISettingsService _settingsService = settingsService;


        /// <summary>
        /// Retornar a página para manutenção de empréstimos.
        /// </summary>
        /// <returns>Página para manutenção de empréstimos.</returns>
        [HttpGet]
        public async Task<IActionResult> Manage() {

            if (!_sessionService.IsSessionActive()) {
                return RedirectToAction("Login", "Login");
            }

            var loansResp = await _loanService.GetLoans();

            if (loansResp.Successful) {

                ViewBag.Settings = new SettingsDto(_settingsService);

                return View(loansResp.Data);

            } else {

                return BadRequest(loansResp.Message);

            }

        }


        /// <summary>
        /// Retornar a página para empréstimo de um livro selecionado na página de
        /// manutenção do acervo. Caso o livro já esteja emprestado, é necessário fazer
        /// a devolução do mesmo.
        /// </summary>
        /// <param name="id">Identificador chave primária do livro.</param>
        /// <returns>Página para empréstimo de um livro.</returns>
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
        /// Emprestar o livro selecionado na página de manutenção do acervo.
        /// </summary>
        /// <param name="loan">Registro de empréstimo.</param>
        /// <returns>Página de redirecionamento.</returns>
        [HttpPost]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> Register(LoanModel loan) {

            if (!_sessionService.IsSessionActive()) {
                return RedirectToAction("Login", "Login");
            }

            if (ModelState.IsValid) {

                var registerLoanResp = await _loanService.RegisterLoan(loan);

                if (registerLoanResp.Successful) {

                    TempData[Constants.SUCCESS_MESSAGE] = "Livro emprestado com sucesso!";

                    return RedirectToAction("Manage", "Book");

                } else {

                    TempData[Constants.ERROR_MESSAGE] = registerLoanResp.Message;

                    return RedirectToAction("Register", new { id = loan.Id });

                }

            } else {

                TempData[Constants.ERROR_MESSAGE] = "Dados do empréstimo incorretos!";

                return RedirectToAction("Register", new { id = loan.Id });

            }

        }


        /// <summary>
        /// Retornar a página para empréstimo de um livro a partir da página manutenção de empréstimos. Esta
        /// página contém uma lista de livros a selecionar.
        /// </summary>
        /// <returns>Página para empréstimo de um livro.</returns>
        [HttpGet]
        public async Task <IActionResult> Register2() {

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
        /// Emprestar o livro selecionado na página de manutenção de empréstimos.
        /// </summary>
        /// <param name="discardedBook">Registro de empréstimo.</param>
        /// <returns>Página de redirecionamento.</returns>
        [HttpPost]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> Register2(LoanModel loan) {

            if (!_sessionService.IsSessionActive()) {
                return RedirectToAction("Login", "Login");
            }

            if (ModelState.IsValid) {

                var registerLoanResp = await _loanService.RegisterLoan(loan);

                if (registerLoanResp.Successful) {

                    TempData[Constants.SUCCESS_MESSAGE] = registerLoanResp.Message;

                    return RedirectToAction("Manage");

                } else {

                    TempData[Constants.ERROR_MESSAGE] = registerLoanResp.Message;

                    return RedirectToAction("Register2");

                }

            } else {

                TempData[Constants.ERROR_MESSAGE] = "Dados do empréstimo incorretos!";

                return RedirectToAction("Register2");

            }

        }


        /// <summary>
        /// Retornar a página para edição de um livro emprestado.
        /// </summary>
        /// <param name="id">Identificador chave primária do registro de empréstimo.</param>
        /// <returns>Página para edição de um livro emprestado.</returns>
        [HttpGet]
        public async Task<IActionResult> Edit(Guid id) {

            if (!_sessionService.IsSessionActive()) {
                return RedirectToAction("Login", "Login");
            }

            var loanResp = await _loanService.GetLoan(id);

            var personsResp = await _personService.GetPersons();

            if (loanResp.Successful && personsResp.Successful) {

                ViewBag.Persons = personsResp.Data;

                return View(loanResp.Data);

            } else {

                if (!loanResp.Successful) {
                    TempData[Constants.ERROR_MESSAGE] = loanResp.Message;
                } else {
                    TempData[Constants.ERROR_MESSAGE] = personsResp.Message;
                }

                return RedirectToAction("Manage");

            }

        }


        /// <summary>
        /// Alterar o cadastro de um livro emprestado.
        /// </summary>
        /// <param name="loan">Registro de empréstimo a ser alterado.</param>
        /// <returns>Página de redirecionamento.</returns>
        [HttpPost]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> Edit(LoanModel loan) {

            if (!_sessionService.IsSessionActive()) {
                return RedirectToAction("Login", "Login");
            }

            if (ModelState.IsValid) {

                var editLoanResp = await _loanService.EditLoan(loan);

                if (editLoanResp.Successful) {

                    TempData[Constants.SUCCESS_MESSAGE] = editLoanResp.Message;

                    return RedirectToAction("Manage");

                } else {

                    TempData[Constants.ERROR_MESSAGE] = editLoanResp.Message;

                    return RedirectToAction("Edit", new { id = loan.Id });

                }

            } else {

                TempData[Constants.ERROR_MESSAGE] = "Dados do empréstimo incorretos!";

                return RedirectToAction("Edit", new { id = loan.Id });

            }

        }


        /// <summary>
        /// Excluir o cadastro de um livro emprestado.
        /// </summary>
        /// <param name="id">Identificador chave primária do registro de empréstimo.</param>
        /// <returns>Página de redirecionamento.</returns>
        [HttpPost]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> Delete(Guid id) {

            if (!_sessionService.IsSessionActive()) {
                return RedirectToAction("Login", "Login");
            }

            var deleteLoanResp = await _loanService.DeleteLoan(id);

            if (deleteLoanResp.Successful) {

                TempData[Constants.SUCCESS_MESSAGE] = deleteLoanResp.Message;

                return RedirectToAction("Manage");

            } else {

                TempData[Constants.ERROR_MESSAGE] = deleteLoanResp.Message;

                return RedirectToAction("Manage");

            }

        }


        /// <summary>
        /// Retornar a página para a devolução de um livro emprestado.
        /// </summary>
        /// <param name="id">Identificador chave primária do registro de empréstimo.</param>
        /// <returns>Página para devolução de um livro emprestado.</returns>
        [HttpGet]
        public async Task<IActionResult> Return(Guid id) {

            if (!_sessionService.IsSessionActive()) {
                return RedirectToAction("Login", "Login");
            }

            var loanResp = await _loanService.GetLoan(id);

            if (loanResp.Successful) {

                return View(loanResp.Data);

            } else {

                TempData[Constants.ERROR_MESSAGE] = loanResp.Message;

                return RedirectToAction("Manage");

            }

        }


        /// <summary>
        /// Fazer a devolução de um livro emprestado.
        /// </summary>
        /// <param name="loan">Registro de empréstimo a ser devolvido.</param>
        /// <returns>Página de redirecionamento.</returns>
        [HttpPost]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> Return(LoanModel loan) {

            if (!_sessionService.IsSessionActive()) {
                return RedirectToAction("Login", "Login");
            }

            if (ModelState.IsValid) {

                var returnLoanResp = await _loanService.ReturnLoan(loan);

                if (returnLoanResp.Successful) {

                    TempData[Constants.SUCCESS_MESSAGE] = returnLoanResp.Message;

                    return RedirectToAction("Manage");

                } else {

                    TempData[Constants.ERROR_MESSAGE] = returnLoanResp.Message;

                    return RedirectToAction("Return", new { id = loan.Id });

                }

            } else {

                TempData[Constants.ERROR_MESSAGE] = "Dados do empréstimo incorretos!";

                return RedirectToAction("Return", new { id = loan.Id });

            }

        }


        /// <summary>
        /// Cancelar a devolução de um livro emprestado.
        /// </summary>
        /// <param name="id">Identificador chave primária do registro de empréstimo.</param>
        /// <returns>Página de redirecionamento.</returns>
        [HttpPost]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> Cancel(Guid id) {

            if (!_sessionService.IsSessionActive()) {
                return RedirectToAction("Login", "Login");
            }

            var cancelReturnResp = await _loanService.CancelReturn(id);

            if (cancelReturnResp.Successful) {

                TempData[Constants.SUCCESS_MESSAGE] = cancelReturnResp.Message;

                return RedirectToAction("Manage");

            } else {

                TempData[Constants.ERROR_MESSAGE] = cancelReturnResp.Message;

                return RedirectToAction("Manage");

            }

        }


    }


}

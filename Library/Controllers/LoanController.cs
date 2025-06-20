using Library.Services.Model.Dto;
using Library.Services.Collection;
using Library.Services.Session;
using Library.Services.User;
using Library.Utils;
using Microsoft.AspNetCore.Mvc;
using Library.Db.Models;

namespace Library.Controllers {


    public class LoanController : Controller {


        private readonly ILoanService _loanService;

        private readonly ICollectionService _collectionService;

        private readonly IBookService _bookService;

        private readonly IPersonService _personService;

        private readonly ISessionService _sessionService;

        private readonly ISettingsService _settingsService;


        public LoanController(ILoanService loanService, IPersonService personService, 
        IBookService bookService, ICollectionService collectionService, ISessionService sessionService,
        ISettingsService settingsService) {
            _collectionService = collectionService;
            _loanService = loanService;
            _bookService = bookService;
            _personService = personService;
            _sessionService = sessionService;
            _settingsService = settingsService;
        }


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

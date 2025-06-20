using Library.Services.Model.Dto;
using Library.Services.Collection;
using Library.Services.Session;
using Library.Services.User;
using Library.Utils;
using Microsoft.AspNetCore.Mvc;
using Library.Db.Models;

namespace Library.Controllers {


    public class DonationController : Controller {


        private readonly IDonationService _donationService;

        private readonly ICollectionService _collectionService;

        private readonly IBookService _bookService;

        private readonly ILoanService _loanService;

        private readonly IPersonService _personService;

        private readonly ISessionService _sessionService;

        private readonly ISettingsService _settingsService;


        public DonationController(IDonationService donationService, IBookService bookService,
        IPersonService personService, ICollectionService collectionService, ISessionService sessionService,
        ISettingsService settingsService, ILoanService loanService) {
            _collectionService = collectionService;
            _bookService = bookService;
            _personService = personService;
            _donationService = donationService;
            _sessionService = sessionService;
            _settingsService = settingsService;
            _loanService = loanService;
        }


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

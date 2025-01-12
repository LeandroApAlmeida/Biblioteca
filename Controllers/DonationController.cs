﻿using Library.Models;
using Library.Services.BookService;
using Library.Services.CollectionService;
using Library.Services.DonationService;
using Library.Services.PersonService;
using Library.Services.SessionService;
using Library.Services.UserService;
using Library.Utils;
using Microsoft.AspNetCore.Mvc;

namespace Library.Controllers {


    public class DonationController : Controller {


        private readonly IDonationService _donationService;

        private readonly ICollectionService _collectionService;

        private readonly IBookService _bookService;

        private readonly IPersonService _personService;

        private readonly ISessionService _sessionService;


        public DonationController(IDonationService donationService, IBookService bookService,
        IPersonService personService, ICollectionService collectionService, ISessionService sessionService) {
            _collectionService = collectionService;
            _bookService = bookService;
            _personService = personService;
            _donationService = donationService;
            _sessionService = sessionService;
        }


        [HttpGet]
        public async Task<IActionResult> Manage() {

            if (!_sessionService.IsTheSessionActive()) {
                return RedirectToAction("Login", "Login");
            }

            _sessionService.SetLayout(this);

            var donatedBooksResp = await _donationService.GetDonatedBooks();

            if (donatedBooksResp.Successful) {

                return View(donatedBooksResp.Data);

            } else {

                return BadRequest(donatedBooksResp.Message);

            }

        }


        [HttpGet]
        public async Task<IActionResult> Register(Guid id) {

            if (!_sessionService.IsTheSessionActive()) {
                return RedirectToAction("Login", "Login");
            }

            _sessionService.SetLayout(this);

            var isBorrowedBookResp = await _collectionService.IsBorrowedBook(id);

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
        public async Task <IActionResult> Register(DonatedBookModel donatedBook) {

            if (!_sessionService.IsTheSessionActive()) {
                return RedirectToAction("Login", "Login");
            }

            if (ModelState.IsValid) {

                var registerDonatedBookResp = await _donationService.RegisterDonatedBook(donatedBook);

                if (registerDonatedBookResp.Successful) {

                    TempData[Constants.SUCCESS_MESSAGE] = registerDonatedBookResp.Message;

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

            if (!_sessionService.IsTheSessionActive()) {
                return RedirectToAction("Login", "Login");
            }

            _sessionService.SetLayout(this);

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
        public async Task<IActionResult> Register2(DonatedBookModel donatedBook) {

            if (!_sessionService.IsTheSessionActive()) {
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

            if (!_sessionService.IsTheSessionActive()) {
                return RedirectToAction("Login", "Login");
            }

            _sessionService.SetLayout(this);

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
        public async Task<IActionResult> Edit(DonatedBookModel donatedBook) {

            if (!_sessionService.IsTheSessionActive()) {
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


        [HttpGet]
        public async Task<IActionResult> Delete(Guid id) {

            if (!_sessionService.IsTheSessionActive()) {
                return RedirectToAction("Login", "Login");
            }

            _sessionService.SetLayout(this);

            var donatedBookResp = await _donationService.GetDonatedBook(id);

            if (donatedBookResp.Successful) {

                return View(donatedBookResp.Data);

            } else {

                return BadRequest(donatedBookResp.Message);

            }

        }


        [HttpPost]
        public async Task<IActionResult> Delete(DonatedBookModel donatedBook) {

            if (!_sessionService.IsTheSessionActive()) {
                return RedirectToAction("Login", "Login");
            }

            if (ModelState.IsValid) {

                var deleteDonatedBookResp = await _donationService.DeleteDonatedBook(donatedBook);

                if (deleteDonatedBookResp.Successful) {

                    TempData[Constants.SUCCESS_MESSAGE] = deleteDonatedBookResp.Message;

                    return RedirectToAction("Manage");

                } else {

                    TempData[Constants.ERROR_MESSAGE] = deleteDonatedBookResp.Message;

                    return RedirectToAction("Delete", new { id = donatedBook.Id });

                }

            } else {

                TempData[Constants.ERROR_MESSAGE] = "Dados da doação incorretos!";

                return RedirectToAction("Delete", new { id = donatedBook.Id });

            }

        }


    }


}

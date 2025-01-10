using Library.Models;
using Library.Services.BookService;
using Library.Services.CollectionService;
using Library.Services.DonationService;
using Library.Services.PersonService;
using Library.Utils;
using Microsoft.AspNetCore.Mvc;

namespace Library.Controllers {


    public class DonationController : Controller {


        private readonly IDonationService _donationService;

        private readonly ICollectionService _collectionService;

        private readonly IBookService _bookService;

        private readonly IPersonService _personService;


        public DonationController(IDonationService donationService, IBookService bookService,
        IPersonService personService, ICollectionService collectionService) {
            _collectionService = collectionService;
            _bookService = bookService;
            _personService = personService;
            _donationService = donationService;
        }


        [HttpGet]
        public async Task<IActionResult> Manage() {

            var donatedBooksResp = await _donationService.GetDonatedBooks();

            if (donatedBooksResp.Successful) {

                return View(donatedBooksResp.Data);

            } else {

                return BadRequest(donatedBooksResp.Message);

            }

        }


        [HttpGet]
        public async Task<IActionResult> Register(Guid id) {

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
                        TempData[Constants.error_message] = bookResp.Message;
                    } else {
                        TempData[Constants.error_message] = personsResp.Message;
                    }

                    return RedirectToAction("Manage", "Book");

                }

            } else {

                TempData[Constants.error_message] = "O livro consta como emprestado. Faça a devolução!";

                return RedirectToAction("Manage", "Book");

            }

        }


        [HttpPost]
        public async Task <IActionResult> Register(DonatedBookModel donatedBook) {

            if (ModelState.IsValid) {

                var registerDonatedBookResp = await _donationService.RegisterDonatedBook(donatedBook);

                if (registerDonatedBookResp.Successful) {

                    TempData[Constants.success_message] = registerDonatedBookResp.Message;

                    return RedirectToAction("Manage", "Book");

                } else {

                    TempData[Constants.error_message] = registerDonatedBookResp.Message;

                    return RedirectToAction("Register", new {id = donatedBook.Id});

                }

            } else {

                TempData[Constants.error_message] = "Dados da doação incorretos!";

                return RedirectToAction("Register", new {id = donatedBook.Id});

            }

        }


        [HttpGet]
        public async Task<IActionResult> Register2() {

            var availableBooksResp = await _collectionService.GetAvailableBooks();

            var personsResp = await _personService.GetPersons();

            if (availableBooksResp.Successful && personsResp.Successful) {

                ViewBag.Books = availableBooksResp.Data;

                ViewBag.Persons = personsResp.Data;

                return View();

            } else {

                if (!availableBooksResp.Successful) {
                    TempData[Constants.error_message] = availableBooksResp.Message;
                } else {
                    TempData[Constants.error_message] = personsResp.Message;
                }

                return RedirectToAction("Manage");

            }

        }


        [HttpPost]
        public async Task<IActionResult> Register2(DonatedBookModel donatedBook) {

            if (ModelState.IsValid) {

                var registerDonatedBookResp = await _donationService.RegisterDonatedBook(donatedBook);

                if (registerDonatedBookResp.Successful) {

                    TempData[Constants.success_message] = registerDonatedBookResp.Message;

                    return RedirectToAction("Manage");

                } else {

                    TempData[Constants.error_message] = registerDonatedBookResp.Message;

                    return RedirectToAction("Register2");

                }

            } else {

                TempData[Constants.error_message] = "Dados da doação incorretos!";

                return RedirectToAction("Register2");

            }

        }


        [HttpGet]
        public async Task<IActionResult> Edit(Guid id) {

            var donatedBookResp = await _donationService.GetDonatedBook(id);

            var personsResp = await _personService.GetPersons();

            if (donatedBookResp.Successful && personsResp.Successful) {

                ViewBag.Persons = personsResp.Data;

                return View(donatedBookResp.Data);

            } else {

                if (!donatedBookResp.Successful) {
                    TempData[Constants.error_message] = donatedBookResp.Message;
                } else {
                    TempData[Constants.error_message] = personsResp.Message;
                }

                return RedirectToAction("Manage");

            }

        }


        [HttpPost]
        public async Task<IActionResult> Edit(DonatedBookModel donatedBook) {

            if (ModelState.IsValid) {

                var editDonatedBookResp = await _donationService.EditDonatedBook(donatedBook);

                if (editDonatedBookResp.Successful) {

                    TempData[Constants.success_message] = editDonatedBookResp.Message;

                    return RedirectToAction("Manage");

                } else {

                    TempData[Constants.error_message] = editDonatedBookResp.Message;

                    return RedirectToAction("Edit", new {id = donatedBook.Id});

                }

            } else {

                TempData[Constants.error_message] = "Dados da doação incorretos!";

                return RedirectToAction("Edit", new {id = donatedBook.Id});

            }

        }


        [HttpGet]
        public async Task<IActionResult> Delete(Guid id) {

            var donatedBookResp = await _donationService.GetDonatedBook(id);

            if (donatedBookResp.Successful) {

                return View(donatedBookResp.Data);

            } else {

                return BadRequest(donatedBookResp.Message);

            }

        }


        [HttpPost]
        public async Task<IActionResult> Delete(DonatedBookModel donatedBook) {

            if (ModelState.IsValid) {

                var deleteDonatedBookResp = await _donationService.DeleteDonatedBook(donatedBook);

                if (deleteDonatedBookResp.Successful) {

                    TempData[Constants.success_message] = deleteDonatedBookResp.Message;

                    return RedirectToAction("Manage");

                } else {

                    TempData[Constants.error_message] = deleteDonatedBookResp.Message;

                    return RedirectToAction("Delete", new { id = donatedBook.Id });

                }

            } else {

                TempData[Constants.error_message] = "Dados da doação incorretos!";

                return RedirectToAction("Delete", new { id = donatedBook.Id });

            }

        }


    }


}

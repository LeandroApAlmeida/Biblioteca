using Library.Models;
using Library.Services.BookService;
using Library.Services.CollectionService;
using Library.Services.LoanService;
using Library.Services.PersonService;
using Library.Utils;
using Microsoft.AspNetCore.Mvc;

namespace Library.Controllers {


    public class LoanController : Controller {


        private readonly ILoanService _loanService;

        private readonly ICollectionService _collectionService;

        private readonly IBookService _bookService;

        private readonly IPersonService _personService;


        public LoanController(ILoanService loanService, IPersonService personService, 
        IBookService bookService, ICollectionService collectionService) {
            _collectionService = collectionService;
            _loanService = loanService;
            _bookService = bookService;
            _personService = personService;
        }


        [HttpGet]
        public async Task<IActionResult> Manage() {

            var loansResp = await _loanService.GetLoans();

            if (loansResp.Successful) {

                return View(loansResp.Data);

            } else {

                return BadRequest(loansResp.Message);

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
        public async Task<IActionResult> Register(LoanModel loan) {

            if (ModelState.IsValid) {

                var registerLoanResp = await _loanService.RegisterLoan(loan);

                if (registerLoanResp.Successful) {

                    TempData[Constants.success_message] = registerLoanResp.Message;

                    return RedirectToAction("Manage", "Book");

                } else {

                    TempData[Constants.error_message] = registerLoanResp.Message;

                    return RedirectToAction("Register", new { id = loan.Id });

                }

            } else {

                TempData[Constants.error_message] = "Dados do empréstimo incorretos!";

                return RedirectToAction("Register", new { id = loan.Id });

            }

        }


        [HttpGet]
        public async Task <IActionResult> Register2() {

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
        public async Task<IActionResult> Register2(LoanModel loan) {

            if (ModelState.IsValid) {

                var registerLoanResp = await _loanService.RegisterLoan(loan);

                if (registerLoanResp.Successful) {

                    TempData[Constants.success_message] = registerLoanResp.Message;

                    return RedirectToAction("Manage");

                } else {

                    TempData[Constants.error_message] = registerLoanResp.Message;

                    return RedirectToAction("Register2");

                }

            } else {

                TempData[Constants.error_message] = "Dados do empréstimo incorretos!";

                return RedirectToAction("Register2");

            }

        }


        [HttpGet]
        public async Task<IActionResult> Edit(Guid id) {

            var loanResp = await _loanService.GetLoan(id);

            var personsResp = await _personService.GetPersons();

            if (loanResp.Successful && personsResp.Successful) {

                ViewBag.Persons = personsResp.Data;

                return View(loanResp.Data);

            } else {

                if (!loanResp.Successful) {
                    TempData[Constants.error_message] = loanResp.Message;
                } else {
                    TempData[Constants.error_message] = personsResp.Message;
                }

                return RedirectToAction("Manage");

            }

        }


        [HttpPost]
        public async Task<IActionResult> Edit(LoanModel loan) {

            if (ModelState.IsValid) {

                var editLoanResp = await _loanService.EditLoan(loan);

                if (editLoanResp.Successful) {

                    TempData[Constants.success_message] = editLoanResp.Message;

                    return RedirectToAction("Manage");

                } else {

                    TempData[Constants.error_message] = editLoanResp.Message;

                    return RedirectToAction("Edit", new { id = loan.Id });

                }

            } else {

                TempData[Constants.error_message] = "Dados do empréstimo incorretos!";

                return RedirectToAction("Edit", new { id = loan.Id });

            }

        }


        [HttpGet]
        public async Task<IActionResult> Delete(Guid id) {

            var loanResp = await _loanService.GetLoan(id);

            if (loanResp.Successful) {

                return View(loanResp.Data);

            } else {

                TempData[Constants.error_message] = loanResp.Message;

                return RedirectToAction("Manage");

            }

        }


        [HttpPost]
        public async Task<IActionResult> Delete(LoanModel loan) {

            if (ModelState.IsValid) {

                var deleteLoanResp = await _loanService.DeleteLoan(loan);

                if (deleteLoanResp.Successful) {

                    TempData[Constants.success_message] = deleteLoanResp.Message;

                    return RedirectToAction("Manage");

                } else {

                    TempData[Constants.error_message] = deleteLoanResp.Message;

                    return RedirectToAction("Delete", new { id = loan.Id });

                }

            } else {

                TempData[Constants.error_message] = "Dados do empréstimo incorretos!";

                return RedirectToAction("Delete", new { id = loan.Id });

            }

        }


        [HttpGet]
        public async Task<IActionResult> Return(Guid id) {

            var loanResp = await _loanService.GetLoan(id);

            if (loanResp.Successful) {

                return View(loanResp.Data);

            } else {

                TempData[Constants.error_message] = loanResp.Message;

                return RedirectToAction("Manage");

            }

        }


        [HttpPost]
        public async Task<IActionResult> Return(LoanModel loan) {

            if (ModelState.IsValid) {

                var returnLoanResp = await _loanService.ReturnLoan(loan);

                if (returnLoanResp.Successful) {

                    TempData[Constants.success_message] = returnLoanResp.Message;

                    return RedirectToAction("Manage");

                } else {

                    TempData[Constants.error_message] = returnLoanResp.Message;

                    return RedirectToAction("Return", new { id = loan.Id });

                }

            } else {

                TempData[Constants.error_message] = "Dados do empréstimo incorretos!";

                return RedirectToAction("Return", new { id = loan.Id });

            }

        }


        [HttpGet]
        public async Task<IActionResult> Cancel(Guid id) {

            var isBorrowedBookResp = await _collectionService.IsBorrowedBook(id);

            if (!isBorrowedBookResp.Successful) return BadRequest(isBorrowedBookResp.Message);

            Boolean isBorrowed = isBorrowedBookResp.Data;

            if (!isBorrowed) {

                var loanResp = await _loanService.GetLoan(id);

                if (loanResp.Successful) {

                    return View(loanResp.Data);

                } else {

                    TempData[Constants.error_message] = loanResp.Message;

                    return RedirectToAction("Manage");

                }

            } else {

                TempData[Constants.error_message] = "O livro consta como emprestado. Faça a devolução!";

                return RedirectToAction("Manage");

            }

        }


        [HttpPost]
        public async Task<IActionResult> Cancel(LoanModel loan) {

            if (ModelState.IsValid) {

                var cancelReturnResp = await _loanService.CancelReturn(loan);

                if (cancelReturnResp.Successful) {

                    TempData[Constants.success_message] = cancelReturnResp.Message;

                    return RedirectToAction("Manage");

                } else {

                    TempData[Constants.error_message] = cancelReturnResp.Message;

                    return RedirectToAction("Cancel", new { id = loan.Id });

                }

            } else {

                TempData[Constants.error_message] = "Dados do empréstimo incorretos!";

                return RedirectToAction("Cancel", new { id = loan.Id });

            }

        }


    }


}

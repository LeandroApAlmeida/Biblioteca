using Library.Models;
using Library.Services.BookService;
using Library.Services.CollectionService;
using Library.Services.DiscardService;
using Library.Services.DonationService;
using Library.Services.LoanService;
using Library.Services.ReportService;
using Library.Services.SessionService;
using Library.Services.SettingsService;
using Library.Utils;
using Microsoft.AspNetCore.Mvc;

namespace Library.Controllers {


    public class ReportController : Controller {


        private readonly IBookService _bookService;

        private readonly ICollectionService _collectionService;

        private readonly IDiscardService _discardService;

        private readonly IDonationService _donationService;

        private readonly ILoanService _loanService;

        private readonly IPdfReportService _pdfReportService;

        private readonly IHtmlReportService _htmlReportService;

        private readonly ISessionService _sessionService;

        private readonly ISettingsService _settingsService;

        
        public ReportController(IBookService bookService, ICollectionService collectionService,
        IDiscardService discardService, IDonationService donationService, ILoanService loanService, 
        IPdfReportService pdfReportService, IHtmlReportService htmlReportService, ISessionService sessionService,
        ISettingsService settingsService) {
            _pdfReportService = pdfReportService;
            _bookService = bookService;
            _discardService = discardService;
            _donationService = donationService;
            _loanService = loanService;
            _sessionService = sessionService;
            _settingsService = settingsService;
            _htmlReportService = htmlReportService;
            _collectionService = collectionService;
        }


        [HttpGet]
        public async Task<IActionResult> BookDetailedReport(Guid id) {

            if (!_sessionService.IsSessionActive()) {
                return RedirectToAction("Login", "Login");
            }

            var bookResp = await _bookService.GetBook(id);

            if (bookResp.Successful) {

                BookModel book = bookResp.Data!;

                if (_settingsService.GetInt(Constants.REPORT_FORMAT_KEY, Constants.PDF_FORMAT) ==
                Constants.PDF_FORMAT) {

                    Response.Headers.Append(
                        "Content-Disposition",
                        "inline; filename=" +
                        book.Title
                        .Replace("\\", "")
                        .Replace("/", "")
                        .Replace("%", " por cento ")
                        .Replace(":", "-")
                        .Replace("*", "")
                        .Replace("<", "")
                        .Replace(">", "")
                        .Replace("?", "")
                        .Replace("\"", "'")
                        .Replace("|", "") +
                        ".pdf"
                    );

                    var pdfBytes = _pdfReportService.BookDetailed(book);

                    return File(pdfBytes, "application/pdf");

                } else {

                    Response.Headers.Append(
                        "Content-Disposition",
                        "inline; filename=" +
                        book.Title
                        .Replace("\\", "")
                        .Replace("/", "")
                        .Replace("%", " por cento ")
                        .Replace(":", "-")
                        .Replace("*", "")
                        .Replace("<", "")
                        .Replace(">", "")
                        .Replace("?", "")
                        .Replace("\"", "'")
                        .Replace("|", "") +
                        ".html"
                    );

                    Response.Headers.Append(
                        "Content-Type",
                        "text/html; charset=utf-8"
                    );

                    var htmlScript = _htmlReportService.BookDetailedWithTitle(book);

                    return File(System.Text.Encoding.UTF8.GetBytes(htmlScript), "text/html; charset=utf-8");

                } 

            } else {

                return BadRequest(bookResp.Message);

            }

        }


        [HttpGet]
        public async Task<IActionResult> BooksInTheCollectionReport() {

            if (!_sessionService.IsSessionActive()) {
                return RedirectToAction("Login", "Login");
            }

            var booksResp = await _collectionService.GetAvailableBooks();

            if (booksResp.Successful) {

                List<BookModel> books = booksResp.Data!;

                if (_settingsService.GetInt(Constants.REPORT_FORMAT_KEY, Constants.PDF_FORMAT) ==
                Constants.PDF_FORMAT) {

                    var pdfBytes = _pdfReportService.BooksInTheCollection(books);

                    Response.Headers.Append(
                        "Content-Disposition",
                        "inline; filename=LivrosNoAcervo.pdf"
                    );

                    return File(pdfBytes, "application/pdf");

                } else {

                    Response.Headers.Append(
                        "Content-Disposition",
                        "inline; filename=LivrosNoAcervo.html"
                    );

                    Response.Headers.Append(
                        "Content-Type",
                        "text/html; charset=utf-8"
                    );

                    var htmlScript = _htmlReportService.BooksInTheCollectionWithTitle(books);

                    return File(System.Text.Encoding.UTF8.GetBytes(htmlScript), "text/html; charset=utf-8");

                }

            } else {

                return BadRequest(booksResp.Message);

            }

        }


        [HttpGet]
        public async Task<IActionResult> RegisteredBooksReport() {

            if (!_sessionService.IsSessionActive()) {
                return RedirectToAction("Login", "Login");
            }

            var booksResp = await _bookService.GetBooks();

            if (booksResp.Successful) {

                List<BookModel> books = booksResp.Data!;

                if (_settingsService.GetInt(Constants.REPORT_FORMAT_KEY, Constants.PDF_FORMAT) ==
                Constants.PDF_FORMAT) {

                    var pdfBytes = _pdfReportService.RegisteredBooks(books);

                    Response.Headers.Append(
                        "Content-Disposition",
                        "inline; filename=LivrosCadastrados.pdf"
                    );

                    return File(pdfBytes, "application/pdf");

                } else {

                    Response.Headers.Append(
                        "Content-Disposition",
                        "inline; filename=LivrosCadastrados.html"
                    );

                    Response.Headers.Append(
                        "Content-Type",
                        "text/html; charset=utf-8"
                    );

                    var htmlScript = _htmlReportService.RegisteredBooksWithTitle(books);

                    return File(System.Text.Encoding.UTF8.GetBytes(htmlScript), "text/html; charset=utf-8");

                }

            } else {

                return BadRequest(booksResp.Message);

            }

        }


        [HttpGet]
        public async Task<IActionResult> DiscardedBooksReport() {

            if (!_sessionService.IsSessionActive()) {
                return RedirectToAction("Login", "Login");
            }

            var discardedBooksResp = await _discardService.GetDiscardedBooks();

            if (discardedBooksResp.Successful) {

                List<DiscardedBookModel> discardedBooks = discardedBooksResp.Data!;

                if (_settingsService.GetInt(Constants.REPORT_FORMAT_KEY, Constants.PDF_FORMAT) ==
                Constants.PDF_FORMAT) {

                    var pdfBytes = _pdfReportService.DiscardedBooks(discardedBooks);

                    Response.Headers.Append(
                        "Content-Disposition",
                        "inline; filename=LivrosDescartados.pdf"
                    );

                    return File(pdfBytes, "application/pdf");

                } else {

                    Response.Headers.Append(
                        "Content-Disposition",
                        "inline; filename=LivrosDescartados.html"
                    );

                    Response.Headers.Append(
                        "Content-Type",
                        "text/html; charset=utf-8"
                    );

                    var htmlScript = _htmlReportService.DiscardedBooksWithTitle(discardedBooks);

                    return File(System.Text.Encoding.UTF8.GetBytes(htmlScript), "text/html; charset=utf-8");

                }

            } else {

                return BadRequest(discardedBooksResp.Message);

            }

        }


        [HttpGet]
        public async Task<IActionResult> DonatedBooksReport() {

            if (!_sessionService.IsSessionActive()) {
                return RedirectToAction("Login", "Login");
            }

            var donatedBooksResp = await _donationService.GetDonatedBooks();

            if (donatedBooksResp.Successful) {

                List<DonatedBookModel> donatedBooks = donatedBooksResp.Data!;

                if (_settingsService.GetInt(Constants.REPORT_FORMAT_KEY, Constants.PDF_FORMAT) ==
                Constants.PDF_FORMAT) {

                    var pdfBytes = _pdfReportService.DonatedBooks(donatedBooks);

                    Response.Headers.Append(
                        "Content-Disposition",
                        "inline; filename=LivrosDoados.pdf"
                    );

                    return File(pdfBytes, "application/pdf");

                } else {

                    Response.Headers.Append(
                        "Content-Disposition",
                        "inline; filename=LivrosDoados.html"
                    );

                    Response.Headers.Append(
                        "Content-Type",
                        "text/html; charset=utf-8"
                    );

                    var htmlScript = _htmlReportService.DonatedBooksWithTitle(donatedBooks);

                    return File(System.Text.Encoding.UTF8.GetBytes(htmlScript), "text/html; charset=utf-8");

                }

            } else {

                return BadRequest(donatedBooksResp.Message);

            }

        }


        [HttpGet]
        public async Task<IActionResult> BorrowedBooksReport() {

            if (!_sessionService.IsSessionActive()) {
                return RedirectToAction("Login", "Login");
            }

            var loansResp = await _loanService.GetLoans();

            if (loansResp.Successful) {

                List<LoanModel> borrowedBooks = loansResp.Data!;

                if (_settingsService.GetInt(Constants.REPORT_FORMAT_KEY, Constants.PDF_FORMAT) ==
                Constants.PDF_FORMAT) {

                    var pdfBytes = _pdfReportService.BorrowedBooks(borrowedBooks);

                    Response.Headers.Append(
                        "Content-Disposition",
                        "inline; filename=LivrosEmprestados.pdf"
                    );

                    return File(pdfBytes, "application/pdf");

                } else {

                    Response.Headers.Append(
                        "Content-Disposition",
                        "inline; filename=LivrosEmprestados.html"
                    );

                    Response.Headers.Append(
                        "Content-Type",
                        "text/html; charset=utf-8"
                    );

                    var htmlScript = _htmlReportService.BorrowedBooksWithTitle(borrowedBooks);

                    return File(System.Text.Encoding.UTF8.GetBytes(htmlScript), "text/html; charset=utf-8");

                }

            } else {

                return BadRequest(loansResp.Message);

            }

        }


    }


}

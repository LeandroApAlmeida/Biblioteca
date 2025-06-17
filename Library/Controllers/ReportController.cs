using Library.Db.Models;
using Library.Services.Collection;
using Library.Services.Report;
using Library.Services.Session;
using Library.Services.User;
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

                    var pdfBytes = _pdfReportService.BookDetailed(id);

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

                    var htmlScript = _htmlReportService.BookDetailed(id, true);

                    return File(System.Text.Encoding.UTF8.GetBytes(htmlScript), "text/html; charset=utf-8");

                } 

            } else {

                return BadRequest(bookResp.Message);

            }

        }


        [HttpGet]
        public IActionResult BooksInTheCollectionReport() {

            if (!_sessionService.IsSessionActive()) {
                return RedirectToAction("Login", "Login");
            }

            if (_settingsService.GetInt(Constants.REPORT_FORMAT_KEY, Constants.PDF_FORMAT) ==
            Constants.PDF_FORMAT) {

                var pdfBytes = _pdfReportService.BooksInTheCollection();

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

                var htmlScript = _htmlReportService.BooksInTheCollection(true);

                return File(System.Text.Encoding.UTF8.GetBytes(htmlScript), "text/html; charset=utf-8");

            }

        }


        [HttpGet]
        public  IActionResult RegisteredBooksReport() {

            if (!_sessionService.IsSessionActive()) {
                return RedirectToAction("Login", "Login");
            }

            if (_settingsService.GetInt(Constants.REPORT_FORMAT_KEY, Constants.PDF_FORMAT) ==
            Constants.PDF_FORMAT) {

                var pdfBytes = _pdfReportService.RegisteredBooks();

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

                var htmlScript = _htmlReportService.RegisteredBooks(true);

                return File(System.Text.Encoding.UTF8.GetBytes(htmlScript), "text/html; charset=utf-8");

            }

        }


        [HttpGet]
        public IActionResult DiscardedBooksReport() {

            if (!_sessionService.IsSessionActive()) {
                return RedirectToAction("Login", "Login");
            }

            if (_settingsService.GetInt(Constants.REPORT_FORMAT_KEY, Constants.PDF_FORMAT) ==
            Constants.PDF_FORMAT) {

                var pdfBytes = _pdfReportService.DiscardedBooks();

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

                var htmlScript = _htmlReportService.DiscardedBooks(true);

                return File(System.Text.Encoding.UTF8.GetBytes(htmlScript), "text/html; charset=utf-8");

            }

        }


        [HttpGet]
        public IActionResult DonatedBooksReport() {

            if (!_sessionService.IsSessionActive()) {
                return RedirectToAction("Login", "Login");
            }

            if (_settingsService.GetInt(Constants.REPORT_FORMAT_KEY, Constants.PDF_FORMAT) ==
            Constants.PDF_FORMAT) {

                var pdfBytes = _pdfReportService.DonatedBooks();

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

                var htmlScript = _htmlReportService.DonatedBooks(true);

                return File(System.Text.Encoding.UTF8.GetBytes(htmlScript), "text/html; charset=utf-8");

            }

        }


        [HttpGet]
        public IActionResult BorrowedBooksReport() {

            if (!_sessionService.IsSessionActive()) {
                return RedirectToAction("Login", "Login");
            }

            if (_settingsService.GetInt(Constants.REPORT_FORMAT_KEY, Constants.PDF_FORMAT) ==
            Constants.PDF_FORMAT) {

                var pdfBytes = _pdfReportService.BorrowedBooks();

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

                var htmlScript = _htmlReportService.BorrowedBooks(true);

                return File(System.Text.Encoding.UTF8.GetBytes(htmlScript), "text/html; charset=utf-8");

            }

        }


    }


}

using Library.Models;
using Library.Services.BookService;
using Library.Services.DiscardService;
using Library.Services.DonationService;
using Library.Services.LoanService;
using Library.Services.ReportService;
using Microsoft.AspNetCore.Mvc;

namespace Library.Controllers {


    public class ReportController : Controller {


        private readonly IBookService _bookService;

        private readonly IDiscardService _discardService;

        private readonly IDonationService _donationService;

        private readonly ILoanService _loanService;

        private readonly ReportService _reportService;

        
        public ReportController(IBookService bookService, IDiscardService discardService,
        IDonationService donationService, ILoanService loanService,  ReportService reportService) {
            _reportService = reportService;
            _bookService = bookService;
            _discardService = discardService;
            _donationService = donationService;
            _loanService = loanService;
        }


        [HttpGet]
        public async Task<IActionResult> BookDetailedReport(Guid id) {

            var bookResp = await _bookService.GetBook(id);

            if (bookResp.Successful) {

                BookModel book = bookResp.Data!;

                var pdfBytes = _reportService.BookDetailed(book);

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

                return File(pdfBytes, "application/pdf");

            } else {

                return BadRequest(bookResp.Message);

            }

        }


        [HttpGet]
        public async Task<IActionResult> BooksInTheCollectionReport() {

            var booksResp = await _bookService.GetBooks();

            if (booksResp.Successful) {

                List<BookModel> books = booksResp.Data!;

                var pdfBytes = _reportService.BooksInTheCollection(books);

                Response.Headers.Append(
                    "Content-Disposition",
                    "inline; filename=LivrosNoAcervo.pdf"
                );

                return File(pdfBytes, "application/pdf");

            } else {

                return BadRequest(booksResp.Message);

            }

        }


        [HttpGet]
        public async Task<IActionResult> DiscardedBooksReport() {

            var discardedBooksResp = await _discardService.GetDiscardedBooks();

            if (discardedBooksResp.Successful) {

                List<DiscardedBookModel> books = discardedBooksResp.Data!;

                List<BookModel> booksList = new(books.Count);

                foreach (DiscardedBookModel discardedBook in books) {
                    booksList.Add(discardedBook.Book);
                }

                var pdfBytes = _reportService.DiscardedBooks(booksList);
                
                Response.Headers.Append(
                    "Content-Disposition",
                    "inline; filename=LivrosDescartados.pdf"
                );
                
                return File(pdfBytes, "application/pdf");

            } else {

                return BadRequest(discardedBooksResp.Message);

            }

        }


        [HttpGet]
        public async Task<IActionResult> DonatedBooksReport() {

            var donatedBooksResp = await _donationService.GetDonatedBooks();

            if (donatedBooksResp.Successful) {

                var pdfBytes = _reportService.DonatedBooks(donatedBooksResp.Data!);
                
                Response.Headers.Append(
                    "Content-Disposition",
                    "inline; filename=LivrosDoados.pdf"
                );
                
                return File(pdfBytes, "application/pdf");

            } else {

                return BadRequest(donatedBooksResp.Message);

            }

        }


        [HttpGet]
        public async Task<IActionResult> BorrowedBooksReport() {

            var loansResp = await _loanService.GetLoans();

            if (loansResp.Successful) {

                var pdfBytes = _reportService.BorrowedBooks(loansResp.Data!);

                Response.Headers.Append(
                    "Content-Disposition",
                    "inline; filename=LivrosEmprestados.pdf"
                );

                return File(pdfBytes, "application/pdf");

            } else {

                return BadRequest(loansResp.Message);

            }

        }


    }


}

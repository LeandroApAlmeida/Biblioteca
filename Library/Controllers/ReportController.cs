using Library.Db.Models;
using Library.Services.Collection;
using Library.Services.Report;
using Library.Services.Session;
using Library.Services.User;
using Library.Utils;
using Microsoft.AspNetCore.Mvc;

namespace Library.Controllers {


    /// <summary>
    /// Controler para renderização de relatórios.
    /// </summary>
    public class ReportController : Controller {


        /// <summary> Objeto para manutenção de livros. </summary>
        private readonly IBookService _bookService;

        /// <summary> Objeto para gerenciamento do acervo. </summary>
        private readonly ICollectionService _collectionService;

        /// <summary> Objeto para manutenção de livros descartados. </summary>
        private readonly IDiscardService _discardService;

        /// <summary> Objeto para manutenção de livros doados. </summary>
        private readonly IDonationService _donationService;

        /// <summary> Objeto para manutenção de empréstimos. </summary>
        private readonly ILoanService _loanService;

        /// <summary> Objeto para geração de relatórios em formato PDF. </summary>
        private readonly IPdfReportService _pdfReportService;

        /// <summary> Objeto para geração de relatórios em formato HTML. </summary>
        private readonly IHtmlReportService _htmlReportService;

        /// <summary> Objeto para gerenciamento de sessão do usuário. </summary>
        private readonly ISessionService _sessionService;

        /// <summary> Objeto para acesso às configurações do usuário. </summary>
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


        /// <summary>
        /// Renderizar o relatório "Detalhes do Livro".
        /// </summary>
        /// <param name="id">Identificador chave primária do livro.</param>
        /// <returns>Página "Relatório Detalhes do Livro".</returns>
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

                    // Relatório no formato PDF.

                    Response.Headers.Append(
                        "Content-Disposition",
                        "inline; filename=" +
                        book.Title
                        .Replace(",", "")
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

                    // Relatório no formato HTML.

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


        /// <summary>
        /// Renderizar o relatório "Livros no Acervo".
        /// </summary>
        /// <returns>Página "Relatório Livros no Acervo".</returns>
        [HttpGet]
        public IActionResult BooksInTheCollectionReport() {

            if (!_sessionService.IsSessionActive()) {
                return RedirectToAction("Login", "Login");
            }

            if (_settingsService.GetInt(Constants.REPORT_FORMAT_KEY, Constants.PDF_FORMAT) ==
            Constants.PDF_FORMAT) {

                // Relatório no formato PDF.

                var pdfBytes = _pdfReportService.BooksInTheCollection();

                Response.Headers.Append(
                    "Content-Disposition",
                    "inline; filename=LivrosNoAcervo.pdf"
                );

                return File(pdfBytes, "application/pdf");

            } else {

                // Relatório no formato HTML.

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


        /// <summary>
        /// Renderizar o relatório "Livros Cadastrados".
        /// </summary>
        /// <returns>Página "Relatório Livros Cadastrados".</returns>
        [HttpGet]
        public  IActionResult RegisteredBooksReport() {

            if (!_sessionService.IsSessionActive()) {
                return RedirectToAction("Login", "Login");
            }

            if (_settingsService.GetInt(Constants.REPORT_FORMAT_KEY, Constants.PDF_FORMAT) ==
            Constants.PDF_FORMAT) {

                // Relatório no formato PDF.

                var pdfBytes = _pdfReportService.RegisteredBooks();

                Response.Headers.Append(
                    "Content-Disposition",
                    "inline; filename=LivrosCadastrados.pdf"
                );

                return File(pdfBytes, "application/pdf");

            } else {

                // Relatório no formato HTML.

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


        /// <summary>
        /// Renderizar o relatório "Livros Descartados".
        /// </summary>
        /// <returns>Página "Relatório Livros Descartados".</returns>
        [HttpGet]
        public IActionResult DiscardedBooksReport() {

            if (!_sessionService.IsSessionActive()) {
                return RedirectToAction("Login", "Login");
            }

            if (_settingsService.GetInt(Constants.REPORT_FORMAT_KEY, Constants.PDF_FORMAT) ==
            Constants.PDF_FORMAT) {

                // Relatório no formato PDF.

                var pdfBytes = _pdfReportService.DiscardedBooks();

                Response.Headers.Append(
                    "Content-Disposition",
                    "inline; filename=LivrosDescartados.pdf"
                );

                return File(pdfBytes, "application/pdf");

            } else {

                // Relatório no formato HTML.

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


        /// <summary>
        /// Renderizar o relatório "Livros Doados".
        /// </summary>
        /// <returns>Página "Relatório Livros Doados".</returns>
        [HttpGet]
        public IActionResult DonatedBooksReport() {

            if (!_sessionService.IsSessionActive()) {
                return RedirectToAction("Login", "Login");
            }

            if (_settingsService.GetInt(Constants.REPORT_FORMAT_KEY, Constants.PDF_FORMAT) ==
            Constants.PDF_FORMAT) {

                // Relatório no formato PDF.

                var pdfBytes = _pdfReportService.DonatedBooks();

                Response.Headers.Append(
                    "Content-Disposition",
                    "inline; filename=LivrosDoados.pdf"
                );

                return File(pdfBytes, "application/pdf");

            } else {

                // Relatório no formato HTML.

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


        /// <summary>
        /// Renderizar o relatório "Livros Emprestados".
        /// </summary>
        /// <returns>Página "Relatório Livros Emprestados".</returns>
        [HttpGet]
        public IActionResult BorrowedBooksReport() {

            if (!_sessionService.IsSessionActive()) {
                return RedirectToAction("Login", "Login");
            }

            if (_settingsService.GetInt(Constants.REPORT_FORMAT_KEY, Constants.PDF_FORMAT) ==
            Constants.PDF_FORMAT) {

                // Relatório no formato PDF.

                var pdfBytes = _pdfReportService.BorrowedBooks();

                Response.Headers.Append(
                    "Content-Disposition",
                    "inline; filename=LivrosEmprestados.pdf"
                );

                return File(pdfBytes, "application/pdf");

            } else {

                // Relatório no formato HTML.

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

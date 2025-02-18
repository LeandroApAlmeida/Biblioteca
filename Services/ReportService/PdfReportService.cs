﻿using DinkToPdf;
using DinkToPdf.Contracts;
using Library.Models;
using System.Text;

namespace Library.Services.ReportService {


    public class PdfReportService: IPdfReportService {


        private readonly IConverter _converter;

        private readonly IHtmlReportService _htmlReportService;


        public PdfReportService(IConverter converter, IHtmlReportService htmlReportService) {
            _converter = converter;
            _htmlReportService = htmlReportService;
        }


        private GlobalSettings GetDefaultGlobalSettings(string title, PaperKind paperSize,
        Orientation orientation) {

            return new GlobalSettings {

                ColorMode = ColorMode.Color,

                Orientation = orientation,

                PaperSize = paperSize,

                Margins = new MarginSettings { Top = 18, Bottom = 18, Left = 10, Right = 10 },

                DocumentTitle = title

            };

        }


        private ObjectSettings GetDefaultObjectSettings(string footerText, string htmlContent) {


            return new ObjectSettings {

                PagesCount = true,

                HtmlContent = htmlContent,

                WebSettings = {
                    DefaultEncoding = "utf-8"
                },

                HeaderSettings = {
                    FontName = "Arial",
                    FontSize = 10,
                    Left = footerText,
                    Line = true,
                    Spacing = 5
                },

                FooterSettings = {
                    FontName = "Arial",
                    FontSize = 10,
                    Line = true,
                    Left = "Impresso em: " + DateTime.Now.ToString(),
                    Right = "Pág. [page] de [toPage]",
                    Spacing = 5
                }

            };

        }


        public byte[] BookDetailed(BookModel book) {

            var objectSettings = GetDefaultObjectSettings(
                "Detalhes do Livro",
                _htmlReportService.BookDetailed(book)
            );

            var pdf = new HtmlToPdfDocument {

                GlobalSettings = GetDefaultGlobalSettings(
                    "Detalhes do Livro",
                    PaperKind.A4,
                    Orientation.Portrait
                ),

                Objects = { objectSettings }

            };

            return _converter.Convert(pdf);

        }


        public byte[] BooksInTheCollection(IEnumerable<BookModel> booksList) {
            
            var objectSettings = GetDefaultObjectSettings(
                "Livros no Acervo",
                _htmlReportService.BooksInTheCollection(booksList)
            );

            var pdf = new HtmlToPdfDocument {

                GlobalSettings = GetDefaultGlobalSettings(
                    "Livros no Acervo",
                    PaperKind.A4,
                    Orientation.Landscape
                ),

                Objects = { objectSettings }

            };

            return _converter.Convert(pdf);

        }


        byte[] IPdfReportService.RegisteredBooks(IEnumerable<BookModel> booksList) {

            var objectSettings = GetDefaultObjectSettings(
                "Livros Cadastrados",
                _htmlReportService.RegisteredBooks(booksList)
            );

            var pdf = new HtmlToPdfDocument {

                GlobalSettings = GetDefaultGlobalSettings(
                    "Livros Cadastrados",
                    PaperKind.A4,
                    Orientation.Landscape
                ),

                Objects = { objectSettings }

            };

            return _converter.Convert(pdf);

        }


        public byte[] DiscardedBooks(IEnumerable<DiscardedBookModel> discardedBooksList) {

            var objectSettings = GetDefaultObjectSettings(
                "Livros Descartados",
                _htmlReportService.DiscardedBooks(discardedBooksList)
            );

            var pdf = new HtmlToPdfDocument {

                GlobalSettings = GetDefaultGlobalSettings(
                    "Livros Descartados",
                    PaperKind.A4,
                    Orientation.Landscape
                ),

                Objects = { objectSettings }

            };

            return _converter.Convert(pdf);

        }


        public byte[] DonatedBooks(IEnumerable<DonatedBookModel> donatedBooksList) {

            var objectSettings = GetDefaultObjectSettings(
                "Livros Doados",
                _htmlReportService.DonatedBooks(donatedBooksList)
            );

            var pdf = new HtmlToPdfDocument {

                GlobalSettings = GetDefaultGlobalSettings(
                    "Livros Doados",
                    PaperKind.A4,
                    Orientation.Landscape
                ),

                Objects = { objectSettings }

            };

            return _converter.Convert(pdf);

        }


        public byte[] BorrowedBooks(IEnumerable<LoanModel> loanList) {

            var objectSettings = GetDefaultObjectSettings(
                "Livros Emprestados",
                _htmlReportService.BorrowedBooks(loanList)
            );

            var pdf = new HtmlToPdfDocument {

                GlobalSettings = GetDefaultGlobalSettings(
                    "Livros Emprestados",
                    PaperKind.A4,
                    Orientation.Landscape
                ),

                Objects = { objectSettings }

            };

            return _converter.Convert(pdf);

        }

        
    }


}

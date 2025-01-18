﻿using DinkToPdf;
using DinkToPdf.Contracts;
using Library.Models;
using System.Text;

namespace Library.Services.ReportService {


    public class PdfReportService: IReportService {


        private readonly IConverter _converter;


        public PdfReportService(IConverter converter) {
            _converter = converter;
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

                $@"
                <html>

                    <head>

                        <style> 
                            
                            .title {{ 
                                font-family: Arial;
                                font-size: 18px;
                                font-weight: bold;
                            }} 
                            
                            .field {{ 
                                font-family: Arial;
                                font-size: 18px;
                                margin-bottom: 15px;
                            }}
                            
                            .center {{
                                display: block;
                                margin-left: auto;
                                margin-right: auto;
                            }}
                            
                            img {{ width: 400px; }}
                        
                        </style>

                    </head>

                    <body> 

                        <div class='title' align=""justify"">{book.Title + (book.Subtitle != null && book.Subtitle.Length > 0 ?
                            " - " + book.Subtitle : "")}</div>
                        
                        </br></br>
                        
                        <div id=""img-preview""><img src=""{book.Cover}"" class=""center"" /></div>
                        
                        </br></br>
                        
                        <div class='field'><b>Autor:</b> {book.Author}</div>
                        
                        <div class='field'><b>Editora:</b> {book.Publisher}</div>
                        
                        <div class='field'><b>ISBN:</b> {book.Isbn}</div>
                        
                        <div class='field'><b>Edição:</b> {book.Edition}</div>
                        
                        <div class='field'><b>Volume:</b> {book.Volume}</div>
                        
                        <div class='field'><b>Ano da publicação:</b> {book.ReleaseYear}</div>
                        
                        <div class='field'><b>Núm. de Páginas:</b> {book.NumberOfPages}</div>
                        
                        <div class='field'><b>Data da Aquisição:</b> {book.AcquisitionDate:dd/MM/yyyy}</div>
                        
                        </br></br>
                        
                        <div class='field'><b>Sinopse:</b> <p id=""summary"" align=""justify"">{book.Summary?.Replace("\n", "</br>")}</p></div> 
                        
                    </body>

                </html>

                "
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

            StringBuilder sb = new StringBuilder();

            if (booksList != null) {

                foreach (BookModel book in booksList) {

                    sb.Append($@"
                        <tr>
                            <td>{book.Title + (book.Subtitle != null ? " - " + book.Subtitle : "")}<d>
                            <td>{book.Author}</td>
                            <td>{book.Publisher}</td>
                            <td>{book.Isbn}</td>
                            <td class=""num-span"">{book.Edition}</td>
                            <td class=""num-span"">{book.Volume}</td>
                            <td class=""num-span"">{book.ReleaseYear}</td>
                            <td class=""num-span"">{book.NumberOfPages}</td>
                            <td >{book.AcquisitionDate:dd/MM/yyyy}</td>                    
                        </tr>    
                    ");

                }

            }

            var objectSettings = GetDefaultObjectSettings(

                "Livros no Acervo",

                $@"
                <html>

                    <head>

                        <style> 

                            table {{
                                border-collapse: collapse;
                                width: 100%;
                            }}

                            td {{
                                text-align: left;
                                padding: 8px;
                                font-family: Arial;
                                font-size: 16px;
                                text-align: left;
                                vertical-align: top;
                            }}

                            th {{
                                text-align: left;
                                padding: 8px;
                                margin-bottom: 10px;
                                font-weight: bold;
                                font-family: Arial;
                                font-size: 16px;
                                color: white;
                                background: black;
                                text-align: left;
                                vertical-align: top;
                            }}

                            tr:nth-child(even) {{
                                background: #e9e9e9;
                            }}

                            thead {{display: table-header-group; }}

                            tbody tr {{ page-break-inside: avoid; }}
                            
                        </style>

                    </head>

                    <body>
                       
                        <table class=""table"" border=""0"">

                            <thead>

                                <tr>
                                    <th ><u>Título e Subtítulo</u></th>
                                    <th style=""width: 16%;"">Autor</th>
                                    <th style=""width: 14%;"">Editora</th>
                                    <th style=""width: 12%;"">ISBN</th>
                                    <th style=""width: 2%;"">Ed.</th>
                                    <th style=""width: 2%;"">Vol.</th>
                                    <th style=""width: 2%;"">Pub.</th>
                                    <th style=""width: 2%;"">Pgs</th>
                                    <th style=""width: 5%;"">Aquisição</th>
                                </tr>

                            </thead>

                            <tbody>

                                {sb.ToString()}

                            </tbody>

                        </table>

                    </body>

                </html>

                "

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


        public byte[] DiscardedBooks(IEnumerable<DiscardedBookModel> discardedBooksList) {

            StringBuilder sb = new StringBuilder();

            if (discardedBooksList != null) {

                foreach (DiscardedBookModel donatedBook in discardedBooksList) {

                    sb.Append($@"
                        <tr>
                            <td>{donatedBook.Book.Title + (donatedBook.Book.Subtitle != null ? " - " + 
                            donatedBook.Book.Subtitle : "")}<d>
                            <td>{donatedBook.Book.Author}</td>
                            <td>{donatedBook.Book.Publisher}</td>
                            <td>{donatedBook.Book.Isbn}</td>
                            <td class=""num-span"">{donatedBook.Book.Edition}</td>
                            <td class=""num-span"">{donatedBook.Book.Volume}</td>
                            <td class=""num-span"">{donatedBook.Book.ReleaseYear}</td>
                            <td class=""num-span"">{donatedBook.Book.NumberOfPages}</td>
                            <td >{donatedBook.Book.AcquisitionDate:dd/MM/yyyy}</td>
                            <td >{donatedBook.Date:dd/MM/yyyy}</td>
                        </tr>    
                    ");

                }

            }

            var objectSettings = GetDefaultObjectSettings(

                "Livros Descartados",

                $@"
                <html>

                    <head>

                        <style> 

                            table {{
                                border-collapse: collapse;
                                width: 100%;
                            }}

                            td {{
                                text-align: left;
                                padding: 8px;
                                font-family: Arial;
                                font-size: 16px;
                                text-align: left;
                                vertical-align: top;
                            }}

                            th {{
                                text-align: left;
                                padding: 8px;
                                margin-bottom: 10px;
                                font-weight: bold;
                                font-family: Arial;
                                font-size: 16px;
                                color: white;
                                background: black;
                                text-align: left;
                                vertical-align: top;
                            }}

                            tr:nth-child(even) {{
                                background: #e9e9e9;
                            }}

                            thead {{display: table-header-group; }}

                            tbody tr {{ page-break-inside: avoid; }}
                            
                        </style>

                    </head>

                    <body>
                       
                        <table class=""table"" border=""0"">

                            <thead>

                                <tr>
                                    <th ><u>Título e Subtítulo</u></th>
                                    <th style=""width: 16%;"">Autor</th>
                                    <th style=""width: 14%;"">Editora</th>
                                    <th style=""width: 12%;"">ISBN</th>
                                    <th style=""width: 2%;"">Ed.</th>
                                    <th style=""width: 2%;"">Vol.</th>
                                    <th style=""width: 2%;"">Pub.</th>
                                    <th style=""width: 2%;"">Pgs</th>
                                    <th style=""width: 5%;"">Aquisição</th>
                                    <th style=""width: 5%;"">Descarte</th>
                                </tr>

                            </thead>

                            <tbody>

                                {sb.ToString()}

                            </tbody>

                        </table>

                    </body>

                </html>

                "

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

            StringBuilder sb = new StringBuilder();

            if (donatedBooksList != null) {

                foreach (DonatedBookModel donatedBook in donatedBooksList) {

                    sb.Append($@"
                        <tr>
                            <td>{donatedBook.Book.Title + (donatedBook.Book.Subtitle != null ? " - " + donatedBook.Book.Subtitle : "")}<d>
                            <td>{donatedBook.Book.Author}</td>
                            <td>{donatedBook.Book.Publisher}</td>
                            <td>{donatedBook.Book.Isbn}</td>
                            <td class=""num-span"">{donatedBook.Book.Edition}</td>
                            <td class=""num-span"">{donatedBook.Book.Volume}</td>
                            <td class=""num-span"">{donatedBook.Book.ReleaseYear}</td>
                            <td class=""num-span"">{donatedBook.Book.NumberOfPages}</td>
                            <td >{donatedBook.Book.AcquisitionDate:dd/MM/yyyy}</td>
                            <td >{donatedBook.Date:dd/MM/yyyy}</td>
                            <td >{donatedBook.Person.Name}</td>  
                        </tr>    
                    ");

                }

            }

            var objectSettings = GetDefaultObjectSettings(

                "Livros Doados",

                $@"
                <html>

                    <head>

                        <style> 

                            table {{
                                border-collapse: collapse;
                                width: 100%;
                            }}

                            td {{
                                text-align: left;
                                padding: 8px;
                                font-family: Arial;
                                font-size: 16px;
                                text-align: left;
                                vertical-align: top;
                            }}

                            th {{
                                text-align: left;
                                padding: 8px;
                                margin-bottom: 10px;
                                font-weight: bold;
                                font-family: Arial;
                                font-size: 16px;
                                color: white;
                                background: black;
                                text-align: left;
                                vertical-align: top;
                            }}

                            tr:nth-child(even) {{
                                background: #e9e9e9;
                            }}

                            thead {{display: table-header-group; }}

                            tbody tr {{ page-break-inside: avoid; }}
                            
                        </style>

                    </head>

                    <body>
                       
                        <table class=""table"" border=""0"">

                            <thead>

                                <tr>
                                    <th><u>Título e Subtítulo</u></th>
                                    <th style=""width: 12%;"">Autor</th>
                                    <th style=""width: 12%;"">Editora</th>
                                    <th style=""width: 11%;"">ISBN</th>
                                    <th style=""width: 2%;"">Ed.</th>
                                    <th style=""width: 2%;"">Vol.</th>
                                    <th style=""width: 2%;"">Pub.</th>
                                    <th style=""width: 4%;"">Pgs</th>
                                    <th style=""width: 5%;"">Aquisição</th>
                                    <th style=""width: 5%;"">Doação</th>
                                    <th style=""width: 12%;"">Beneficiário</th>
                                </tr>

                            </thead>

                            <tbody>

                                {sb.ToString()}

                            </tbody>

                        </table>

                    </body>

                </html>

                "

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

            StringBuilder sb = new StringBuilder();

            if (loanList != null) {

                foreach (LoanModel loan in loanList) {

                    sb.Append($@"
                        <tr>
                            <td>{loan.Book.Title + (loan.Book.Subtitle != null ? " - " + loan.Book.Subtitle : "")}<d>
                            <td>{loan.Book.Author}</td>
                            <td>{loan.Book.Publisher}</td>
                            <td>{loan.Book.Isbn}</td>
                            <td class=""num-span"">{loan.Book.Edition}</td>
                            <td class=""num-span"">{loan.Book.Volume}</td>
                            <td class=""num-span"">{loan.Book.ReleaseYear}</td>
                            <td class=""num-span"">{loan.Book.NumberOfPages}</td>
                            <td >{loan.Book.AcquisitionDate:dd/MM/yyyy}</td>
                            <td >{loan.Date:dd/MM/yyyy}</td>
                            <td >{loan.ReturnDate:dd/MM/yyyy}</td>
                            <td >{loan.Person.Name}</td>  
                        </tr>    
                    ");

                }

            }

            var objectSettings = GetDefaultObjectSettings(

                "Livros Emprestados",

                $@"
                <html>

                    <head>

                        <style> 

                            table {{
                                border-collapse: collapse;
                                width: 100%;
                            }}

                            td {{
                                text-align: left;
                                padding: 8px;
                                font-family: Arial;
                                font-size: 14px;
                                text-align: left;
                                vertical-align: top;
                            }}

                            th {{
                                text-align: left;
                                padding: 8px;
                                margin-bottom: 10px;
                                font-weight: bold;
                                font-family: Arial;
                                font-size: 14px;
                                color: white;
                                background: black;
                                text-align: left;
                                vertical-align: top;
                            }}

                            tr:nth-child(even) {{
                                background: #e9e9e9;
                            }}

                            thead {{display: table-header-group; }}

                            tbody tr {{ page-break-inside: avoid; }}
                            
                        </style>

                    </head>

                    <body>
                       
                        <table class=""table"" border=""0"">

                            <thead>

                                <tr>
                                    <th><u>Título e Subtítulo</u></th>
                                    <th style=""width: 12%;"">Autor</th>
                                    <th style=""width: 12%;"">Editora</th>
                                    <th style=""width: 10%;"">ISBN</th>
                                    <th style=""width: 2%;"">Ed.</th>
                                    <th style=""width: 2%;"">Vol.</th>
                                    <th style=""width: 2%;"">Pub.</th>
                                    <th style=""width: 4%;"">Pgs</th>
                                    <th style=""width: 4%;"">Aquisição</th>
                                    <th style=""width: 4%;"">Retirada</th>
                                    <th style=""width: 4%;"">Devolução</th>
                                    <th style=""width: 10%;"">Tomador</th>
                                </tr>

                            </thead>

                            <tbody>

                                {sb.ToString()}

                            </tbody>

                        </table>

                    </body>

                </html>

                "

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

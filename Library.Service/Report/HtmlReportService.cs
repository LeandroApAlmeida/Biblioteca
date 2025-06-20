using Library.Services.Model.Dto;
using Library.Services.Collection;
using Library.Services.User;
using System.Text;
using Library.Db.Models;

namespace Library.Services.Report {


    public class HtmlReportService : IHtmlReportService {


        private readonly ISettingsService _settingsService;

        private readonly ICollectionService _collectionService;

        private readonly IBookService _bookService;

        private readonly IDiscardService _discardService;

        private readonly IDonationService _donationService;

        private readonly ILoanService _loanService;


        public HtmlReportService(ISettingsService settingsService, ICollectionService collectionService,
        IBookService bookService, IDiscardService discardService, IDonationService donationService,
        ILoanService loanService) {
            _settingsService = settingsService;
            _collectionService = collectionService;
            _bookService = bookService;
            _discardService = discardService;
            _donationService = donationService;
            _loanService = loanService;
        }


        private string FormatReportTitle(string reportTitle) {

            DateTime now = DateTime.Now;

            string formattedDate = "Impresso em: " + now.ToString("dd/MM/yyyy, HH:mm:ss");

            string title = $@"

                <style>
                    .header {{
                        display: flex;
                        justify-content: space-between;
                        align-items: center;
                        font-family: Arial;
                        font-size: 16px;
                        font-weight: bold;
                        margin-bottom: 2px;                        
                    }}
                    
                </style>

                <div class=""header"">
                    <div class=""title"">{reportTitle}</div>
                    <div class=""date"" id=""currentDate"">{formattedDate}</div>
                </div>

                <hr style = ""margin-bottom: 30px;""/>

            ";

            return title;

        }


        public string BookDetailed(Guid id, bool renderTitle) {

            var bookResp = _bookService.GetBook(id).GetAwaiter().GetResult();

            BookModel? book;

            if (bookResp.Successful) {
                book = bookResp.Data!;
            } else {
                return $@"

                    <html>

                        <head>

                            <meta charset=""utf-8"" />
        
                            <meta name=""viewport"" content=""width=device-width, initial-scale=1.0"" />

                        </head>

                        <body> 

                        </body>

                    </html>

                ";
            }

            string htmlScript = $@"
                
                <html>

                    <head>

                        <meta charset=""utf-8"" />
        
                        <meta name=""viewport"" content=""width=device-width, initial-scale=1.0"" />

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

                        <link rel=""icon"" type=""image/png"" sizes=""16x16"" href=""/img/report_icon_24.png"" />

                    </head>

                    <body> 

                        <!--report-title-location-->

                        <div class='title' align=""justify"">{book.Title + (book.Subtitle != null && book.Subtitle.Length > 0 ?
                            " - " + book.Subtitle : "")}</div>
                        
                        </br></br>
                        
                        <div id=""img-preview""><img src=""{book.Cover.Data}"" class=""center"" /></div>
                        
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

            ";

            if (renderTitle) {

                return htmlScript.Replace(
                    "<!--report-title-location-->",
                    FormatReportTitle("Detalhes do Livro (versão HTML)")
                );

            } else {

                return htmlScript;

            }

        }


        public string BooksInTheCollection(bool renderTitle) {

            StringBuilder sb = new StringBuilder();

            var booksResp = _collectionService.GetCollectionBooks().GetAwaiter().GetResult();

            List<BookModel> booksList = new List<BookModel>();

            if (booksResp.Successful) {
                booksList = booksResp.Data!;
            }

            if (booksList != null) {

                foreach (BookModel book in booksList) {

                    sb.Append($@"
                        <tr>
                            <td>{book.Title + (book.Subtitle != null ? " - " + book.Subtitle : "")}</td>
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

            string htmlScript = $@"

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

                        <link rel=""icon"" type=""image/png"" sizes=""16x16"" href=""/img/report_icon_24.png"" />

                    </head>

                    <body>

                        <!--report-title-location-->
                       
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

            ";

            if (renderTitle) {

                return htmlScript.Replace(
                    "<!--report-title-location-->",
                    FormatReportTitle("Livros no Acervo (versão HTML)")
                );

            } else {

                return htmlScript;
            
            }

        }


        public string DiscardedBooks(bool renderTitle) {

            StringBuilder sb = new StringBuilder();

            List<DiscardedBookModel> discardedBooksList = new List<DiscardedBookModel>();

            var discardedBooksResp = _discardService.GetDiscardedBooks().GetAwaiter().GetResult();

            if (discardedBooksResp.Successful) {
                discardedBooksList = discardedBooksResp.Data!;
            }

            if (discardedBooksList != null) {

                foreach (DiscardedBookModel donatedBook in discardedBooksList) {

                    sb.Append($@"
                        <tr>
                            <td>{donatedBook.Book.Title + (donatedBook.Book.Subtitle != null ? " - " +
                            donatedBook.Book.Subtitle : "")}</td>
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

            string htmlScript = $@"

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

                        <link rel=""icon"" type=""image/png"" sizes=""16x16"" href=""/img/report_icon_24.png"" />

                    </head>

                    <body>

                        <!--report-title-location-->
                       
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

            ";

            if (renderTitle) {

                return htmlScript.Replace(
                    "<!--report-title-location-->",
                    FormatReportTitle("Livros Descartados (versão HTML)")
                );

            } else {

                return htmlScript;

            }

        }


        public string RegisteredBooks(bool renderTitle) {

            var booksResp = _bookService.GetBooks().GetAwaiter().GetResult();

            List<BookModel> booksList = new List<BookModel>();

            if (booksResp.Successful) {
                booksList = booksResp.Data!;
            }

            SettingsDto settings = new SettingsDto(_settingsService);

            // Cores de texto.
            string? discardedTextColor = settings.DiscardedTextColor;
            string? donatedTextColor = settings.DonatedTextColor;
            string? borrowedTextColor = settings.BorrowedTextColor;

            // Efeitos de fonte.
            const string FONT_BOLD = "font-weight:bold;";
            const string FONT_UNDERLINE = "text-decoration:underline;";
            const string FONT_ITALIC = "font-style:italic;";

            // Tags HTML para efeitos de fonte de livros descartados.
            string discardedFontStyle = "";

            if (settings.IsDiscardedBold != null && settings.IsDiscardedBold == true) discardedFontStyle += FONT_BOLD;
            if (settings.IsDiscardedUnderline != null && settings.IsDiscardedUnderline == true) discardedFontStyle += FONT_UNDERLINE;
            if (settings.IsDiscardedItalic != null && settings.IsDiscardedItalic == true) discardedFontStyle += FONT_ITALIC;

            // Tags HTML para efeitos de fonte de livros doados.
            string donatedFontStyle = "";

            if (settings.IsDonatedBold != null && settings.IsDonatedBold == true) donatedFontStyle += FONT_BOLD;
            if (settings.IsDonatedUnderline != null && settings.IsDonatedUnderline == true) donatedFontStyle += FONT_UNDERLINE;
            if (settings.IsDonatedItalic != null && settings.IsDonatedItalic == true) donatedFontStyle += FONT_ITALIC;

            // Tags HTML para efeitos de fonte de livros emprestados.
            string borrowedFontStyle = "";

            if (settings.IsBorrowedBold != null && settings.IsBorrowedBold == true) borrowedFontStyle += FONT_BOLD;
            if (settings.IsBorrowedUnderline != null && settings.IsBorrowedUnderline == true) borrowedFontStyle += FONT_UNDERLINE;
            if (settings.IsBorrowedItalic != null && settings.IsBorrowedItalic == true) borrowedFontStyle += FONT_ITALIC;

            StringBuilder sb = new StringBuilder();

            if (booksList != null) {

                foreach (BookModel book in booksList) {

                    string textColor = "color:";
                    string fontStyle = "";

                    if (book.IsBorrowed) {
                        textColor += borrowedTextColor;
                        fontStyle += borrowedFontStyle;
                    } else if (book.IsDiscarded) {
                        textColor += discardedTextColor;
                        fontStyle += discardedFontStyle;
                    } else if (book.IsDonated) {
                        textColor += donatedTextColor;
                        fontStyle += donatedFontStyle;
                    } else {
                        textColor += "black";
                    }

                    string textStyle = textColor + ";" + fontStyle;

                    sb.Append($@"
                        <tr>
                            <td style=""{textStyle}"">{book.Title + (book.Subtitle != null ? " - " + book.Subtitle : "")}</td>
                            <td style=""{textStyle}"">{book.Author}</td>
                            <td style=""{textStyle}"">{book.Publisher}</td>
                            <td style=""{textStyle}"">{book.Isbn}</td>
                            <td style=""{textStyle}"" class=""num-span"">{book.Edition}</td>
                            <td style=""{textStyle}"" class=""num-span"">{book.Volume}</td>
                            <td style=""{textStyle}"" class=""num-span"">{book.ReleaseYear}</td>
                            <td style=""{textStyle}"" class=""num-span"">{book.NumberOfPages}</td>
                            <td style=""{textStyle}"">{book.AcquisitionDate:dd/MM/yyyy}</td>                    
                        </tr>    
                    ");

                }

            }

            string htmlScript = $@"

                <html>

                    <head>

                        <style> 

                            table {{
                                border-collapse: collapse;
                                width: 100%;
                            }}

                            #table-1 td {{
                                text-align: left;
                                padding: 8px;
                                font-family: Arial;
                                font-size: 16px;
                                text-align: left;
                                vertical-align: top;
                            }}

                            #table-1 th {{
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

                            #table-1 tr:nth-child(even) {{
                                background: #e9e9e9;
                            }}

                            #table-1 thead {{display: table-header-group; }}

                            #table-1 tbody tr {{ page-break-inside: avoid; }}

                            #table-2 th {{
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

                            #table-2 td {{
                                text-align: left;
                                padding: 8px;
                                font-family: Arial;
                                font-size: 16px;
                                vertical-align: top;
                            }}

                            span {{
                                font-family: Arial;
                                font-size: 16px;
                            }}

                            .square {{
                                width: 22px;
                                height: 22px;
                                border: 1px solid #cccc;
                                border-radius: 12px;
                                display: inline-block;
                            }}
                            
                        </style>

                        <link rel=""icon"" type=""image/png"" sizes=""16x16"" href=""/img/report_icon_24.png"" />

                    </head>

                    <body>

                        <!--report-title-location-->
                       
                        <table id=""table-1"" class=""table"" border=""0"" >

                            <thead >

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

                        <table id = ""table-2"" class=""table"" border=""1"" style =""margin-top:25px;"" >

                            <thead >

                                <tr>
                                    <th style=""width: 3%;"">Cor</th>
                                    <th>Significado</th>
                                </tr>

                            </thead>

                            <tbody>

                                <tr>
                                    <td style="""" ><span class=""square"" style=""margin-left:5px;background-color:{discardedTextColor};""></span></td>
                                    <td >Livros que foram descartados</td>                  
                                </tr>

                                <tr>
                                    <td style="""" ><span class=""square"" style=""margin-left:5px;background-color:{donatedTextColor};""></span></td>
                                    <td >Livros que foram doados</td>                  
                                </tr>

                                <tr>
                                    <td style="""" ><span class=""square"" style=""margin-left:5px; margin-right:4px;background-color:{borrowedTextColor};""></span></td>
                                    <td >Livros que estão emprestados</td>                  
                                </tr>

                                <tr>
                                    <td style="""" ><span class=""square"" style=""margin-left:5px; margin-right:4px;background-color:black;""></span></td>
                                    <td >Livros disponíveis no acervo</td>                  
                                </tr>

                            </tbody>

                        </table>

                    </body>

                </html>

            ";

            if (renderTitle) {

                return htmlScript.Replace(
                    "<!--report-title-location-->",
                    FormatReportTitle("Livros Cadastrados (versão HTML)")
                );

            } else {
            
                return htmlScript;
            
            }

        }


        public string BorrowedBooks(bool renderTitle) {

            StringBuilder sb = new StringBuilder();

            var loansResp = _loanService.GetLoans().GetAwaiter().GetResult();

            List<LoanModel> loansList = new List<LoanModel>();

            if (loansResp.Successful) {
                loansList = loansResp.Data!;
            }

            if (loansList != null) {

                foreach (LoanModel loan in loansList) {

                    sb.Append($@"
                        <tr>
                            <td>{loan.Book.Title + (loan.Book.Subtitle != null ? " - " + loan.Book.Subtitle : "")}</td>
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

            string htmlScript = $@"

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

                        <link rel=""icon"" type=""image/png"" sizes=""16x16"" href=""/img/report_icon_24.png"" />

                    </head>

                    <body>

                        <!--report-title-location-->
                       
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

            ";

            if (renderTitle) {

                return htmlScript.Replace(
                    "<!--report-title-location-->",
                    FormatReportTitle("Livros Emprestados (versão HTML)")
                );

            } else {

                return htmlScript;
            
            }

        }


        public string DonatedBooks(bool renderTitle) {

            StringBuilder sb = new StringBuilder();

            var donatedBooksResp = _donationService.GetDonatedBooks().GetAwaiter().GetResult();

            List<DonatedBookModel> donatedBooksList = new List<DonatedBookModel>();

            if (donatedBooksResp.Successful) {
                donatedBooksList = donatedBooksResp.Data!;
            }

            if (donatedBooksList != null) {

                foreach (DonatedBookModel donatedBook in donatedBooksList) {

                    sb.Append($@"
                        <tr>
                            <td>{donatedBook.Book.Title + (donatedBook.Book.Subtitle != null ? " - " + donatedBook.Book.Subtitle : "")}</td>
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

            string htmlScript = $@"
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

                        <link rel=""icon"" type=""image/png"" sizes=""16x16"" href=""/img/report_icon_24.png"" />

                    </head>

                    <body>

                        <!--report-title-location-->
                       
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

            ";

            if (renderTitle) {

                return htmlScript.Replace(
                    "<!--report-title-location-->",
                    FormatReportTitle("Livros Doados (versão HTML)")
                );

            } else {

                return htmlScript;
            
            }

        }


    }


}
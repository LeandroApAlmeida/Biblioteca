using Library.Dto;
using Library.Models;
using Library.Services.SettingsService;
using System.Text;

namespace Library.Services.ReportService {


    public class HtmlReportService : IHtmlReportService {


        private readonly ISettingsService _settingsService;


        public HtmlReportService(ISettingsService settingsService) {
            _settingsService = settingsService;
        }


        public string BookDetailed(BookModel book) {

            return $@"
                
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

            ";

        }


        public string BooksInTheCollection(IEnumerable<BookModel> booksList) {

            StringBuilder sb = new StringBuilder();

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

            return $@"

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

            ";
        }


        public string RegisteredBooks(IEnumerable<BookModel> booksList) {

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

            return $@"

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
                       
                        <table class=""table"" border=""0"" >

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

                    </body>

                </html>

            ";

        }


        public string BorrowedBooks(IEnumerable<LoanModel> loanList) {

            StringBuilder sb = new StringBuilder();

            if (loanList != null) {

                foreach (LoanModel loan in loanList) {

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

            return $@"

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

            ";

        }


        public string DiscardedBooks(IEnumerable<DiscardedBookModel> discardedBooksList) {

            StringBuilder sb = new StringBuilder();

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

            return $@"

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

            ";

        }


        public string DonatedBooks(IEnumerable<DonatedBookModel> donatedBooksList) {

            StringBuilder sb = new StringBuilder();

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

            return $@"
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

            ";

        }


    }


}
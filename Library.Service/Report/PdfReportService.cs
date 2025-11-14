using DinkToPdf;
using DinkToPdf.Contracts;

namespace Library.Services.Report {


    /// <summary>
    /// Classe para geração de relatórios em formato PDF.
    /// </summary>
    public class PdfReportService: IPdfReportService {


        /// <summary> Conversor HTML para PDF do DinkToPdf. </summary>
        private readonly IConverter _converter;

        /// <summary> Objeto para geração de relatórios em HTML. </summary>
        private readonly IHtmlReportService _htmlReportService;


        public PdfReportService(IConverter converter, IHtmlReportService htmlReportService) {
            _converter = converter;
            _htmlReportService = htmlReportService;
        }


        /// <summary>
        /// Obter as configurações globais do PDF.
        /// </summary>
        /// <param name="title">Título do relatório.</param>
        /// <param name="paperSize">Tamanho do papel.</param>
        /// <param name="orientation">Orientação da página.</param>
        /// <returns>Configurações globais do PDF.</returns>
        private GlobalSettings GetGlobalSettings(string title, PaperKind paperSize, Orientation orientation) {

            return new GlobalSettings {

                ColorMode = ColorMode.Color,

                Orientation = orientation,

                PaperSize = paperSize,

                Margins = new MarginSettings { Top = 18, Bottom = 18, Left = 10, Right = 10 },

                DocumentTitle = title

            };

        }


        /// <summary>
        /// Obter as configurações do PDF.
        /// </summary>
        /// <param name="footerText">Texto do rodapé da página.</param>
        /// <param name="htmlContent">Conteúdo da página, em formato HTML.</param>
        /// <returns>Configurações do PDF.</returns>
        private ObjectSettings GetObjectSettings(string footerText, string htmlContent) {


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
                    Left = "Impresso em: " + DateTime.Now.ToString("dd/MM/yyyy, HH:mm:ss"),
                    Right = "Pág. [page] de [toPage]",
                    Spacing = 5
                }

            };

        }


        public byte[] BookDetailed(Guid id) {

            var objectSettings = GetObjectSettings(
                "Detalhes do Livro",
                _htmlReportService.BookDetailed(id, false)
            );

            var pdf = new HtmlToPdfDocument {

                GlobalSettings = GetGlobalSettings(
                    "Detalhes do Livro",
                    PaperKind.A4,
                    Orientation.Portrait
                ),

                Objects = { objectSettings }

            };

            return _converter.Convert(pdf);

        }


        public byte[] BooksInTheCollection() {
            
            var objectSettings = GetObjectSettings(
                "Livros no Acervo",
                _htmlReportService.BooksInTheCollection(false)
            );

            var pdf = new HtmlToPdfDocument {

                GlobalSettings = GetGlobalSettings(
                    "Livros no Acervo",
                    PaperKind.A4,
                    Orientation.Landscape
                ),

                Objects = { objectSettings }

            };

            return _converter.Convert(pdf);

        }


        public byte[] RegisteredBooks() {

            var objectSettings = GetObjectSettings(
                "Livros Cadastrados",
                _htmlReportService.RegisteredBooks(false)
            );

            var pdf = new HtmlToPdfDocument {

                GlobalSettings = GetGlobalSettings(
                    "Livros Cadastrados",
                    PaperKind.A4,
                    Orientation.Landscape
                ),

                Objects = { objectSettings }

            };

            return _converter.Convert(pdf);

        }


        public byte[] DiscardedBooks() {

            var objectSettings = GetObjectSettings(
                "Livros Descartados",
                _htmlReportService.DiscardedBooks(false)
            );

            var pdf = new HtmlToPdfDocument {

                GlobalSettings = GetGlobalSettings(
                    "Livros Descartados",
                    PaperKind.A4,
                    Orientation.Landscape
                ),

                Objects = { objectSettings }

            };

            return _converter.Convert(pdf);

        }


        public byte[] DonatedBooks() {

            var objectSettings = GetObjectSettings(
                "Livros Doados",
                _htmlReportService.DonatedBooks(false)
            );

            var pdf = new HtmlToPdfDocument {

                GlobalSettings = GetGlobalSettings(
                    "Livros Doados",
                    PaperKind.A4,
                    Orientation.Landscape
                ),

                Objects = { objectSettings }

            };

            return _converter.Convert(pdf);

        }


        public byte[] BorrowedBooks() {

            var objectSettings = GetObjectSettings(
                "Livros Emprestados",
                _htmlReportService.BorrowedBooks(false)
            );

            var pdf = new HtmlToPdfDocument {

                GlobalSettings = GetGlobalSettings(
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

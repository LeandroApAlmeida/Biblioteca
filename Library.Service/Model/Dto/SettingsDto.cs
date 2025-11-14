using Library.Services.User;
using Library.Utils;

namespace Library.Services.Model.Dto {


    /// <summary>
    /// DTO para configuração do ambiente.
    /// </summary>
    public class SettingsDto {


        /// <summary>
        /// Constructor da classe
        /// </summary>
        /// <param name="settingsService">Objeto para gerenciamento das configurações do ambiente.</param>
        public SettingsDto(ISettingsService settingsService) {


            BorrowedTextColor = settingsService.GetString(
                Constants.BORROWED_TEXT_COLOR_KEY,
                Constants.DEFAULT_BORROWED_TEXT_COLOR
            );

            DonatedTextColor = settingsService.GetString(
                Constants.DONATED_TEXT_COLOR_KEY,
                Constants.DEFAULT_DONATED_TEXT_COLOR
            );

            DiscardedTextColor = settingsService.GetString(
                Constants.DISCARDED_TEXT_COLOR_KEY,
                Constants.DEFAULT_DISCARDED_TEXT_COLOR
            );

            PageBackgroundColor = settingsService.GetString(
                Constants.PAGE_BACKGROUND_COLOR_KEY,
                Constants.DEFAULT_PAGE_BACKGROUND_COLOR
            );


            IsDiscardedBold = settingsService.GetBoolean(
                Constants.DISCARDED_BOLD_KEY,
                false
            );

            IsDiscardedUnderline = settingsService.GetBoolean(
                Constants.DISCARDED_UNDERLINE_KEY,
                false
            );

            IsDiscardedItalic = settingsService.GetBoolean(
                Constants.DISCARDED_ITALIC_KEY,
                false
            );


            IsDonatedBold = settingsService.GetBoolean(
                Constants.DONATED_BOLD_KEY,
                false
            );

            IsDonatedUnderline = settingsService.GetBoolean(
                Constants.DONATED_UNDERLINE_KEY,
                false
            );

            IsDonatedItalic = settingsService.GetBoolean(
                Constants.DONATED_ITALIC_KEY,
                false
            );


            IsBorrowedBold = settingsService.GetBoolean(
                Constants.BORROWED_BOLD_KEY,
                false
            );

            IsBorrowedUnderline = settingsService.GetBoolean(
                Constants.BORROWED_UNDERLINE_KEY,
                false
            );

            IsBorrowedItalic = settingsService.GetBoolean(
                Constants.BORROWED_ITALIC_KEY,
                false
            );

            IsApplyStylesToLists = settingsService.GetBoolean(
                Constants.APPLY_STYLES_TO_LISTS_KEY,
                false
            );

            IsShowFooterCaption = settingsService.GetBoolean(
                Constants.SHOW_FOOTER_CAPTION_KEY,
                false
            );

            ReportFormat = settingsService.GetInt(
                Constants.REPORT_FORMAT_KEY,
                Constants.DEFAULT_REPORT_FORMAT
            );


        }


        /// <summary> Cor do texto para livros emprestados. </summary>
        public string? BorrowedTextColor { get; set; }

        /// <summary> Cor do texto para livros doados. </summary>
        public string? DonatedTextColor { get; set; }

        /// <summary> Cor do texto para livros descartados. </summary>
        public string? DiscardedTextColor { get; set; }

        /// <summary> Cor de fundo das páginas. </summary>
        public string? PageBackgroundColor { get; set; }


        /// <summary> Aplicar negrito para livros descartados. </summary>
        public bool? IsDiscardedBold { get; set; }

        /// <summary> Aplicar sublinhado para livros descartados. </summary>
        public bool? IsDiscardedUnderline { get; set; }

        /// <summary> Aplicar itálico para livros descartados. </summary>
        public bool? IsDiscardedItalic { get; set; }


        /// <summary> Aplicar negrito para livros doados. </summary>
        public bool? IsDonatedBold { get; set; }

        /// <summary> Aplicar sublinhado para livros doados. </summary>
        public bool? IsDonatedUnderline { get; set; }

        /// <summary> Aplicar itálico para livros doados. </summary>
        public bool? IsDonatedItalic { get; set; }


        /// <summary> Aplicar negrito para livros emprestados. </summary>
        public bool? IsBorrowedBold { get; set; }

        /// <summary> Aplicar sublinhado para livros emprestados. </summary>
        public bool? IsBorrowedUnderline { get; set; }

        /// <summary> Aplicar itálico para livros emprestados. </summary>
        public bool? IsBorrowedItalic { get; set; }


        /// <summary> Aplicar os efeitos de fonte à respectivas listas de livros. </summary>
        public bool? IsApplyStylesToLists { get; set; }


        /// <summary> Mostrar legenda de cores no rodapé das páginas. </summary>
        public bool? IsShowFooterCaption { get; set; }


        /// <summary> Formato do relatório (PDF|HTML). </summary>
        public int? ReportFormat { get; set; }


    }


}
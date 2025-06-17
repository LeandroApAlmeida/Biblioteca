using Library.Services.User;
using Library.Utils;

namespace Library.Services.Model.Dto {


    public class SettingsDto {


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
                Constants.SHOW_FOOTER_CAPTION,
                false
            );

            ReportFormat = settingsService.GetInt(
                Constants.REPORT_FORMAT_KEY,
                Constants.DEFAULT_REPORT_FORMAT
            );


        }


        public string? BorrowedTextColor { get; set; }

        public string? DonatedTextColor { get; set; }

        public string? DiscardedTextColor { get; set; }

        public string? PageBackgroundColor { get; set; }


        public bool? IsDiscardedBold { get; set; }

        public bool? IsDiscardedUnderline { get; set; }

        public bool? IsDiscardedItalic { get; set; }


        public bool? IsDonatedBold { get; set; }

        public bool? IsDonatedUnderline { get; set; }

        public bool? IsDonatedItalic { get; set; }


        public bool? IsBorrowedBold { get; set; }

        public bool? IsBorrowedUnderline { get; set; }

        public bool? IsBorrowedItalic { get; set; }


        public bool? IsApplyStylesToLists { get; set; }


        public bool? IsShowFooterCaption { get; set; }


        public int? ReportFormat { get; set; }


    }


}
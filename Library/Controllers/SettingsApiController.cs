using Library.Services.Session;
using Library.Services.User;
using Library.Utils;
using Microsoft.AspNetCore.Mvc;

namespace Library.Controllers {


    [ApiController]
    [Route("Api/[controller]")]
    public class SettingsApiController : Controller {


        private readonly ISettingsService _settingsService;

        private readonly ISessionService _sessionService;


        public SettingsApiController(ISettingsService settingsService, ISessionService sessionService) {
            _settingsService = settingsService;
            _sessionService = sessionService;
        }


        private ActionResult SetStringValue(string key, string value) {
            try {
                if (!_sessionService.IsSessionActive()) throw new Exception("Sessão inativa.");
                _settingsService.SetString(key, value);
                return Ok();
            } catch (Exception ex) {
                return BadRequest(new { message = "Erro", error = ex.Message });
            }
        }


        private ActionResult SetBooleanValue(string key, bool value) {
            try {
                if (!_sessionService.IsSessionActive()) throw new Exception("Sessão inativa.");
                _settingsService.SetBoolean(key, value);
                return Ok();
            } catch (Exception ex) {
                return BadRequest(new { message = "Erro", error = ex.Message });
            }
        }


        private ActionResult SetIntValue(string key, int value) {
            try {
                if (!_sessionService.IsSessionActive()) throw new Exception("Sessão inativa.");
                _settingsService.SetInt(key, value);
                return Ok();
            } catch (Exception ex) {
                return BadRequest(new { message = "Erro", error = ex.Message });
            }
        }


        [HttpPost("SetDiscardedTextColor/{color}")]
        public ActionResult SetDiscardedTextColor(string color) {
            return SetStringValue(Constants.DISCARDED_TEXT_COLOR_KEY, color);
        }


        [HttpPost("SetDonatedTextColor/{color}")]
        public ActionResult SetDonatetTextColor(string color) {
            return SetStringValue(Constants.DONATED_TEXT_COLOR_KEY, color);
        }


        [HttpPost("SetBorrowedTextColor/{color}")]
        public ActionResult SetBorrowedTextColor(string color) {
            return SetStringValue(Constants.BORROWED_TEXT_COLOR_KEY, color);
        }


        [HttpPost("SetPageBackgroundColor/{color}")]
        public ActionResult SetPageBackgroundColor(string color) {
            return SetStringValue(Constants.PAGE_BACKGROUND_COLOR_KEY, color);
        }


        [HttpPost("SetDiscardedBold/{isBold}")]
        public ActionResult SetDiscardedBold(bool isBold) {
            return SetBooleanValue(Constants.DISCARDED_BOLD_KEY, isBold);
        }


        [HttpPost("SetDiscardedUnderline/{isUnderline}")]
        public ActionResult SetDiscardedUnderline(bool isUnderline) {
            return SetBooleanValue(Constants.DISCARDED_UNDERLINE_KEY, isUnderline);
        }


        [HttpPost("SetDiscardedItalic/{isItalic}")]
        public ActionResult SetDiscardedItalic(bool isItalic) {
            return SetBooleanValue(Constants.DISCARDED_ITALIC_KEY, isItalic);
        }


        [HttpPost("SetDonatedBold/{isBold}")]
        public ActionResult SetDonatedBold(bool isBold) {
            return SetBooleanValue(Constants.DONATED_BOLD_KEY, isBold);
        }


        [HttpPost("SetDonatedUnderline/{isUnderline}")]
        public ActionResult SetDonatedUnderline(bool isUnderline) {
            return SetBooleanValue(Constants.DONATED_UNDERLINE_KEY, isUnderline);
        }


        [HttpPost("SetDonatedItalic/{isItalic}")]
        public ActionResult SetDonatedItalic(bool isItalic) {
            return SetBooleanValue(Constants.DONATED_ITALIC_KEY, isItalic);
        }


        [HttpPost("SetBorrowedBold/{isBold}")]
        public ActionResult SetBorrowedBold(bool isBold) {
            return SetBooleanValue(Constants.BORROWED_BOLD_KEY, isBold);
        }


        [HttpPost("SetBorrowedUnderline/{isUnderline}")]
        public ActionResult SetBorrowedUnderline(bool isUnderline) {
            return SetBooleanValue(Constants.BORROWED_UNDERLINE_KEY, isUnderline);
        }


        [HttpPost("SetBorrowedItalic/{isItalic}")]
        public ActionResult SetBorrowedItalic(bool isItalic) {
            return SetBooleanValue(Constants.BORROWED_ITALIC_KEY, isItalic);
        }


        [HttpPost("SetReportFormat/{format}")]
        public ActionResult SetReportFormat(int format) {
            return SetIntValue(Constants.REPORT_FORMAT_KEY, format);
        }


        [HttpPost("SetApplyStylesToLists/{isApply}")]
        public ActionResult SetApplyStylesToLists(bool isApply) {
            return SetBooleanValue(Constants.APPLY_STYLES_TO_LISTS_KEY, isApply);
        }


        [HttpPost("SetShowFooterCaption/{isShow}")]
        public ActionResult SetShowFooterCaption(bool isShow) {
            return SetBooleanValue(Constants.SHOW_FOOTER_CAPTION_KEY, isShow);
        }


    }


}

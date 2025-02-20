using Library.Services.SessionService;
using Library.Services.SettingsService;
using Library.Utils;
using Microsoft.AspNetCore.Mvc;

namespace Library.Controllers {


    [ApiController]
    [Route("api/[controller]")]
    public class SettingsApiController : Controller {


        private readonly ISettingsService _settingsService;

        private readonly ISessionService _sessionService;


        public SettingsApiController(ISettingsService settingsService, ISessionService sessionService) {
            _settingsService = settingsService;
            _sessionService = sessionService;
        }


        [HttpPost("SetBorrowedTextColor/{color}")]
        public ActionResult SetBorrowedTextColor(string color) {
            try {
                if (!_sessionService.IsSessionActive()) throw new Exception("Sessão inativa.");
                _settingsService.SetString(Constants.BORROWED_TEXT_COLOR_KEY, color);
                return Ok();
            } catch (Exception ex) {
                return BadRequest(new { message = "Erro", error = ex.Message });
            }  
        }


        [HttpPost("SetDiscardedTextColor/{color}")]
        public ActionResult SetDiscardedTextColor(string color) {
            try {
                if (!_sessionService.IsSessionActive()) throw new Exception("Sessão inativa.");
                _settingsService.SetString(Constants.DISCARDED_TEXT_COLOR_KEY, color);
                return Ok();
            } catch (Exception ex) {
                return BadRequest(new { message = "Erro", error = ex.Message });
            }
        }


        [HttpPost("SetDonatedTextColor/{color}")]
        public ActionResult SetDonatetTextColor(string color) {
            try {
                if (!_sessionService.IsSessionActive()) throw new Exception("Sessão inativa.");
                _settingsService.SetString(Constants.DONATED_TEXT_COLOR_KEY, color);
                return Ok();
            } catch (Exception ex) {
                return BadRequest(new { message = "Erro", error = ex.Message });
            }
        }


        [HttpPost("SetBorrowedFontStyle/{isBold}/{isUnderline}/{isItalic}")]
        public ActionResult SetBorrowedFontStyle(bool isBold, bool isUnderline, bool isItalic) {
            try {
                if (!_sessionService.IsSessionActive()) throw new Exception("Sessão inativa.");
                _settingsService.SetBoolean(Constants.BORROWED_BOLD_KEY, isBold);
                _settingsService.SetBoolean(Constants.BORROWED_UNDERLINE_KEY, isUnderline);
                _settingsService.SetBoolean(Constants.BORROWED_ITALIC_KEY, isItalic);
                return Ok();
            } catch (Exception ex) {
                return BadRequest(new { message = "Erro", error = ex.Message });
            }
        }


        [HttpPost("SetDiscardedFontStyle/{isBold}/{isUnderline}/{isItalic}")]
        public ActionResult SetDiscardedFontStyle(bool isBold, bool isUnderline, bool isItalic) {
            try {
                if (!_sessionService.IsSessionActive()) throw new Exception("Sessão inativa.");
                _settingsService.SetBoolean(Constants.DISCARDED_BOLD_KEY, isBold);
                _settingsService.SetBoolean(Constants.DISCARDED_UNDERLINE_KEY, isUnderline);
                _settingsService.SetBoolean(Constants.DISCARDED_ITALIC_KEY, isItalic);
                return Ok();
            } catch (Exception ex) {
                return BadRequest(new { message = "Erro", error = ex.Message });
            }
        }


        [HttpPost("SetDonatedFontStyle/{isBold}/{isUnderline}/{isItalic}")]
        public ActionResult SetDonatedFontStyle(bool isBold, bool isUnderline, bool isItalic) {
            try {
                if (!_sessionService.IsSessionActive()) throw new Exception("Sessão inativa.");
                _settingsService.SetBoolean(Constants.DONATED_BOLD_KEY, isBold);
                _settingsService.SetBoolean(Constants.DONATED_UNDERLINE_KEY, isUnderline);
                _settingsService.SetBoolean(Constants.DONATED_ITALIC_KEY, isItalic);
                return Ok();
            } catch (Exception ex) {
                return BadRequest(new { message = "Erro", error = ex.Message });
            }
        }


        [HttpPost("SetReportFormat/{format}")]
        public ActionResult SetReportFormat(int format) {
            try {
                if (!_sessionService.IsSessionActive()) throw new Exception("Sessão inativa.");
                _settingsService.SetInt(Constants.REPORT_FORMAT_KEY, format);
                return Ok();
            } catch (Exception ex) {
                return BadRequest(new { message = "Erro", error = ex.Message });
            }
        }


        [HttpPost("SetApplyStylesToLists/{isApply}")]
        public ActionResult SetApplyStylesToLists(bool isApply) {
            try {
                if (!_sessionService.IsSessionActive()) throw new Exception("Sessão inativa.");
                _settingsService.SetBoolean(Constants.APPLY_STYLES_TO_LISTS_KEY, isApply);
                return Ok();
            } catch (Exception ex) {
                return BadRequest(new { message = "Erro", error = ex.Message });
            }
        }


        [HttpPost("SetShowFooterCaption/{isShow}")]
        public ActionResult SetShowFooterCaption(bool isShow) {
            try {
                if (!_sessionService.IsSessionActive()) throw new Exception("Sessão inativa.");
                _settingsService.SetBoolean(Constants.SHOW_FOOTER_CAPTION, isShow);
                return Ok();
            } catch (Exception ex) {
                return BadRequest(new { message = "Erro", error = ex.Message });
            }
        }


    }


}

using Library.Services.Session;
using Library.Services.User;
using Library.Utils;
using Microsoft.AspNetCore.Mvc;

namespace Library.Controllers {


    /// <summary>
    /// API para gerenciamento das configurações de usuário.
    /// </summary>
    [ApiController]
    [Route("Api/[controller]")]
    public class SettingsApiController(ISettingsService settingsService, ISessionService sessionService) : Controller {


        /// <summary> Objeto para acesso às configurações do usuário. </summary>
        private readonly ISettingsService _settingsService = settingsService;

        /// <summary> Objeto para gerenciamento de sessão do usuário. </summary>
        private readonly ISessionService _sessionService = sessionService;


        /// <summary>
        /// Definir o valor String de uma configuração.
        /// </summary>
        /// <param name="key">Chave da configuração.</param>
        /// <param name="value">Valor da configuração.</param>
        /// <returns></returns>
        private ActionResult SetStringValue(string key, string value) {
            try {
                if (!_sessionService.IsSessionActive()) throw new Exception("Sessão inativa.");
                _settingsService.SetString(key, value);
                return Ok();
            } catch (Exception ex) {
                return BadRequest(new { message = "Erro", error = ex.Message });
            }
        }


        /// <summary>
        /// Definir o valor Boolean de uma configuração.
        /// </summary>
        /// <param name="key">Chave da configuração.</param>
        /// <param name="value">Valor da configuração.</param>
        /// <returns></returns>
        private ActionResult SetBooleanValue(string key, bool value) {
            try {
                if (!_sessionService.IsSessionActive()) throw new Exception("Sessão inativa.");
                _settingsService.SetBoolean(key, value);
                return Ok();
            } catch (Exception ex) {
                return BadRequest(new { message = "Erro", error = ex.Message });
            }
        }


        /// <summary>
        /// Definir o valor Int de uma configuração.
        /// </summary>
        /// <param name="key">Chave da configuração.</param>
        /// <param name="value">Valor da configuração.</param>
        /// <returns></returns>
        private ActionResult SetIntValue(string key, int value) {
            try {
                if (!_sessionService.IsSessionActive()) throw new Exception("Sessão inativa.");
                _settingsService.SetInt(key, value);
                return Ok();
            } catch (Exception ex) {
                return BadRequest(new { message = "Erro", error = ex.Message });
            }
        }


        /// <summary>
        /// Definir a cor de texto de um livro descartado.
        /// </summary>
        /// <param name="color">Cor de texto de um livro descartado.</param>
        /// <returns></returns>
        [HttpPost("SetDiscardedTextColor/{color}")]
        public ActionResult SetDiscardedTextColor(string color) {
            return SetStringValue(Constants.DISCARDED_TEXT_COLOR_KEY, color);
        }


        /// <summary>
        /// Definir a cor de texto de um livro doado.
        /// </summary>
        /// <param name="color">Cor de texto de um livro doado.</param>
        /// <returns></returns>
        [HttpPost("SetDonatedTextColor/{color}")]
        public ActionResult SetDonatetTextColor(string color) {
            return SetStringValue(Constants.DONATED_TEXT_COLOR_KEY, color);
        }


        /// <summary>
        /// Definir a cor de texto de um livro emprestado.
        /// </summary>
        /// <param name="color">Cor de texto de um livro emprestado.</param>
        /// <returns></returns>
        [HttpPost("SetBorrowedTextColor/{color}")]
        public ActionResult SetBorrowedTextColor(string color) {
            return SetStringValue(Constants.BORROWED_TEXT_COLOR_KEY, color);
        }


        /// <summary>
        /// Definir a cor de fundo da página.
        /// </summary>
        /// <param name="color">Cor de fundo da página.</param>
        /// <returns></returns>
        [HttpPost("SetPageBackgroundColor/{color}")]
        public ActionResult SetPageBackgroundColor(string color) {
            return SetStringValue(Constants.PAGE_BACKGROUND_COLOR_KEY, color);
        }


        /// <summary>
        /// Definir o efeito de negrito para livros descartados.
        /// </summary>
        /// <param name="isBold">Aplicar o efeito de negrito para livros descartados.</param>
        /// <returns></returns>
        [HttpPost("SetDiscardedBold/{isBold}")]
        public ActionResult SetDiscardedBold(bool isBold) {
            return SetBooleanValue(Constants.DISCARDED_BOLD_KEY, isBold);
        }


        /// <summary>
        /// Definir o efeito de sublinhado para livros descartados.
        /// </summary>
        /// <param name="isBold">Aplicar o efeito de sublinhado para livros descartados.</param>
        /// <returns></returns>
        [HttpPost("SetDiscardedUnderline/{isUnderline}")]
        public ActionResult SetDiscardedUnderline(bool isUnderline) {
            return SetBooleanValue(Constants.DISCARDED_UNDERLINE_KEY, isUnderline);
        }


        /// <summary>
        /// Definir o efeito de itálico para livros descartados.
        /// </summary>
        /// <param name="isBold">Aplicar o efeito de itálico para livros descartados.</param>
        /// <returns></returns>
        [HttpPost("SetDiscardedItalic/{isItalic}")]
        public ActionResult SetDiscardedItalic(bool isItalic) {
            return SetBooleanValue(Constants.DISCARDED_ITALIC_KEY, isItalic);
        }


        /// <summary>
        /// Definir o efeito de negrito para livros doados.
        /// </summary>
        /// <param name="isBold">Aplicar o efeito de negrito para livros doados.</param>
        /// <returns></returns>
        [HttpPost("SetDonatedBold/{isBold}")]
        public ActionResult SetDonatedBold(bool isBold) {
            return SetBooleanValue(Constants.DONATED_BOLD_KEY, isBold);
        }


        /// <summary>
        /// Definir o efeito de sublinhado para livros doados.
        /// </summary>
        /// <param name="isBold">Aplicar o efeito de sublinhado para livros doados.</param>
        /// <returns></returns>
        [HttpPost("SetDonatedUnderline/{isUnderline}")]
        public ActionResult SetDonatedUnderline(bool isUnderline) {
            return SetBooleanValue(Constants.DONATED_UNDERLINE_KEY, isUnderline);
        }


        /// <summary>
        /// Definir o efeito de itálico para livros doados.
        /// </summary>
        /// <param name="isBold">Aplicar o efeito de itálico para livros doados.</param>
        /// <returns></returns>
        [HttpPost("SetDonatedItalic/{isItalic}")]
        public ActionResult SetDonatedItalic(bool isItalic) {
            return SetBooleanValue(Constants.DONATED_ITALIC_KEY, isItalic);
        }


        /// <summary>
        /// Definir o efeito de negrito para livros emprestados.
        /// </summary>
        /// <param name="isBold">Aplicar o efeito de negrito para livros emprestados.</param>
        /// <returns></returns>
        [HttpPost("SetBorrowedBold/{isBold}")]
        public ActionResult SetBorrowedBold(bool isBold) {
            return SetBooleanValue(Constants.BORROWED_BOLD_KEY, isBold);
        }


        /// <summary>
        /// Definir o efeito de sublinhado para livros emprestados.
        /// </summary>
        /// <param name="isBold">Aplicar o efeito de sublinhado para livros emprestados.</param>
        /// <returns></returns>
        [HttpPost("SetBorrowedUnderline/{isUnderline}")]
        public ActionResult SetBorrowedUnderline(bool isUnderline) {
            return SetBooleanValue(Constants.BORROWED_UNDERLINE_KEY, isUnderline);
        }


        /// <summary>
        /// Definir o efeito de itálico para livros emprestados.
        /// </summary>
        /// <param name="isBold">Aplicar o efeito de itálico para livros emprestados.</param>
        /// <returns></returns>
        [HttpPost("SetBorrowedItalic/{isItalic}")]
        public ActionResult SetBorrowedItalic(bool isItalic) {
            return SetBooleanValue(Constants.BORROWED_ITALIC_KEY, isItalic);
        }


        /// <summary>
        /// Definir o formato do relatório.
        /// </summary>
        /// <param name="format">formato do relatório.</param>
        /// <returns></returns>
        [HttpPost("SetReportFormat/{format}")]
        public ActionResult SetReportFormat(int format) {
            return SetIntValue(Constants.REPORT_FORMAT_KEY, format);
        }


        /// <summary>
        /// Aplicar os estilos de texto e de fonte às listas.
        /// </summary>
        /// <param name="isApply">Aplicar os efeitos às listas.</param>
        /// <returns></returns>
        [HttpPost("SetApplyStylesToLists/{isApply}")]
        public ActionResult SetApplyStylesToLists(bool isApply) {
            return SetBooleanValue(Constants.APPLY_STYLES_TO_LISTS_KEY, isApply);
        }


        /// <summary>
        /// Mostrar a legenda de cores no rodapé das páginas e diálogos.
        /// </summary>
        /// <param name="isShow">Mostrar a legenda de cores.</param>
        /// <returns></returns>
        [HttpPost("SetShowFooterCaption/{isShow}")]
        public ActionResult SetShowFooterCaption(bool isShow) {
            return SetBooleanValue(Constants.SHOW_FOOTER_CAPTION_KEY, isShow);
        }


    }


}

using Library.Services.Session;
using Library.Services.User;
using Microsoft.AspNetCore.Mvc;
using System.Globalization;

namespace Library.Controllers {


    public class SessionController : Controller {


        private readonly ISessionService _sessionService;

        private readonly ILogService _logService;


        public SessionController(ISessionService sessionService, ILogService logService) {
            _sessionService = sessionService;
            _logService = logService;
        }


        [HttpGet]
        public async Task<IActionResult> Manage(DateTime beginDate, DateTime endDate) {

            if (!_sessionService.IsSessionActive()) {
                return RedirectToAction("Login", "Login");
            }

            if (!_sessionService.IsAdminSession()) {
                return RedirectToAction("Index", "Home");
            }

            if (beginDate == DateTime.MinValue) {

                DateTime date;
                
                if (DateTime.TryParseExact("01/01/1900", "dd/MM/yyyy", CultureInfo.InvariantCulture, 
                DateTimeStyles.None, out date)) {
                    beginDate = date;
                }

            }

            if (endDate == DateTime.MinValue) {

                DateTime date;

                if (DateTime.TryParseExact("31/12/9999", "dd/MM/yyyy", CultureInfo.InvariantCulture,
                DateTimeStyles.None, out date)) {
                    beginDate = date;
                }

            }

            ViewData["BeginDate"] = beginDate.ToString("yyyy-MM-dd");
            ViewData["EndDate"] = endDate.ToString("yyyy-MM-dd");

            DateTime adjustedBeginDate = new DateTime(beginDate.Year, beginDate.Month, beginDate.Day, 0, 0, 0);
            DateTime adjustedEndDate = new DateTime(endDate.Year, endDate.Month, endDate.Day, 23, 59, 59);
            
            var sessionLogResp = await _logService.GetSessionLog(adjustedBeginDate, adjustedEndDate);

            if (sessionLogResp.Successful) {

                return View(sessionLogResp.Data);

            } else {

                return BadRequest(sessionLogResp.Message);

            }

        }


    }


}

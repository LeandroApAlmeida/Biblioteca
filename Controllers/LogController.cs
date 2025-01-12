﻿using Library.Services.LogService;
using Library.Services.SessionService;
using Library.Services.UserService;
using Microsoft.AspNetCore.Mvc;

namespace Library.Controllers {


    public class LogController : Controller {


        private readonly ISessionService _sessionService;

        private readonly ILogService _logService;


        public LogController(ISessionService sessionService, ILogService logService) {
            _sessionService = sessionService;
            _logService = logService;
        }


        [HttpGet]
        public async Task<IActionResult> Log(DateTime beginData, DateTime endDate) {

            if (!_sessionService.IsTheSessionActive()) {
                return RedirectToAction("Login", "Login");
            }

            if (_sessionService.GetSessionData()!.User.Role.Id != (int)UserRole.Admin) {
                return RedirectToAction("Details", "Book");
            }

            _sessionService.SetLayout(this);

            DateTime date1 = DateTime.MinValue;

            DateTime date2 = DateTime.MaxValue;

            var sessionLogResp = await _logService.GetSessionLog(date1, date2);

            if (sessionLogResp.Successful) {

                return View(sessionLogResp.Data);

            } else {

                return BadRequest(sessionLogResp.Message);

            }

        }


    }


}

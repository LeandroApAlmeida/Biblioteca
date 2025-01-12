using Library.Dto;
using Library.Models;
using Library.Services.LoginService;
using Library.Services.SessionService;
using Library.Services.UserService;
using Library.Utils;
using Microsoft.AspNetCore.Mvc;

namespace Library.Controllers {


    public class LoginController : Controller {

        
        private readonly ILoginService _loginService;

        private readonly ISessionService _sessionService;

        private readonly IUserService _userService;


        public LoginController(ILoginService loginService, ISessionService sessionService,
        IUserService userService) {
            _loginService = loginService;
            _sessionService = sessionService;
            _userService = userService;
        }


        [HttpGet]
        public async Task<IActionResult> Login() {

            if (_sessionService.IsTheSessionActive()) {
                return RedirectToAction("Details", "Book");
            }

            var registeredAdminResp = await _userService.RegisteredAdmin();

            if (registeredAdminResp.Successful) {
                ViewBag.RegisteredAdmin = registeredAdminResp.Data;
            } else {
                ViewBag.RegisteredAdmin = true;
                TempData[Constants.ERROR_MESSAGE] = registeredAdminResp.Message;
            }

            return View();
        }


        [HttpPost]
        public async Task<IActionResult> Login(LoginDto login) {

            if (_sessionService.IsTheSessionActive()) {
                return RedirectToAction("Details", "Book");
            }

            if (ModelState.IsValid) {

                var loginResp = await _loginService.Login(login);

                if (loginResp.Successful) {

                    if (loginResp.Data != null) {

                        return RedirectToAction("Details", "Book");

                    } else {

                        TempData[Constants.ERROR_MESSAGE] = loginResp.Message;

                        return RedirectToAction("Login");

                    }

                } else {

                    TempData[Constants.ERROR_MESSAGE] = loginResp.Message;

                    return RedirectToAction("Login");

                }

            } else {

                TempData[Constants.ERROR_MESSAGE] = "Credenciais inválidas!";

                return RedirectToAction("Login");

            }

        }


        [HttpGet]
        public async Task<IActionResult> Logout() {

            var removeSessionResp = await _sessionService.RemoveSession();

            if (removeSessionResp.Successful) {

                return RedirectToAction("Login");

            } else {

                TempData[Constants.ERROR_MESSAGE] += removeSessionResp.Message;

                return RedirectToAction("Details", "Book");

            }

        }


    }


}
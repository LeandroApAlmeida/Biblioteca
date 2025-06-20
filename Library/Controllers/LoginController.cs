using Library.Services.Model.Dto;
using Library.Services.Authentication;
using Library.Services.Session;
using Library.Services.User;
using Library.Utils;
using Microsoft.AspNetCore.Mvc;
using System.Net;

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

            if (_sessionService.IsSessionActive()) {
                return RedirectToAction("Index", "Home");
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
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> Login(LoginDto login) {

            if (ModelState.IsValid) {

                string host = Dns.GetHostName();

                string ip;

                try {
                    ip = "[local]"; // Dns.GetHostAddresses(host)[2].ToString();
                } catch {
                    ip = "[local]";
                }
                 
                var loginResp = await _loginService.Login(login, ip);

                if (loginResp.Successful) {

                    if (loginResp.Data != null) {

                        return RedirectToAction("Index", "Home");

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


        [HttpPost]
        public async Task<IActionResult> Logout() {

            if (!_sessionService.IsSessionActive()) {
                return RedirectToAction("Login", "Login");
            }

            var logoutResp = await _loginService.Logout();

            if (logoutResp.Successful) {

                return RedirectToAction("Login");

            } else {

                TempData[Constants.ERROR_MESSAGE] += logoutResp.Message;

                return RedirectToAction("Index", "Home");

            }

        }


    }


}
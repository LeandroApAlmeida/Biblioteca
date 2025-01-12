using Library.Dto;
using Library.Services.SessionService;
using Library.Services.UserService;
using Library.Utils;
using Microsoft.AspNetCore.Mvc;

namespace Library.Controllers {


    public class UserController : Controller {


        private readonly IUserService _userService;

        private readonly ISessionService _sessionService;


        public UserController(IUserService userService, ISessionService sessionService) {
            _userService = userService;
            _sessionService = sessionService;
        }


        [HttpGet]
        public async Task<IActionResult> Manage() {

            if (!_sessionService.IsTheSessionActive()) {
                return RedirectToAction("Login", "Login");
            }

            if (_sessionService.GetSessionData()!.User.Role.Id != (int)UserRole.Admin) {
                return RedirectToAction("Details", "Book");
            }

            _sessionService.SetLayout(this);

            var usersResp = await _userService.GetUsers();

            if (usersResp.Successful) {

                return View(usersResp.Data);

            } else {

                return BadRequest(usersResp.Message);

            }

        }


        [HttpGet]
        public IActionResult RegisterAdm() {


            if (_sessionService.IsTheSessionActive()) {
                return RedirectToAction("Details", "Book");
            }

            ViewBag.UserRole = (int)UserRole.Admin;
            
            return View();
        
        }


        [HttpPost]
        public async Task<IActionResult> RegisterAdm(UserDto admin) {

            if (ModelState.IsValid) {

                var registerUserResp = await _userService.RegisterUser(admin);

                if (registerUserResp.Successful) {

                    TempData[Constants.SUCCESS_MESSAGE] = registerUserResp.Message;

                    return RedirectToAction("Login", "Login");

                } else {

                    TempData[Constants.ERROR_MESSAGE] = registerUserResp.Message;

                    return View(admin);

                }

            } else {

                TempData[Constants.ERROR_MESSAGE] = "Dados do usuário incorretos!";

                return View(admin);

            }
        
        }


        [HttpGet]
        public async Task<IActionResult> Register() {

            if (!_sessionService.IsTheSessionActive()) {
                return RedirectToAction("Login", "Login");
            }

            if (_sessionService.GetSessionData()!.User.Role.Id != (int)UserRole.Admin) {
                return RedirectToAction("Details", "Book");
            }

            _sessionService.SetLayout(this);

            ViewBag.UserRole = (int) UserRole.Guest;

            return View();

        }


        [HttpPost]
        public async Task<IActionResult> Register(UserDto user) {

            if (!_sessionService.IsTheSessionActive()) {
                return RedirectToAction("Login", "Login");
            }

            if (_sessionService.GetSessionData()!.User.Role.Id != (int)UserRole.Admin) {
                return RedirectToAction("Details", "Book");
            }

            if (ModelState.IsValid) {

                var registerUserResp = await _userService.RegisterUser(user);

                if (registerUserResp.Successful) {

                    TempData[Constants.SUCCESS_MESSAGE] = registerUserResp.Message;

                    return RedirectToAction("Manage", "User");

                } else {

                    TempData[Constants.ERROR_MESSAGE] = registerUserResp.Message;

                    return View(user);

                }

            } else {

                TempData[Constants.ERROR_MESSAGE] = "Dados do usuário incorretos!";

                return View(user);

            }

        }


    }


}

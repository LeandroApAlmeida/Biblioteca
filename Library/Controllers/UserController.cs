using Library.Services.Model.Dto;
using Library.Services.Authentication;
using Library.Services.Session;
using Library.Services.User;
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

            if (!_sessionService.IsSessionActive()) {
                return RedirectToAction("Login", "Login");
            }

            if (!_sessionService.IsAdminSession()) {
                return RedirectToAction("Details", "Book");
            }

            var usersResp = await _userService.GetUsers();

            if (usersResp.Successful) {

                return View(usersResp.Data);

            } else {

                return BadRequest(usersResp.Message);

            }

        }


        [HttpGet]
        public IActionResult RegisterAdm() {


            if (_sessionService.IsSessionActive()) {
                return RedirectToAction("Details", "Book");
            }

            ViewBag.UserRole = (int)UserRole.Admin;
            
            return View();
        
        }


        [HttpPost]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> RegisterAdm(UserDto admin) {

            if (_sessionService.IsSessionActive()) {
                return RedirectToAction("Details", "Book");
            }

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
        public IActionResult Register() {

            if (!_sessionService.IsSessionActive()) {
                return RedirectToAction("Login", "Login");
            }

            if (!_sessionService.IsAdminSession()) {
                return RedirectToAction("Details", "Book");
            }

            ViewBag.UserRole = (int) UserRole.Guest;

            return View();

        }


        [HttpPost]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> Register(UserDto user) {

            if (!_sessionService.IsSessionActive()) {
                return RedirectToAction("Login", "Login");
            }

            if (!_sessionService.IsAdminSession()) {
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


        [HttpGet]
        public async Task<IActionResult> Edit(Guid id) {

            if (!_sessionService.IsSessionActive()) {
                return RedirectToAction("Login", "Login");
            }

            if (!_sessionService.IsAdminSession()) {
                return RedirectToAction("Details", "Book");
            }

            ViewBag.UserRole = (int)UserRole.Guest;

            var userResp = await _userService.GetUser(id);

            if (userResp.Successful) {

                if (userResp.Data == null) {

                    TempData[Constants.ERROR_MESSAGE] = userResp.Message;

                    return RedirectToAction("Manage");

                }
                
                UserDto user = new UserDto() {
                    Id = userResp.Data.Id,
                    Role = userResp.Data.Role.Id,
                    Name = userResp.Data.Name,
                    UserName = userResp.Data.UserName,
                    Password = "",
                    ConfPassword = ""
                };

                return View(user);
            
            } else {
            
                TempData[Constants.ERROR_MESSAGE] = userResp.Message;
                
                return RedirectToAction("Manage");
            
            }

        }


        [HttpPost]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> Edit(UserDto user) {

            if (!_sessionService.IsSessionActive()) {
                return RedirectToAction("Login", "Login");
            }

            if (ModelState.IsValid) {

                var editUserResp = await _userService.EditUser(user);

                if (editUserResp.Successful) {

                    TempData[Constants.SUCCESS_MESSAGE] = editUserResp.Message;

                    return RedirectToAction("Manage");

                } else {

                    TempData[Constants.ERROR_MESSAGE] = editUserResp.Message;

                    return View(user);

                }

            } else {

                TempData[Constants.ERROR_MESSAGE] = "Dados do usuário incorretos!";

                return View(user);

            }

        }


        [HttpPost]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> Delete(Guid id) {

            if (!_sessionService.IsSessionActive()) {
                return RedirectToAction("Login", "Login");
            }

            var deleteUserResp = await _userService.DeleteUser(id);

            if (deleteUserResp.Successful) {

                TempData[Constants.SUCCESS_MESSAGE] = deleteUserResp.Message;

                return RedirectToAction("Manage");

            } else {

                TempData[Constants.ERROR_MESSAGE] = deleteUserResp.Message;

                return RedirectToAction("Manage");

            }

        }


        [HttpPost]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> Undelete(Guid id) {

            if (!_sessionService.IsSessionActive()) {
                return RedirectToAction("Login", "Login");
            }

            var undeleteUserResp = await _userService.UndeleteUser(id);

            if (undeleteUserResp.Successful) {

                TempData[Constants.SUCCESS_MESSAGE] = undeleteUserResp.Message;

                return RedirectToAction("Manage");

            } else {

                TempData[Constants.ERROR_MESSAGE] = undeleteUserResp.Message;

                return RedirectToAction("Manage");

            }

        }


    }


}

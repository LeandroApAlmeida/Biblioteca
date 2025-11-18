using Library.Services.Model.Dto;
using Library.Services.Session;
using Library.Services.User;
using Library.Utils;
using Microsoft.AspNetCore.Mvc;

namespace Library.Controllers {


    /// <summary>
    /// Controlador para a manutenção de usuários.
    /// </summary>
    public class UserController(IUserService userService, ISessionService sessionService) : Controller {


        /// <summary> Objeto para a manutenção de usuários. </summary>
        private readonly IUserService _userService = userService;

        /// <summary> Objeto para gerenciamento de sessão do usuário. </summary>
        private readonly ISessionService _sessionService = sessionService;


        /// <summary>
        /// Retornar a página de manutenção de usuários.
        /// </summary>
        /// <returns>Página de manutenção de usuários.</returns>
        [HttpGet]
        public async Task<IActionResult> Manage() {

            if (!_sessionService.IsSessionActive()) {
                return RedirectToAction("Login", "Login");
            }

            if (!_sessionService.IsAdminSession()) {
                return RedirectToAction("Index", "Home");
            }

            var usersResp = await _userService.GetUsers();

            if (usersResp.Successful) {

                return View(usersResp.Data);

            } else {

                return BadRequest(usersResp.Message);

            }

        }


        /// <summary>
        /// Retornar a página para cadastro do administrador.
        /// </summary>
        /// <returns>Página para cadastro do administrador.</returns>
        [HttpGet]
        public IActionResult RegisterAdm() {


            if (_sessionService.IsSessionActive()) {
                return RedirectToAction("Index", "Home");
            }

            ViewBag.UserRole = (int)UserRole.Admin;
            
            return View();
        
        }


        /// <summary>
        /// Cadastrar o usuário administrador.
        /// </summary>
        /// <param name="admin">Usuáro administrador.</param>
        /// <returns>Página de redirecionamento.</returns>
        [HttpPost]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> RegisterAdm(UserDto admin) {

            if (_sessionService.IsSessionActive()) {
                return RedirectToAction("Index", "Home");
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


        /// <summary>
        /// Retornar a página para cadastro de um novo usuário.
        /// </summary>
        /// <returns>Página para cadastro de um novo usuário.</returns>
        [HttpGet]
        public IActionResult Register() {

            if (!_sessionService.IsSessionActive()) {
                return RedirectToAction("Login", "Login");
            }

            if (!_sessionService.IsAdminSession()) {
                return RedirectToAction("Index", "Home");
            }

            ViewBag.UserRole = (int) UserRole.Guest;

            return View();

        }


        /// <summary>
        /// Cadastrar um novo usuário.
        /// </summary>
        /// <param name="user">Novo usuário.</param>
        /// <returns>Página de redirecionamento.</returns>
        [HttpPost]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> Register(UserDto user) {

            if (!_sessionService.IsSessionActive()) {
                return RedirectToAction("Login", "Login");
            }

            if (!_sessionService.IsAdminSession()) {
                return RedirectToAction("Index", "Home");
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


        /// <summary>
        /// Retornar a página para edição de um usuário.
        /// </summary>
        /// <param name="id">Identificador chave primária do usuário.</param>
        /// <returns>Página para edição de um usuário.</returns>
        [HttpGet]
        public async Task<IActionResult> Edit(Guid id) {

            if (!_sessionService.IsSessionActive()) {
                return RedirectToAction("Login", "Login");
            }

            if (!_sessionService.IsAdminSession()) {
                return RedirectToAction("Index", "Home");
            }

            ViewBag.UserRole = (int)UserRole.Guest;

            var userResp = await _userService.GetUserWithoutHash(id);

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


        /// <summary>
        /// Alterar o cadastro de um usuário.
        /// </summary>
        /// <param name="user">Usuário a ser alterado.</param>
        /// <returns>Página de redirecionamento.</returns>
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


        /// <summary>
        /// Excluir o cadastro de um usuário (afastar o usuário).
        /// </summary>
        /// <param name="id">Identificador chave primária do usuário.</param>
        /// <returns>Página de redirecionamento.</returns>
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


        /// <summary>
        /// Restaurar o cadastro de um usuário (retornar o usuário).
        /// </summary>
        /// <param name="id">Identificador chave primária do usuário.</param>
        /// <returns>Página de redirecionamento.</returns>
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

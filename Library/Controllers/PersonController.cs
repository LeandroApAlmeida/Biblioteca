using Library.Db.Models;
using Library.Services.Collection;
using Library.Services.Session;
using Library.Services.User;
using Library.Utils;
using Microsoft.AspNetCore.Mvc;
using System;

namespace Library.Controllers {


    /// <summary>
    /// Controler para manutenção de pessoas.
    /// </summary>
    public class PersonController(IPersonService personService, ISessionService sessionService) : Controller {


        /// <summary> Objeto para manutenção de pessoas. </summary>
        private readonly IPersonService _personService = personService;

        /// <summary> Objeto para gerenciamento de sessão do usuário. </summary>
        private readonly ISessionService _sessionService = sessionService;


        /// <summary>
        /// Retornar a página para manutenção de pessoas.
        /// </summary>
        /// <returns>Página para manutenção de pessoas.</returns>
        [HttpGet]
        public async Task<IActionResult> Manage() {

            if (!_sessionService.IsSessionActive()) {
                return RedirectToAction("Login", "Login");
            }

            var personsResp = await _personService.GetPersons();

            var deletedPersonsResp = await _personService.GetDeletedPersons();

            if (personsResp.Successful) {

                ViewBag.DeletedPersons = deletedPersonsResp.Data != null ? deletedPersonsResp.Data : new List<PersonModel>();

                return View(personsResp.Data);

            } else {

                return BadRequest(personsResp.Message);

            }

        }


        /// <summary>
        /// Retornar a página para cadastro de uma nova pessoa.
        /// </summary>
        /// <returns>Página para cadastro de uma nova pessoa.</returns>
        [HttpGet]
        public IActionResult Register() {

            if (!_sessionService.IsSessionActive()) {
                return RedirectToAction("Login", "Login");
            }

            return View();

        }


        /// <summary>
        /// Cadastrar uma nova pessoa.
        /// </summary>
        /// <param name="book">Pessoa a ser cadastrada.</param>
        /// <returns>Página de redirecionamento.</returns>
        [HttpPost]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> Register(PersonModel person) {

            if (!_sessionService.IsSessionActive()) {
                return RedirectToAction("Login", "Login");
            }

            if (ModelState.IsValid) {

                var registerPersonResp = await _personService.RegisterPerson(person);

                if (registerPersonResp.Successful) {

                    TempData[Constants.SUCCESS_MESSAGE] = registerPersonResp.Message;

                    return RedirectToAction("Manage");

                } else {

                    TempData[Constants.ERROR_MESSAGE] = registerPersonResp.Message;

                    return View(person);

                }

            } else {

                TempData[Constants.ERROR_MESSAGE] = "Dados da pessoa incorretos!";

                return View(person);

            }

        }


        /// <summary>
        /// Retornar a página para edição de uma pessoa.
        /// </summary>
        /// <param name="id">Identificador chave primária da pessoa.</param>
        /// <returns>Página para edição de uma pessoa.</returns>
        [HttpGet]
        public async Task<IActionResult> Edit(Guid id) {

            if (!_sessionService.IsSessionActive()) {
                return RedirectToAction("Login", "Login");
            }

            var personResp = await _personService.GetPerson(id);

            if (personResp.Successful) {

                if (personResp.Data != null) {
                    return View(personResp.Data);
                } else {
                    return NotFound();
                }

            } else {

                return BadRequest(personResp.Message);

            }

        }


        /// <summary>
        /// Alterar o cadastro de uma pessoa.
        /// </summary>
        /// <param name="book">Pessoa a ser alterada.</param>
        /// <returns>Página de redirecionamento.</returns>
        [HttpPost]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> Edit(PersonModel person) {

            if (!_sessionService.IsSessionActive()) {
                return RedirectToAction("Login", "Login");
            }

            if (ModelState.IsValid) {

                var editPersonResp = await _personService.EditPerson(person);

                if (editPersonResp.Successful) {

                    TempData[Constants.SUCCESS_MESSAGE] = editPersonResp.Message;

                    return RedirectToAction("Manage");

                } else {

                    TempData[Constants.ERROR_MESSAGE] = editPersonResp.Message;

                    return View(person);

                }

            } else {

                TempData[Constants.ERROR_MESSAGE] = "Dados da pessoa incorretos!";

                return View(person);

            }

        }


        /// <summary>
        /// Excluir o cadastro de uma pessoa.
        /// </summary>
        /// <param name="book">Identificador chave primária da pessoa.</param>
        /// <returns>Página de redirecionamento.</returns>
        [HttpPost]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> Delete(Guid id) {

            if (!_sessionService.IsSessionActive()) {
                return RedirectToAction("Login", "Login");
            }

            var deletePersonResp = await _personService.DeletePerson(id);

            if (deletePersonResp.Successful) {

                TempData[Constants.SUCCESS_MESSAGE] = deletePersonResp.Message;

                return RedirectToAction("Manage");

            } else {

                TempData[Constants.ERROR_MESSAGE] = deletePersonResp.Message;

                return RedirectToAction("Manage");

            }

        }


        /// <summary>
        /// Retornar o cadastro de uma pessoa.
        /// </summary>
        /// <param name="id">Identificador chave primária da pessoa.</param>
        /// <returns>Página de redirecionamento.</returns>
        [HttpPost]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> Undelete(Guid id) {

            if (!_sessionService.IsSessionActive()) {
                return RedirectToAction("Login", "Login");
            }

            var deletedPersonResp = await _personService.UndeletePerson(id);

            if (deletedPersonResp.Successful) {

                TempData[Constants.SUCCESS_MESSAGE] = deletedPersonResp.Message;

                return RedirectToAction("Manage");

            } else {

                TempData[Constants.ERROR_MESSAGE] = deletedPersonResp.Message;

                return RedirectToAction("Manage");

            }

        }


    }


}

﻿using Library.Models;
using Library.Services.PersonService;
using Library.Services.SessionService;
using Library.Services.UserService;
using Library.Utils;
using Microsoft.AspNetCore.Mvc;

namespace Library.Controllers {


    /// <summary>
    /// Controler para manutenção de pessoas.
    /// </summary>
    public class PersonController : Controller {


        private readonly IPersonService _personService;

        private readonly ISessionService _sessionService;


        public PersonController(IPersonService personService, ISessionService sessionService) {
            _personService = personService;
            _sessionService = sessionService;
        }


        /// <summary>
        /// Retornar a página de manutenção de pessoas.
        /// </summary>
        /// <returns>Página de manutenção de pessoas.</returns>
        [HttpGet]
        public async Task<IActionResult> Manage() {

            if (!_sessionService.IsSessionActive()) {
                return RedirectToAction("Login", "Login");
            }

            _sessionService.SetLayout(this);

            var personsResp = await _personService.GetPersons();

            if (personsResp.Successful) {

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

            _sessionService.SetLayout(this);

            return View();

        }


        /// <summary>
        /// Cadastrar uma nova pessoa.
        /// </summary>
        /// <param name="book">Nova pessoa</param>
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
        /// <param name="id">Identificador da pessoa</param>
        /// <returns>Página para edição de uma pessoa.</returns>
        [HttpGet]
        public async Task<IActionResult> Edit(Guid id) {

            if (!_sessionService.IsSessionActive()) {
                return RedirectToAction("Login", "Login");
            }

            _sessionService.SetLayout(this);

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
        /// <param name="book">Pessoa a ser alterada</param>
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
        /// Retornar a página para a exclusão de uma pessoa.
        /// </summary>
        /// <param name="id">Identificador da pessoa</param>
        /// <returns>Página para a exclusão de uma pessoa.</returns>
        [HttpGet]
        public async Task<IActionResult> Delete(Guid id) {

            if (!_sessionService.IsSessionActive()) {
                return RedirectToAction("Login", "Login");
            }

            _sessionService.SetLayout(this);

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
        /// Excluir o cadastro de uma pessoa.
        /// </summary>
        /// <param name="book">Pessoa a ser excluída</param>
        /// <returns>Página de redirecionamento.</returns>
        [HttpPost]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> Delete(PersonModel person) {

            if (!_sessionService.IsSessionActive()) {
                return RedirectToAction("Login", "Login");
            }

            if (ModelState.IsValid) {

                var deletePersonResp = await _personService.DeletePerson(person);

                if (deletePersonResp.Successful) {

                    TempData[Constants.SUCCESS_MESSAGE] = deletePersonResp.Message;

                    return RedirectToAction("Manage");

                } else {

                    TempData[Constants.ERROR_MESSAGE] = deletePersonResp.Message;

                    return View(person);

                }

            } else {

                TempData[Constants.SUCCESS_MESSAGE] = "Dados da pessoa incorretos!";

                return View(person);

            }

        }


    }


}

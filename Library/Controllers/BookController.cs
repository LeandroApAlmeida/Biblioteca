using Library.Services.Model.Dto;
using Library.Services.Collection;
using Library.Services.Session;
using Library.Services.User;
using Library.Utils;
using Microsoft.AspNetCore.Mvc;
using Library.Db.Models;

namespace Library.Controllers {


    /// <summary>
    /// Controller para a manutenção de livros do acervo.
    /// </summary>
    public class BookController : Controller {


        /// <summary> Objeto para manutenção de livros. </summary>
        private readonly IBookService _bookService;

        /// <summary> Objeto para gerenciamento do acervo. </summary>
        private readonly ICollectionService _collectionService;

        /// <summary> Objeto para gerenciamento de sessão do usuário. </summary>
        private readonly ISessionService _sessionService;

        /// <summary> Objeto para acesso às configurações do usuário. </summary>
        private readonly ISettingsService _settingsService;


        public BookController(IBookService bookService, ICollectionService collectionService,
        ISessionService sessionService, ISettingsService settingsService) {
            _collectionService = collectionService;
            _bookService = bookService;
            _sessionService = sessionService;
            _settingsService = settingsService;
        }


        /// <summary>
        /// Retornar a página de manutenção do acervo.
        /// </summary>
        /// <returns>Página de manutenção do acervo.</returns>
        [HttpGet]
        public async Task<IActionResult> Manage() {

            if (!_sessionService.IsSessionActive()) {
                return RedirectToAction("Login", "Login");
            }

            var booksResp = await _collectionService.GetCollectionBooks();
            var deletedBooksResp = await _bookService.GetDeletedBooks();

            if (booksResp.Successful) {

                ViewBag.Settings = new SettingsDto(_settingsService);
                ViewBag.DeletedBooks = deletedBooksResp.Data != null ? deletedBooksResp.Data : new List<BookModel>();

                return View(booksResp.Data);
            
            } else {
                
                return BadRequest(booksResp.Message);
            
            }

        }


        /// <summary>
        /// Retornar a página Detalhes do Livro, sem dados.
        /// </summary>
        /// <returns>Página Detalhes do Livro, sem dados.</returns>
        [HttpGet]
        public IActionResult NoBook() {

            if (!_sessionService.IsSessionActive()) {
                return RedirectToAction("Login", "Login");
            }

            ViewBag.Settings = new SettingsDto(_settingsService);

            return View();

        }


        /// <summary>
        /// Retornar a página Detalhes do Livro.
        /// </summary>
        /// <param name="id">Identificador chave primária do livro.</param>
        /// <returns>Página Detalhes do livro.</returns>
        [HttpGet]
        public async Task<IActionResult> Details(Guid id) {

            if (!_sessionService.IsSessionActive()) {
                return RedirectToAction("Login", "Login");
            }

            var booksIdsResp = await _bookService.GetBooksIds();

            if (!booksIdsResp.Successful) return BadRequest(booksIdsResp.Message);

            List<Guid> booksIds = booksIdsResp.Data!;

            if (booksIds != null && booksIds.Count != 0) {

                BookModel? book;

                // Status de primeiro livro da lista.
                Boolean isFirstBook;

                // Status de último livro da lista.
                Boolean isLastBook;

                if (id == Guid.Empty) {

                    var firstBookResp = await _bookService.GetBook(booksIds.First());

                    book = firstBookResp.Data;

                    isFirstBook = true;

                    isLastBook = booksIds.First() == booksIds.Last();

                } else {

                    var bookResp = await _bookService.GetBook(id);

                    book = bookResp.Data;

                    isFirstBook = id == booksIds.First();

                    isLastBook = id == booksIds.Last();

                }

                // Passa os parâmetros para a Razor Page.
                ViewBag.IsFirstBook = isFirstBook;
                ViewBag.IsLastBook = isLastBook;

                if (book != null) {

                    var booksResp = await _bookService.GetBooksWithThumbnails();

                    if (booksResp.Successful) {
                        ViewBag.Books = booksResp.Data;
                    } else {
                        ViewBag.Books = new List<BookModel>();
                    }

                    ViewBag.Settings = new SettingsDto(_settingsService);

                    return View(book);
                
                } else {
                    
                    return NotFound();
                
                }

            } else {

                // Caso não exista nenhum livro cadastrado no banco de dados, retorna uma
                // página vazia, apenas com o contorno da capa, o títulos dos campos e sem
                // os botões de navegação.

                return RedirectToAction("NoBook");

            }

        }


        /// <summary>
        /// Retornar a página com os detalhes do livro adiante do atual na consulta.
        /// </summary>
        /// <param name="id">Identificador chave primária do livro atual.</param>
        /// <returns>Página com os detalhes do livro.</returns>
        [HttpGet]
        public async Task<IActionResult> NextBook(Guid id) {

            if (!_sessionService.IsSessionActive()) {
                return RedirectToAction("Login", "Login");
            }

            var nextBookResp = await _bookService.NextBookId(id);

            if (nextBookResp.Successful) {
                
                return RedirectToAction("Details", new { id = nextBookResp.Data });
            
            } else {
                
                return BadRequest(nextBookResp.Message);
            
            }

        }


        /// <summary>
        /// Retornar a página com os detalhes do livro anterior ao atual na consulta.
        /// </summary>
        /// <param name="id">Identificador chave primária do livro atual.</param>
        /// <returns>Página com os detalhes do livro.</returns>
        [HttpGet]
        public async Task<IActionResult> PreviousBook(Guid id) {

            if (!_sessionService.IsSessionActive()) {
                return RedirectToAction("Login", "Login");
            }

            var previousBookResp = await _bookService.PreviousBookId(id);

            if (previousBookResp.Successful) {
                
                return RedirectToAction("Details", new { id = previousBookResp.Data });
            
            } else {
                
                return BadRequest(previousBookResp.Message);
            
            }

        }


        /// <summary>
        /// Retornar a página com os detalhes do primeiro livro recuperado na consulta.
        /// </summary>
        /// <returns>Página com os detalhes do livro.</returns>
        [HttpGet]
        public async Task<IActionResult> FirstBook() {

            if (!_sessionService.IsSessionActive()) {
                return RedirectToAction("Login", "Login");
            }

            var firstBookResp = await _bookService.FirstBookId();

            if (firstBookResp.Successful) {
                
                return RedirectToAction("Details", new { id = firstBookResp.Data });
            
            } else {
                
                return BadRequest(firstBookResp.Message);
            
            }

        }


        /// <summary>
        /// Retornar a página com os detalhes do último livro recuperado na consulta.
        /// </summary>
        /// <returns>Página com os detalhes do livro.</returns>
        [HttpGet]
        public async Task<IActionResult> LastBook() {

            if (!_sessionService.IsSessionActive()) {
                return RedirectToAction("Login", "Login");
            }

            var lastBookResp = await _bookService.LastBookId();

            if (lastBookResp.Successful) {
                
                return RedirectToAction("Details", new { id = lastBookResp.Data });
            
            } else {
                
                return BadRequest(lastBookResp.Message);
            
            }

        }


        /// <summary>
        /// Retornar a página para cadastro de um novo livro.
        /// </summary>
        /// <returns>Página para cadastro de um novo livro.</returns>
        [HttpGet]
        public IActionResult Register() {

            if (!_sessionService.IsSessionActive()) {
                return RedirectToAction("Login", "Login");
            }

            return View();

        }


        /// <summary>
        /// Cadastrar um novo livro.
        /// </summary>
        /// <param name="book">Livro a ser cadastrado.</param>
        /// <returns>Página de redirecionamento.</returns>
        [HttpPost]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> Register(BookModel book) {

            if (!_sessionService.IsSessionActive()) {
                return RedirectToAction("Login", "Login");
            }

            if (ModelState.IsValid) {

                var registerBookResp = await _bookService.RegisterBook(book);

                if (registerBookResp.Successful) {
                    
                    TempData[Constants.SUCCESS_MESSAGE] = registerBookResp.Message;
                    
                    return RedirectToAction("Manage");
                
                } else {
                    
                    TempData[Constants.ERROR_MESSAGE] = registerBookResp.Message;
                    
                    return View(book);
                
                }

            } else {

                TempData[Constants.ERROR_MESSAGE] = "Dados do livro incorretos!";

                return View(book);

            }

        }


        /// <summary>
        /// Retornar a página para edição de um livro.
        /// </summary>
        /// <param name="id">Identificador chave primária do livro.</param>
        /// <returns>Página para edição de um livro.</returns>
        [HttpGet]
        public async Task<IActionResult> Edit(Guid id) {

            if (!_sessionService.IsSessionActive()) {
                return RedirectToAction("Login", "Login");
            }

            var bookResp = await _bookService.GetBook(id);

            if (bookResp.Successful) {

                return View(bookResp.Data);

            } else {

                return BadRequest(bookResp.Message);

            }

        }


        /// <summary>
        /// Alterar o cadastro de um livro.
        /// </summary>
        /// <param name="book">Livro a ser alterado.</param>
        /// <returns>Página de redirecionamento.</returns>
        [HttpPost]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> Edit(BookModel book) {

            if (!_sessionService.IsSessionActive()) {
                return RedirectToAction("Login", "Login");
            }

            if (ModelState.IsValid) {

                var editBookResp = await _bookService.EditBook(book);

                if (editBookResp.Successful) {
                    
                    TempData[Constants.SUCCESS_MESSAGE] = editBookResp.Message;
                    
                    return RedirectToAction("Manage");
                
                } else {

                    TempData[Constants.ERROR_MESSAGE] = editBookResp.Message;

                    return View(book);
                
                }

            } else {

                TempData[Constants.ERROR_MESSAGE] = "Dados do livro incorretos!";

                return View(book);

            }

        }


        /// <summary>
        /// Excluir o cadastro de um livro.
        /// </summary>
        /// <param name="id">Identificador chave primária do livro.</param>
        /// <returns>Página de redirecionamento.</returns>
        [HttpPost]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> Delete(Guid id) {

            if (!_sessionService.IsSessionActive()) {
                return RedirectToAction("Login", "Login");
            }

            var deleteBookResp = await _bookService.DeleteBook(id);

            if (deleteBookResp.Successful) {

                TempData[Constants.SUCCESS_MESSAGE] = deleteBookResp.Message;

                return RedirectToAction("Manage");

            } else {

                TempData[Constants.ERROR_MESSAGE] = deleteBookResp.Message;

                return RedirectToAction("Manage");

            }

        }


        /// <summary>
        /// Restaurar o cadastro de um livro.
        /// </summary>
        /// <param name="id">Identificador chave primária do livro.</param>
        /// <returns>Página de redirecionamento.</returns>
        [HttpPost]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> Undelete(Guid id) {

            if (!_sessionService.IsSessionActive()) {
                return RedirectToAction("Login", "Login");
            }

            var undeleteBookResp = await _bookService.UndeleteBook(id);

            if (undeleteBookResp.Successful) {

                TempData[Constants.SUCCESS_MESSAGE] = undeleteBookResp.Message;

                return RedirectToAction("Manage");

            } else {

                TempData[Constants.ERROR_MESSAGE] = undeleteBookResp.Message;

                return RedirectToAction("Manage");

            }

        }


    }


}
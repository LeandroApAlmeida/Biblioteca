using Library.Data;
using Library.Db.Models;
using Microsoft.EntityFrameworkCore;

namespace Library.Services.Collection {


    /// <summary>
    /// Classe para manutenção de livros descartados.
    /// </summary>
    public class DiscardService(ApplicationDbContext context, ILoanService loanService) : IDiscardService {


        /// <summary> Objeto para acesso ao banco de dados. </summary>
        private readonly ApplicationDbContext _context = context;

        /// <summary> Objeto para manutenção de livros emprestados. </summary>
        private readonly ILoanService _loanService = loanService;


        public async Task<Response<List<DiscardedBookModel>>> GetDiscardedBooks() {

            Response<List<DiscardedBookModel>> response = new();

            try {

                List<DiscardedBookModel> discardedBooks = await _context.DiscardedBooks
                .Include(db => db.Book)
                .OrderBy(db => db.Book.Title)
                .ThenBy(db => db.Id)
                .AsNoTracking()
                .ToListAsync();

                response.Data = discardedBooks;

                return response;

            } catch (Exception ex) {

                response.Message = ex.Message;
                response.Successful = false;

                return response;

            }

        }


        public async Task<Response<List<Guid>>> GetDiscardedBooksIds() {

            Response<List<Guid>> response = new();

            try {

                List<Guid> booksIds = await _context.DiscardedBooks.Select(db => db.Id).ToListAsync();

                response.Data = booksIds;

                return response;

            } catch (Exception ex) {

                response.Data = [];
                response.Message = ex.Message;
                response.Successful = false;

                return response;

            }

        }


        public async Task<Response<DiscardedBookModel>> GetDiscardedBook(Guid id) {

            Response<DiscardedBookModel> response = new();

            try {

                if (id != Guid.Empty) {

                    List<DiscardedBookModel> discardedBook = await _context.DiscardedBooks
                    .Include(db => db.Book)
                    .ThenInclude(db => db.Cover)
                    .Where(db => db.Id == id)
                    .AsNoTracking()
                    .ToListAsync();

                    response.Data = discardedBook.First();

                    return response;

                } else {

                    throw new Exception("Identificador do livro inválido.");

                }

            } catch (Exception ex) {

                response.Message = ex.Message;
                response.Successful = false;

                return response;

            }

        }

        
        public async Task<Response<DiscardedBookModel>> RegisterDiscardedBook(DiscardedBookModel discardedBook) {

            Response<DiscardedBookModel> response = new();

            try {

                var isBorrowedBookResp = await _loanService.IsBorrowedBook(discardedBook.Book.Id);

                if (!isBorrowedBookResp.Successful) throw new Exception(isBorrowedBookResp.Message);

                bool isBorrowed = isBorrowedBookResp.Data;

                if (!isBorrowed) {

                    _context.Attach(discardedBook.Book);

                    discardedBook.RegistrationDate = DateTime.Now;
                    discardedBook.LastUpdateDate = discardedBook.RegistrationDate;

                    _context.DiscardedBooks.Add(discardedBook);

                    await _context.SaveChangesAsync();

                    response.Message = "Livro cadastrado com sucesso!";
                    response.Data = discardedBook;

                    return response;

                } else {

                    response.Message = "O livro consta como emprestado. Faça a devolução!";

                    return response;

                }

            } catch (Exception ex) {

                response.Message = ex.Message;
                response.Successful = false;

                return response;

            }

        }

        
        public async Task<Response<DiscardedBookModel>> EditDiscardedBook(DiscardedBookModel discardedBook) {

            Response<DiscardedBookModel> response = new();

            try {

                _context.Attach(discardedBook);

                discardedBook.LastUpdateDate = DateTime.Now;

                _context.Entry(discardedBook).State = EntityState.Modified;

                _context.DiscardedBooks.Update(discardedBook);

                await _context.SaveChangesAsync();

                response.Message = "Livro alterado com sucesso!";
                response.Data = discardedBook;

                return response;

            } catch (Exception ex) {

                response.Message = ex.Message;
                response.Successful = false;

                return response;

            }

        }

                
        public async Task<Response<DiscardedBookModel>> DeleteDiscardedBook(Guid id) {

            Response<DiscardedBookModel> response = new();

            try {

                var discardedBookResp = await GetDiscardedBook(id);

                DiscardedBookModel? discardedBook = discardedBookResp.Data;

                if (discardedBook == null) throw new Exception(discardedBookResp.Message);

                _context.DiscardedBooks.Remove(discardedBook);

                await _context.SaveChangesAsync();

                response.Message = "Livro excluído com sucesso!";
                response.Data = discardedBook;

                return response;

            } catch (Exception ex) {

                response.Message = ex.Message;
                response.Successful = false;

                return response;

            }

        }


        public async Task<Response<bool>> IsDiscardedBook(Guid id) {

            Response<bool> response = new();

            try {

                if (id != Guid.Empty) {

                    var discardedBooksIds = await _context.DiscardedBooks
                    .Select(db => new { db.Id })
                    .AsNoTracking()
                    .ToListAsync();

                    bool isDiscarded = false;

                    foreach (var bookId in discardedBooksIds) {
                        if (bookId.Id == id) {
                            isDiscarded = true;
                            break;
                        }
                    }

                    response.Data = isDiscarded;

                    return response;

                } else {

                    throw new Exception("Identificador do livro inválido.");

                }

            } catch (Exception ex) {

                response.Message = ex.Message;
                response.Successful = false;

                return response;

            }

        }


    }


}

using Library.Data;
using Library.Db.Models;
using Microsoft.EntityFrameworkCore;

namespace Library.Services.Collection {


    public class DiscardService : IDiscardService {


        private readonly ApplicationDbContext _context;

        private readonly ICollectionService _collectionService;


        public DiscardService(ApplicationDbContext context, ICollectionService collectionService) {
            _context = context;
            _collectionService = collectionService;
        }


        public async Task<Response<List<DiscardedBookModel>>> GetDiscardedBooks() {

            Response<List<DiscardedBookModel>> response = new();

            try {

                List<DiscardedBookModel> discardedBooks = await _context.DiscardedBooks
                .Include(db => db.Book)
                .OrderBy(b => b.Book.Title)
                .ThenBy(b => b.Id)
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


        public async Task<Response<DiscardedBookModel>> GetDiscardedBook(Guid id) {

            Response<DiscardedBookModel> response = new();

            try {

                if (id != Guid.Empty) {

                    List<DiscardedBookModel> discardedBook = await _context.DiscardedBooks
                    .Include(db => db.Book)
                    .ThenInclude(b => b.Cover)
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

                var isBorrowedBookResp = await _collectionService.IsBorrowedBook(discardedBook.Book.Id);

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


    }


}

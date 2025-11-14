using Library.Data;
using Library.Db.Models;
using Microsoft.EntityFrameworkCore;

namespace Library.Services.Collection {


    /// <summary>
    /// Classe para manutenção de livros doados.
    /// </summary>
    public class DonationService : IDonationService {


        /// <summary> Objeto para acesso ao banco de dados. </summary>
        private readonly ApplicationDbContext _context;

        /// <summary> Objeto para manutenção de livros emprestados. </summary>
        private readonly ILoanService _loanService;


        public DonationService(ApplicationDbContext context, ILoanService loanService) {
            _context = context;
            _loanService = loanService;
        }


        public async Task<Response<List<DonatedBookModel>>> GetDonatedBooks() {

            Response<List<DonatedBookModel>> response = new();

            try {

                List<DonatedBookModel> donatedBooks = await _context.DonatedBooks
                .Include(db => db.Person)
                .Include(db => db.Book)
                .OrderBy(db => db.Book.Title)
                .ThenBy(db => db.Book.Id)
                .AsNoTracking()
                .ToListAsync();

                response.Data = donatedBooks;

                return response;

            } catch (Exception ex) {

                response.Message = ex.Message;
                response.Successful = false;
                response.Data = null;

                return response;

            }

        }


        public async Task<Response<List<Guid>>> GetDonatedBooksIds() {

            Response<List<Guid>> response = new();

            try {

                List<Guid> booksIds = await _context.DonatedBooks.Select(db => db.Id).ToListAsync();

                response.Data = booksIds;

                return response;

            } catch (Exception ex) {

                response.Data = [];
                response.Message = ex.Message;
                response.Successful = false;

                return response;

            }

        }


        public async Task<Response<DonatedBookModel>> GetDonatedBook(Guid id) {

            Response<DonatedBookModel> response = new();

            try {

                if (id != Guid.Empty) {

                    List<DonatedBookModel> donatedBooks = await _context.DonatedBooks
                    .Include(db => db.Person)
                    .Include (db => db.Book)
                    .ThenInclude(db => db.Cover)
                    .Where(db => db.Id == id)
                    .AsNoTracking()
                    .ToListAsync();

                    response.Data = donatedBooks.First();

                    return response;

                } else {

                    throw new Exception("Identificador do livro inválido.");

                }

            } catch (Exception ex) {

                response.Message = ex.Message;
                response.Successful = false;
                response.Data = null;

                return response;

            }

        }


        public async Task<Response<DonatedBookModel>> RegisterDonatedBook(DonatedBookModel donatedBook) {

            Response<DonatedBookModel> response = new();

            try {

                var isBorrowedBookResp = await _loanService.IsBorrowedBook(donatedBook.Book.Id);

                if (!isBorrowedBookResp.Successful) throw new Exception(isBorrowedBookResp.Message);

                bool isBorrowed = isBorrowedBookResp.Data;

                if (!isBorrowed) {

                    _context.Attach(donatedBook.Book);
                    _context.Attach(donatedBook.Person);

                    donatedBook.RegistrationDate = DateTime.Now;
                    donatedBook.LastUpdateDate = donatedBook.RegistrationDate;

                    _context.DonatedBooks.Add(donatedBook);

                    await _context.SaveChangesAsync();

                    response.Message = "Livro cadastrado com sucesso!";
                    response.Data = donatedBook;

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


        public async Task<Response<DonatedBookModel>> EditDonatedBook(DonatedBookModel donatedBook) {

            Response<DonatedBookModel> response = new();

            try {

                _context.Attach(donatedBook);

                donatedBook.LastUpdateDate = DateTime.Now;

                _context.Entry(donatedBook).State = EntityState.Modified;

                _context.DonatedBooks.Update(donatedBook);

                await _context.SaveChangesAsync();

                response.Message = "Livro alterado com sucesso!";
                response.Data = donatedBook;

                return response;

            } catch (Exception ex) {

                response.Message = ex.Message;
                response.Successful = false;

                return response;

            }

        }


        public async Task<Response<DonatedBookModel>> DeleteDonatedBook(Guid id) {

            Response<DonatedBookModel> response = new();

            try {

                var donatedBookResp = await GetDonatedBook(id);

                DonatedBookModel? donatedBook = donatedBookResp.Data;

                if (donatedBook == null) throw new Exception(donatedBookResp.Message);

                _context.DonatedBooks.Remove(donatedBook);

                await _context.SaveChangesAsync();

                response.Message = "Livro excluído com sucesso!";
                response.Data = donatedBook;

                return response;

            } catch (Exception ex) {

                response.Message = ex.Message;
                response.Successful = false;

                return response;

            }

        }


        public async Task<Response<bool>> IsDonatedBook(Guid id) {

            Response<bool> response = new();

            try {

                if (id != Guid.Empty) {

                    var donatedBooksIds = await _context.DonatedBooks
                    .Select(db => new { db.Id })
                    .AsNoTracking()
                    .ToListAsync();

                    bool isDonated = false;

                    foreach (var bookId in donatedBooksIds) {
                        if (bookId.Id == id) {
                            isDonated = true;
                            break;
                        }
                    }

                    response.Data = isDonated;

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

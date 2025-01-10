using Library.Data;
using Library.Models;
using Microsoft.EntityFrameworkCore;

namespace Library.Services.CollectionService {


    public class CollectionService: ICollectionService {


        private readonly ApplicationDbContext _context;


        public CollectionService(ApplicationDbContext context) {
            _context = context;
        }


        public async Task<ResponseModel<List<BookModel>>> GetAvailableBooks() {

            ResponseModel<List<BookModel>> response = new();

            try {

                var discardedBooksIdsResp = await DiscardedBooksIds();
                var donatedBooksIdsResp = await DonatedBooksIds();
                var borrowedBooksIdsResp = await BorrowedBooksIds();

                if (!discardedBooksIdsResp.Successful) throw new Exception(discardedBooksIdsResp.Message);
                if (!donatedBooksIdsResp.Successful) throw new Exception(donatedBooksIdsResp.Message);
                if (!borrowedBooksIdsResp.Successful) throw new Exception(borrowedBooksIdsResp.Message);

                List<BookModel> availableBooks = await _context.Books
                .Select(b => new BookModel {
                    Id = b.Id,
                    Title = b.Title,
                    Subtitle = b.Subtitle,
                    Author = b.Author,
                    Publisher = b.Publisher,
                    Isbn = b.Isbn,
                    Edition = b.Edition,
                    Volume = b.Volume,
                    ReleaseYear = b.ReleaseYear,
                    NumberOfPages = b.NumberOfPages,
                    AcquisitionDate = b.AcquisitionDate,
                    Summary = b.Summary,
                    LastUpdateDate = b.LastUpdateDate,
                    RegistrationDate = b.RegistrationDate,
                    Cover = "",
                    IsDeleted = b.IsDeleted
                })
                .Where(b =>
                    !discardedBooksIdsResp.Data!.Contains(b.Id) &&
                    !donatedBooksIdsResp.Data!.Contains(b.Id) &&
                    !borrowedBooksIdsResp.Data!.Contains(b.Id) &&
                    b.IsDeleted == false
                )
                .OrderBy(b => b.Title)
                .ThenBy(b => b.Id)
                .AsNoTracking()
                .ToListAsync();

                response.Data = availableBooks;

                return response;

            } catch (Exception ex) {

                response.Message = ex.Message;
                response.Successful = false;

                return response;

            }

        }


        public async Task<ResponseModel<Boolean>> IsBorrowedBook(Guid id) {

            ResponseModel<Boolean> response = new();

            try {

                if (id != Guid.Empty) {

                    var loans = await _context.Loans
                    .Select(lm => new { lm.IsReturned, lm.IsDeleted, lm.Book.Id })
                    .Where(lm => lm.IsDeleted == false && lm.IsReturned == false)
                    .AsNoTracking()
                    .ToListAsync();

                    Boolean isBorrowed = false;

                    foreach (var loan in loans) {
                        if (loan.Id == id) {
                            isBorrowed = true;
                            break;
                        }
                    }

                    response.Data = isBorrowed;

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


        /// <summary>
        /// Obter os identificadores (id) dos livros no acervo.
        /// </summary>
        /// <returns></returns>
        public async Task<ResponseModel<List<Guid>>> GetBooksIds() {

            ResponseModel<List<Guid>> response = new();

            try {

                List<Guid> booksIds = await _context.Books
                .Where(b => b.IsDeleted == false)
                .OrderBy(b => b.Title)
                .ThenBy(b => b.Id)
                .Select(b => b.Id).ToListAsync();

                response.Data = booksIds;

                return response;

            } catch (Exception ex) {

                response.Data = [];
                response.Message = ex.Message;
                response.Successful = false;

                return response;

            }

        }


        public async Task<ResponseModel<List<Guid>>> BorrowedBooksIds() {

            ResponseModel<List<Guid>> response = new();

            try {

                List<Guid> booksIds = await _context.Loans
                .Where(lm => lm.IsReturned == false && lm.IsDeleted == false)
                .Select(lm => lm.Book.Id).ToListAsync();

                response.Data = booksIds;

                return response;

            } catch (Exception ex) {

                response.Data = [];
                response.Message = ex.Message;
                response.Successful = false;

                return response;

            }

        }


        public async Task<ResponseModel<List<Guid>>> DiscardedBooksIds() {

            ResponseModel<List<Guid>> response = new();

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


        public async Task<ResponseModel<List<Guid>>> DonatedBooksIds() {

            ResponseModel<List<Guid>> response = new();

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


    }


}

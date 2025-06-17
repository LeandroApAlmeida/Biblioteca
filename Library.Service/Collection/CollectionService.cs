using Library.Data;
using Library.Db.Models;
using Microsoft.EntityFrameworkCore;

namespace Library.Services.Collection {


    public class CollectionService: ICollectionService {


        private readonly ApplicationDbContext _context;

        private List<Guid>? borrowedBooksIds;

        private List<Guid>? donatedBooksIds;

        private List<Guid>? discardedBooksIds;


        public CollectionService(ApplicationDbContext context) {
            _context = context;
            FillBooksLists().Wait();
        }


        private async Task FillBooksLists() {

            var borrowedBooksIdsResp = await BorrowedBooksIds();
            var donatedBooksIdsResp = await DonatedBooksIds();
            var discardedBooksIdsResp = await DiscardedBooksIds();

            borrowedBooksIds = borrowedBooksIdsResp.Data;
            donatedBooksIds = donatedBooksIdsResp.Data;
            discardedBooksIds = discardedBooksIdsResp.Data;

        }


        public async Task<Response<List<BookModel>>> GetAvailableBooks() {

            Response<List<BookModel>> response = new();

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
                    Cover = b.Cover,
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

                foreach (var book in availableBooks) {
                    book.IsBorrowed = borrowedBooksIds!.Contains(book.Id);
                    book.IsDonated = donatedBooksIds!.Contains(book.Id);
                    book.IsDiscarded = discardedBooksIds!.Contains(book.Id);
                }

                response.Data = availableBooks;

                return response;

            } catch (Exception ex) {

                response.Message = ex.Message;
                response.Successful = false;

                return response;

            }

        }


        public async Task<Response<List<BookModel>>> GetCollectionBooks() {

            Response<List<BookModel>> response = new();

            try {

                var discardedBooksIdsResp = await DiscardedBooksIds();
                var donatedBooksIdsResp = await DonatedBooksIds();

                if (!discardedBooksIdsResp.Successful) throw new Exception(discardedBooksIdsResp.Message);
                if (!donatedBooksIdsResp.Successful) throw new Exception(donatedBooksIdsResp.Message);

                List<BookModel> availableBooks = await _context.Books
                .Select(b => new BookModel {
                    Id = b.Id,
                    Cover = b.Cover,
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
                    IsDeleted = b.IsDeleted
                })
                .Where(b =>
                    !discardedBooksIdsResp.Data!.Contains(b.Id) &&
                    !donatedBooksIdsResp.Data!.Contains(b.Id) &&
                    b.IsDeleted == false
                )
                .OrderBy(b => b.Title)
                .ThenBy(b => b.Id)
                .AsNoTracking()
                .ToListAsync();

                foreach (var book in availableBooks) {
                    book.IsBorrowed = borrowedBooksIds!.Contains(book.Id);
                    book.IsDonated = donatedBooksIds!.Contains(book.Id);
                    book.IsDiscarded = discardedBooksIds!.Contains(book.Id);
                }

                response.Data = availableBooks;

                return response;

            } catch (Exception ex) {

                response.Message = ex.Message;
                response.Successful = false;

                return response;

            }

        }


        public async Task<Response<List<BookModel>>> GetDeletedBooks() {

            Response<List<BookModel>> response = new();

            try {

               
                List<BookModel> availableBooks = await _context.Books
                .Select(b => new BookModel {
                    Id = b.Id,
                    Cover = b.Cover,
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
                    IsDeleted = b.IsDeleted
                })
                .Where(b =>
                    b.IsDeleted == true
                )
                .OrderBy(b => b.Title)
                .ThenBy(b => b.Id)
                .AsNoTracking()
                .ToListAsync();

                foreach (var book in availableBooks) {
                    book.IsBorrowed = borrowedBooksIds!.Contains(book.Id);
                    book.IsDonated = donatedBooksIds!.Contains(book.Id);
                    book.IsDiscarded = discardedBooksIds!.Contains(book.Id);
                }

                response.Data = availableBooks;

                return response;

            } catch (Exception ex) {

                response.Message = ex.Message;
                response.Successful = false;

                return response;

            }

        }


        public async Task<Response<bool>> IsBorrowedBook(Guid id) {

            Response<bool> response = new();

            try {

                if (id != Guid.Empty) {

                    var loans = await _context.Loans
                    .Select(lm => new { lm.IsReturned, lm.IsDeleted, lm.Book.Id })
                    .Where(lm => lm.IsDeleted == false && lm.IsReturned == false)
                    .AsNoTracking()
                    .ToListAsync();

                    bool isBorrowed = false;

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


        /// <summary>
        /// Obter os identificadores (id) dos livros no acervo.
        /// </summary>
        /// <returns></returns>
        public async Task<Response<List<Guid>>> GetBooksIds() {

            Response<List<Guid>> response = new();

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


        public async Task<Response<List<Guid>>> BorrowedBooksIds() {

            Response<List<Guid>> response = new();

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


        public async Task<Response<List<Guid>>> DiscardedBooksIds() {

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


        public async Task<Response<List<Guid>>> DonatedBooksIds() {

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


    }


}

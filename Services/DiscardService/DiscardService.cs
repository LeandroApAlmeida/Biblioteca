using Library.Data;
using Library.Models;
using Library.Services.CollectionService;
using Microsoft.EntityFrameworkCore;

namespace Library.Services.DiscardService {


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
                .Select(db => new DiscardedBookModel {

                    Id = db.Id,

                    Book = new BookModel {
                        Id = db.Book.Id,
                        Title = db.Book.Title,
                        Subtitle = db.Book.Subtitle,
                        Author = db.Book.Author,
                        Publisher = db.Book.Publisher,
                        Isbn = db.Book.Isbn,
                        Edition = db.Book.Edition,
                        Volume = db.Book.Volume,
                        ReleaseYear = db.Book.ReleaseYear,
                        NumberOfPages = db.Book.NumberOfPages,
                        AcquisitionDate = db.Book.AcquisitionDate,
                        Summary = db.Book.Summary,
                        LastUpdateDate = db.Book.LastUpdateDate,
                        RegistrationDate = db.Book.RegistrationDate,
                        Cover = "",
                        IsDeleted = db.Book.IsDeleted
                    },

                    Reason = db.Reason,

                    Date = db.Date,

                    RegistrationDate = db.RegistrationDate,

                    LastUpdateDate = db.LastUpdateDate

                })
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
                    .Select(db => new DiscardedBookModel {

                        Id = db.Id,

                        Book = new BookModel {
                            Id = db.Book.Id,
                            Title = db.Book.Title,
                            Subtitle = db.Book.Subtitle,
                            Author = db.Book.Author,
                            Publisher = db.Book.Publisher,
                            Isbn = db.Book.Isbn,
                            Edition = db.Book.Edition,
                            Volume = db.Book.Volume,
                            ReleaseYear = db.Book.ReleaseYear,
                            NumberOfPages = db.Book.NumberOfPages,
                            AcquisitionDate = db.Book.AcquisitionDate,
                            Summary = db.Book.Summary,
                            LastUpdateDate = db.Book.LastUpdateDate,
                            RegistrationDate = db.Book.RegistrationDate,
                            Cover = db.Book.Cover,
                            IsDeleted = db.Book.IsDeleted
                        },

                        Reason = db.Reason,

                        Date = db.Date,

                        RegistrationDate = db.RegistrationDate,

                        LastUpdateDate = db.LastUpdateDate

                    })
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

                Boolean isBorrowed = isBorrowedBookResp.Data;

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


        public async Task<Response<DiscardedBookModel>> DeleteDiscardedBook(DiscardedBookModel discardedBook) {

            Response<DiscardedBookModel> response = new();

            try {

                _context.Attach(discardedBook);

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

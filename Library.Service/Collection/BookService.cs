using Library.Data;
using Library.Db.Models;
using Microsoft.EntityFrameworkCore;

namespace Library.Services.Collection {


    /// <summary>
    /// Service para a manutenção de livros do acervo.
    /// </summary>
    public class BookService : IBookService {


        private readonly ApplicationDbContext _context;

        private readonly ICollectionService _collectionService;

        private List<Guid>? borrowedBooksIds;

        private List<Guid>? donatedBooksIds;

        private List<Guid>? discardedBooksIds;


        public BookService(ApplicationDbContext context, ICollectionService collectionService) {
            _context = context;
            _collectionService = collectionService;
            FillBooksLists().Wait();
        }


        private async Task FillBooksLists() {
            
            var borrowedBooksIdsResp = await _collectionService.BorrowedBooksIds();
            var donatedBooksIdsResp = await _collectionService.DonatedBooksIds();
            var discardedBooksIdsResp = await _collectionService.DiscardedBooksIds();
            
            borrowedBooksIds = borrowedBooksIdsResp.Data;
            donatedBooksIds = donatedBooksIdsResp.Data;
            discardedBooksIds = discardedBooksIdsResp.Data;
        
        }


        public async Task<Response<List<BookModel>>> GetBooks() {

            Response<List<BookModel>> response = new();

            try {

                var discardedBooksIdsResp = await _collectionService.DiscardedBooksIds();
                var donatedBooksIdsResp = await _collectionService.DonatedBooksIds();

                if (!discardedBooksIdsResp.Successful) throw new Exception(discardedBooksIdsResp.Message);
                if (!donatedBooksIdsResp.Successful) throw new Exception(donatedBooksIdsResp.Message);

                List<BookModel> books = await _context.Books
                .Where(b =>
                    b.IsDeleted == false
                )
                .OrderBy(b => b.Title)
                .ThenBy(b => b.Id)
                .AsNoTracking()
                .ToListAsync();

                foreach (var book in books) {
                    book.IsBorrowed = borrowedBooksIds!.Contains(book.Id);
                    book.IsDonated = donatedBooksIds!.Contains(book.Id);
                    book.IsDiscarded = discardedBooksIds!.Contains(book.Id);
                }

                response.Data = books;

                return response;

            } catch (Exception ex) {

                response.Message = ex.Message;
                response.Successful = false;

                return response;

            }

        }


        public async Task<Response<BookModel>> GetBook(Guid id) {

            Response<BookModel> response = new();

            try {

                if (id != Guid.Empty) {

                    List<BookModel> books = await _context.Books
                    .Include(b => b.Cover)
                    .Where(b => b.Id == id)
                    .AsNoTracking()
                    .ToListAsync();

                    if (books != null && books.Count > 0) {

                        BookModel book = books.First();

                        var borrowedResp = await _collectionService.IsBorrowedBook(book.Id);
                        var donatedResp = await _collectionService.IsDonatedBook(book.Id);
                        var discardedResp = await _collectionService.IsDiscardedBook(book.Id);
                        
                        book.IsBorrowed = borrowedResp.Data;
                        book.IsDonated = donatedResp.Data;
                        book.IsDiscarded = discardedResp.Data;

                        response.Data = book;

                        return response;

                    } else {

                        throw new Exception("Livro não encontrado.");

                    }

                } else {

                    throw new Exception("Identificador do livro inválido.");

                }

            } catch (Exception ex) {

                response.Data = null;
                response.Message = ex.Message;
                response.Successful = false;

                return response;

            }

        }


        public async Task<Response<Guid>> NextBookId(Guid id) {

            Response<Guid> response = new();

            try {

                if (id != Guid.Empty) {

                    Guid guid = id;

                    var booksIdsResp = await _collectionService.GetBooksIds();

                    bool getNextId = false;

                    foreach (Guid bookId in booksIdsResp.Data!) {
                        if (!getNextId) {
                            if (bookId == id) {
                                getNextId = true;
                            }
                        } else {
                            guid = bookId;
                            break;
                        }
                    }

                    response.Data = guid;

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


        public async Task<Response<Guid>> PreviousBookId(Guid id) {

            Response<Guid> response = new();

            try {

                if (id != Guid.Empty) {

                    Guid guid = id;

                    var booksIdsResp = await _collectionService.GetBooksIds();

                    Guid guidTmp = booksIdsResp.Data!.First();

                    foreach (Guid bookId in booksIdsResp.Data!) {
                        if (bookId == id) {
                            guid = guidTmp;
                            break;
                        }
                        guidTmp = bookId;
                    }

                    response.Data = guid;

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


        public async Task<Response<Guid>> FirstBookId() {

            Response<Guid> response = new();

            try {

                var booksIdsResp = await _collectionService.GetBooksIds();

                response.Data = booksIdsResp.Data!.First();

                return response;

            } catch (Exception ex) {

                response.Message = ex.Message;
                response.Successful = false;

                return response;

            }

        }


        public async Task<Response<Guid>> LastBookId() {

            Response<Guid> response = new();

            try {

                var booksIdsResp = await _collectionService.GetBooksIds();

                response.Data = booksIdsResp.Data!.Last();

                return response;

            } catch (Exception ex) {

                response.Message = ex.Message;
                response.Successful = false;

                return response;

            }

        }


        public async Task<Response<BookModel>> RegisterBook(BookModel book) {

            Response<BookModel> response = new();

            try {

                DateTime date = DateTime.Now;

                book.Id = Guid.NewGuid();
                book.RegistrationDate = date;
                book.LastUpdateDate = date;
                book.IsDeleted = false;

                book.Cover.Id = Guid.NewGuid();
                book.Cover.RegistrationDate = date;
                book.Cover.LastUpdateDate = date;

                _context.Books.Add(book);

                await _context.SaveChangesAsync();

                response.Message = "Livro cadastrado com sucesso!";
                response.Data = book;

                return response;

            } catch (Exception ex) {

                response.Message = ex.Message;
                response.Successful = false;

                return response;

            }

        }


        public async Task<Response<BookModel>> EditBook(BookModel book) {

            Response<BookModel> response = new();

            try {

                _context.Attach(book);

                book.LastUpdateDate = DateTime.Now;

                _context.Entry(book).State = EntityState.Modified;

                _context.Books.Update(book);

                _context.Attach(book.Cover);

                book.Cover.LastUpdateDate = DateTime.Now;

                _context.Entry(book.Cover).State = EntityState.Modified;

                _context.Cover.Update(book.Cover);

                await _context.SaveChangesAsync();

                response.Message = "Livro alterado com sucesso!";
                response.Data = book;

                return response;

            } catch (Exception ex) {

                response.Message = ex.Message;
                response.Successful = false;

                return response;

            }

        }


        public async Task<Response<BookModel>> DeleteBook(Guid id) {

            Response<BookModel> response = new();

            try {

                var bookResp = await GetBook(id);

                BookModel? book = bookResp.Data;

                if (book == null) throw new Exception(bookResp.Message);

                var isBorrowedBookResp = await _collectionService.IsBorrowedBook(book.Id);

                if (!isBorrowedBookResp.Successful) throw new Exception(isBorrowedBookResp.Message);

                bool isBorrowed = isBorrowedBookResp.Data;

                if (!isBorrowed) {

                    book.IsDeleted = true;
                    book.LastUpdateDate = DateTime.Now;

                    _context.Entry(book).State = EntityState.Modified;

                    _context.Books.Update(book);

                    await _context.SaveChangesAsync();

                    response.Message = "Livro excluído com sucesso!";
                    response.Data = book;

                    return response;

                } else {

                    throw new Exception("O livro consta como emprestado. Faça a devolução!");

                }

            } catch (Exception ex) {

                response.Message = ex.Message;
                response.Successful = false;

                return response;

            }

        }


        public async Task<Response<BookModel>> UndeleteBook(Guid id) {

            Response<BookModel> response = new();

            try {

                var bookResp = await GetBook(id);

                BookModel? book = bookResp.Data;

                if (book == null) throw new Exception(bookResp.Message);

                book.IsDeleted = false;
                book.LastUpdateDate = DateTime.Now;

                _context.Entry(book).State = EntityState.Modified;

                _context.Books.Update(book);

                await _context.SaveChangesAsync();

                response.Message = "Livro retornado com sucesso!";
                response.Data = book;

                return response;

            } catch (Exception ex) {

                response.Message = ex.Message;
                response.Successful = false;

                return response;

            }

        }


    }


}
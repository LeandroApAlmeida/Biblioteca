using Library.Data;
using Library.Db.Models;
using Microsoft.EntityFrameworkCore;
using System;
using System.Drawing;
using System.Drawing.Imaging;
using System.IO;
using static System.Runtime.InteropServices.JavaScript.JSType;


namespace Library.Services.Collection {


    /// <summary>
    /// Service para a manutenção de livros do acervo.
    /// </summary>
    public class BookService : IBookService {


        private readonly ApplicationDbContext _context;

        private readonly IDiscardService _discardService;

        private readonly IDonationService _donationService;

        private readonly ILoanService _loanService;

        private List<Guid>? borrowedBooksIds;

        private List<Guid>? donatedBooksIds;

        private List<Guid>? discardedBooksIds;


        public BookService(ApplicationDbContext context, IDiscardService discardService,
        ILoanService loanService, IDonationService donationService) {
            _context = context;
            _discardService = discardService;
            _loanService = loanService;
            _donationService = donationService;
            FillBooksLists().Wait();
        }


        private async Task FillBooksLists() {
            
            var borrowedBooksIdsResp = await _loanService.GetBorrowedBooksIds();
            var donatedBooksIdsResp = await _donationService.GetDonatedBooksIds();
            var discardedBooksIdsResp = await _discardService.GetDiscardedBooksIds();
            
            borrowedBooksIds = borrowedBooksIdsResp.Data;
            donatedBooksIds = donatedBooksIdsResp.Data;
            discardedBooksIds = discardedBooksIdsResp.Data;
        
        }


        public async Task<Response<List<BookModel>>> GetBooks() {

            Response<List<BookModel>> response = new();

            try {

                List<BookModel> books = await _context.Books
                .Where(b => b.IsDeleted == false)
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


        public async Task<Response<List<BookModel>>> GetBooksWithThumbnails() {

            Response<List<BookModel>> response = new();

            try {

                List<BookModel> books = await _context.Books
                .Select(static b => new BookModel {
                    
                    Id = b.Id,
                    Title = b.Title,
                    Subtitle = b.Subtitle,
                    Author = b.Author,
                    Publisher = b.Publisher,
                    Isbn = b.Isbn,
                    Edition = b.Edition,
                    Volume = b.Volume,
                    ReleaseYear = b.ReleaseYear,
                    AcquisitionDate = b.AcquisitionDate,
                    NumberOfPages = b.NumberOfPages,
                    RegistrationDate = b.RegistrationDate,
                    LastUpdateDate = b.LastUpdateDate,
                    Summary = b.Summary,
                    IsDeleted = b.IsDeleted, 
                    IsBorrowed = b.IsBorrowed,
                    IsDiscarded = b.IsDiscarded,
                    IsDonated = b.IsDonated,
                    
                    Cover = new CoverModel {
                        Id = b.Cover.Id,
                        Data = "",
                        Thumbnail = b.Cover.Thumbnail,
                        RegistrationDate = b.Cover.RegistrationDate,
                        LastUpdateDate = b.Cover.LastUpdateDate
                    }

                })
                .Where(b => b.IsDeleted == false)
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


        public async Task<Response<List<BookModel>>> GetDeletedBooks() {

            Response<List<BookModel>> response = new();

            try {

                List<BookModel> availableBooks = await _context.Books
                .Where(b => b.IsDeleted == true)
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

                        book.IsBorrowed = borrowedBooksIds!.Contains(book.Id);
                        book.IsDonated = donatedBooksIds!.Contains(book.Id);
                        book.IsDiscarded = discardedBooksIds!.Contains(book.Id);

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

                    var booksIdsResp = await GetBooksIds();

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

                    var booksIdsResp = await GetBooksIds();

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

                var booksIdsResp = await GetBooksIds();

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

                var booksIdsResp = await GetBooksIds();

                response.Data = booksIdsResp.Data!.Last();

                return response;

            } catch (Exception ex) {

                response.Message = ex.Message;
                response.Successful = false;

                return response;

            }

        }


        private string CreateCoverThumbnail(string imageBase64) {
            
            string base64Data = imageBase64.Substring(imageBase64.IndexOf(",") + 1);
            byte[] imageData = Convert.FromBase64String(base64Data);

            using MemoryStream ms = new MemoryStream(imageData);
            using Image image = Image.FromStream(ms);
            using Bitmap thumbnail = new Bitmap(image, new Size(90, 120));
            using MemoryStream thumbStream = new MemoryStream();

            thumbnail.Save(thumbStream, ImageFormat.Jpeg);

            return $"data:image/jpeg;base64,{Convert.ToBase64String(thumbStream.ToArray())}";
        }


        private string ConvertCoverToJpeg(string base64Image) {

            if (!base64Image.StartsWith("data:image/jpeg;base64")) {

                string base64Data = base64Image.Substring(base64Image.IndexOf(",") + 1);
                byte[] imageData = Convert.FromBase64String(base64Data);

                using MemoryStream ms = new MemoryStream(imageData);
                using Image image = Image.FromStream(ms);
                using MemoryStream jpgStream = new MemoryStream();

                image.Save(jpgStream, ImageFormat.Jpeg);

                return $"data:image/jpeg;base64,{Convert.ToBase64String(jpgStream.ToArray())}";

            } else {

                return base64Image;

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

                string base64Image = book.Cover.Data;

                book.Cover.Id = Guid.NewGuid();
                book.Cover.Data = ConvertCoverToJpeg(base64Image);
                book.Cover.Thumbnail = CreateCoverThumbnail(base64Image);
                book.Cover.RegistrationDate = date;
                book.Cover.LastUpdateDate = date;

                _context.Books.Add(book);

                _context.Covers.Add(book.Cover);

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

                string base64Image = book.Cover.Data;

                book.Cover.Data = ConvertCoverToJpeg(base64Image);
                book.Cover.Thumbnail = CreateCoverThumbnail(base64Image);
                book.Cover.LastUpdateDate = DateTime.Now;

                _context.Entry(book.Cover).State = EntityState.Modified;

                _context.Covers.Update(book.Cover);

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

                var isBorrowedBookResp = await _loanService.IsBorrowedBook(book.Id);

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
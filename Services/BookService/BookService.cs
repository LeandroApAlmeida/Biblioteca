﻿using Library.Data;
using Library.Models;
using Library.Services.CollectionService;
using Microsoft.EntityFrameworkCore;

namespace Library.Services.BookService {


    public class BookService : IBookService {


        private readonly ApplicationDbContext _context;

        private readonly ICollectionService _collectionService;


        public BookService(ApplicationDbContext context, ICollectionService collectionService) {
            _context = context;
            _collectionService = collectionService;
        }


        public async Task<ResponseModel<List<BookModel>>> GetBooks() {

            ResponseModel<List<BookModel>> response = new();

            try {

                var discardedBooksIdsResp = await _collectionService.DiscardedBooksIds();
                var donatedBooksIdsResp = await _collectionService.DonatedBooksIds();

                if (!discardedBooksIdsResp.Successful) throw new Exception(discardedBooksIdsResp.Message);
                if (!donatedBooksIdsResp.Successful) throw new Exception(donatedBooksIdsResp.Message);

                List<BookModel> books = await _context.Books
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
                    b.IsDeleted == false
                )
                .OrderBy(b => b.Title)
                .ThenBy(b => b.Id)
                .AsNoTracking()
                .ToListAsync();

                response.Data = books;

                return response;

            } catch (Exception ex) {

                response.Message = ex.Message;
                response.Successful = false;

                return response;

            }

        }


        public async Task<ResponseModel<BookModel>> GetBook(Guid id) {

            ResponseModel<BookModel> response = new();

            try {

                if (id != Guid.Empty) {

                    List<BookModel> books = await _context.Books
                    .Where(b => b.Id == id)
                    .AsNoTracking()
                    .ToListAsync();

                    if (books != null && books.Count > 0) {

                        BookModel book = books.First();

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


        public async Task<ResponseModel<Guid>> NextBookId(Guid id) {

            ResponseModel<Guid> response = new();

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


        public async Task<ResponseModel<Guid>> PreviousBookId(Guid id) {

            ResponseModel<Guid> response = new();

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


        public async Task<ResponseModel<Guid>> FirstBookId() {

            ResponseModel<Guid> response = new();

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


        public async Task<ResponseModel<Guid>> LastBookId() {

            ResponseModel<Guid> response = new();

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


        public async Task<ResponseModel<BookModel>> RegisterBook(BookModel book) {

            ResponseModel<BookModel> response = new();

            try {

                DateTime date = DateTime.Now;

                book.Id = Guid.NewGuid();
                book.RegistrationDate = date;
                book.LastUpdateDate = date;
                book.IsDeleted = false;

                _context.Books.Add(book);

                await _context.SaveChangesAsync();

                response.Message = "Livro cadastrado com sucesso!";

                return response;

            } catch (Exception ex) {

                response.Message = ex.Message;
                response.Successful = false;

                return response;

            }

        }


        public async Task<ResponseModel<BookModel>> EditBook(BookModel book) {

            ResponseModel<BookModel> response = new();

            try {

                _context.Attach(book);

                book.LastUpdateDate = DateTime.Now;

                _context.Entry(book).State = EntityState.Modified;

                _context.Books.Update(book);

                await _context.SaveChangesAsync();

                response.Message = "Livro alterado com sucesso!";

                return response;

            } catch (Exception ex) {

                response.Message = ex.Message;
                response.Successful = false;

                return response;

            }

        }


        public async Task<ResponseModel<BookModel>> DeleteBook(BookModel book) {

            ResponseModel<BookModel> response = new();

            try {

                var isBorrowedBookResp = await _collectionService.IsBorrowedBook(book.Id);

                if (!isBorrowedBookResp.Successful) throw new Exception(isBorrowedBookResp.Message);

                Boolean isBorrowed = isBorrowedBookResp.Data;

                if (!isBorrowed) {

                    _context.Attach(book);

                    book.IsDeleted = true;
                    book.LastUpdateDate = DateTime.Now;

                    _context.Entry(book).State = EntityState.Modified;

                    _context.Books.Update(book);

                    await _context.SaveChangesAsync();

                    response.Message = "Livro excluído com sucesso!";

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


    }


}
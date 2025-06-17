using Library.Data;
using Library.Db.Models;
using Microsoft.EntityFrameworkCore;

namespace Library.Services.Collection {


    public class DonationService : IDonationService {


        private readonly ApplicationDbContext _context;

        private readonly ICollectionService _collectionService;


        public DonationService(ApplicationDbContext context, ICollectionService collectionService) {
            _context = context;
            _collectionService = collectionService;
        }


        public async Task<Response<List<DonatedBookModel>>> GetDonatedBooks() {

            Response<List<DonatedBookModel>> response = new();

            try {

                List<DonatedBookModel> donatedBooks = await _context.DonatedBooks
                .Select(db => new DonatedBookModel {

                    Id = db.Id,

                    Book = new BookModel {
                        Id = db.Book.Id,
                        Cover = db.Book.Cover,
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
                        IsDeleted = db.Book.IsDeleted
                    },

                    Person = new PersonModel {
                        Id = db.Person.Id,
                        Name = db.Person.Name,
                        Street = db.Person.Street,
                        Complement = db.Person.Complement,
                        District = db.Person.District,
                        FederalState = db.Person.FederalState,
                        Country = db.Person.Country,
                        City = db.Person.City,
                        Number = db.Person.Number,
                        PostalCode = db.Person.PostalCode,
                        Description = db.Person.Description,
                        RegistrationDate = db.Person.RegistrationDate,
                        LastUpdateDate = db.Person.LastUpdateDate,
                        IsDeleted = db.Person.IsDeleted
                    },

                    Date = db.Date,

                    RegistrationDate = db.RegistrationDate,

                    LastUpdateDate = db.LastUpdateDate,

                    Notes = db.Notes

                })
                .OrderBy(b => b.Book.Title)
                .ThenBy(b => b.Book.Id)
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


        public async Task<Response<DonatedBookModel>> GetDonatedBook(Guid id) {

            Response<DonatedBookModel> response = new();

            try {

                if (id != Guid.Empty) {

                    List<DonatedBookModel> donatedBooks = await _context.DonatedBooks
                    .Select(db => new DonatedBookModel {

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

                        Person = new PersonModel {
                            Id = db.Person.Id,
                            Name = db.Person.Name,
                            Street = db.Person.Street,
                            Complement = db.Person.Complement,
                            District = db.Person.District,
                            FederalState = db.Person.FederalState,
                            Country = db.Person.Country,
                            City = db.Person.City,
                            Number = db.Person.Number,
                            PostalCode = db.Person.PostalCode,
                            Description = db.Person.Description,
                            RegistrationDate = db.Person.RegistrationDate,
                            LastUpdateDate = db.Person.LastUpdateDate,
                            IsDeleted = db.Person.IsDeleted
                        },

                        Date = db.Date,

                        RegistrationDate = db.RegistrationDate,

                        LastUpdateDate = db.LastUpdateDate,

                        Notes = db.Notes

                    })
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

                var isBorrowedBookResp = await _collectionService.IsBorrowedBook(donatedBook.Book.Id);

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


    }


}

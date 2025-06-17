using Library.Data;
using Library.Db.Models;
using Microsoft.EntityFrameworkCore;

namespace Library.Services.Collection {


    public class LoanService : ILoanService {


        private readonly ApplicationDbContext _context;

        private readonly ICollectionService _collectionService;


        public LoanService(ApplicationDbContext context, ICollectionService collectionService) {
            _context = context;
            _collectionService = collectionService;
        }


        public async Task<Response<List<LoanModel>>> GetLoans() {

            Response<List<LoanModel>> response = new();

            try {

                List<LoanModel> borrowedBooks = await _context.Loans
                .Select(lm => new LoanModel {

                    Id = lm.Id,

                    Book = new BookModel {
                        Id = lm.Book.Id,
                        Cover = lm.Book.Cover,
                        Title = lm.Book.Title,
                        Subtitle = lm.Book.Subtitle,
                        Author = lm.Book.Author,
                        Publisher = lm.Book.Publisher,
                        Isbn = lm.Book.Isbn,
                        Edition = lm.Book.Edition,
                        Volume = lm.Book.Volume,
                        ReleaseYear = lm.Book.ReleaseYear,
                        NumberOfPages = lm.Book.NumberOfPages,
                        AcquisitionDate = lm.Book.AcquisitionDate,
                        Summary = lm.Book.Summary,
                        LastUpdateDate = lm.Book.LastUpdateDate,
                        RegistrationDate = lm.Book.RegistrationDate,
                        IsDeleted = lm.Book.IsDeleted
                    },

                    Person = new PersonModel {
                        Id = lm.Person.Id,
                        Name = lm.Person.Name,
                        Street = lm.Person.Street,
                        Complement = lm.Person.Complement,
                        District = lm.Person.District,
                        FederalState = lm.Person.FederalState,
                        Country = lm.Person.Country,
                        City = lm.Person.City,
                        Number = lm.Person.Number,
                        PostalCode = lm.Person.PostalCode,
                        Description = lm.Person.Description,
                        RegistrationDate = lm.Person.RegistrationDate,
                        LastUpdateDate = lm.Person.LastUpdateDate,
                        IsDeleted = lm.Person.IsDeleted
                    },

                    Notes = lm.Notes,

                    Date = lm.Date,

                    ReturnDate = lm.ReturnDate,

                    RegistrationDate = lm.RegistrationDate,

                    LastUpdateDate = lm.LastUpdateDate,

                    IsDeleted = lm.IsDeleted,

                    IsReturned = lm.IsReturned

                })
                .Where(lm => lm.IsDeleted == false)
                .OrderBy(lm => lm.Date)
                .ThenBy(b => b.Book.Title)
                .AsNoTracking()
                .ToListAsync();

                response.Data = borrowedBooks;

                return response;

            } catch (Exception ex) {

                response.Message = ex.Message;
                response.Successful = false;

                return response;

            }

        }


        public async Task<Response<LoanModel?>> GetLoan(Guid id) {

            Response<LoanModel?> response = new();

            try {

                if (id != Guid.Empty) {

                    List<LoanModel> loans = await _context.Loans
                    .Select(lm => new LoanModel {

                        Id = lm.Id,

                        Book = new BookModel {
                            Id = lm.Book.Id,
                            Title = lm.Book.Title,
                            Subtitle = lm.Book.Subtitle,
                            Author = lm.Book.Author,
                            Publisher = lm.Book.Publisher,
                            Isbn = lm.Book.Isbn,
                            Edition = lm.Book.Edition,
                            Volume = lm.Book.Volume,
                            ReleaseYear = lm.Book.ReleaseYear,
                            NumberOfPages = lm.Book.NumberOfPages,
                            AcquisitionDate = lm.Book.AcquisitionDate,
                            Summary = lm.Book.Summary,
                            LastUpdateDate = lm.Book.LastUpdateDate,
                            RegistrationDate = lm.Book.RegistrationDate,
                            Cover = lm.Book.Cover,
                            IsDeleted = lm.Book.IsDeleted
                        },

                        Person = new PersonModel {
                            Id = lm.Person.Id,
                            Name = lm.Person.Name,
                            Street = lm.Person.Street,
                            Complement = lm.Person.Complement,
                            District = lm.Person.District,
                            FederalState = lm.Person.FederalState,
                            Country = lm.Person.Country,
                            City = lm.Person.City,
                            Number = lm.Person.Number,
                            PostalCode = lm.Person.PostalCode,
                            Description = lm.Person.Description,
                            RegistrationDate = lm.Person.RegistrationDate,
                            LastUpdateDate = lm.Person.LastUpdateDate,
                            IsDeleted = lm.Person.IsDeleted
                        },

                        Date = lm.Date,

                        ReturnDate = lm.ReturnDate,

                        RegistrationDate = lm.RegistrationDate,

                        LastUpdateDate = lm.LastUpdateDate,

                        Notes = lm.Notes,

                        IsDeleted = lm.IsDeleted,

                        IsReturned = lm.IsReturned

                    })
                    .Where(db => db.Id == id)
                    .AsNoTracking()
                    .ToListAsync();

                    response.Data = loans.First();

                    return response;

                } else {

                    throw new Exception("Identificador do empréstimo inválido.");

                }

            } catch (Exception ex) {

                response.Message = ex.Message;
                response.Successful = false;
                response.Data = null;

                return response;

            }

        }


        public async Task<Response<LoanModel>> RegisterLoan(LoanModel loan) {

            Response<LoanModel> response = new();

            try {

                var isBorrowedBookResp = await _collectionService.IsBorrowedBook(loan.Book.Id);

                if (!isBorrowedBookResp.Successful) throw new Exception(isBorrowedBookResp.Message);

                bool isBorrowed = isBorrowedBookResp.Data;

                if (!isBorrowed) {

                    _context.Attach(loan.Book);
                    _context.Attach(loan.Person);

                    loan.Id = Guid.NewGuid();
                    loan.RegistrationDate = DateTime.Now;
                    loan.LastUpdateDate = loan.RegistrationDate;

                    _context.Loans.Add(loan);

                    await _context.SaveChangesAsync();

                    response.Message = "Livro cadastrado com sucesso!";
                    response.Data = loan;

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


        public async Task<Response<LoanModel>> EditLoan(LoanModel loan) {

            Response<LoanModel> response = new();

            try {

                _context.Attach(loan);

                loan.LastUpdateDate = DateTime.Now;

                _context.Entry(loan).State = EntityState.Modified;

                _context.Loans.Update(loan);

                await _context.SaveChangesAsync();

                response.Message = "Livro alterado com sucesso!";
                response.Data = loan;

                return response;

            } catch (Exception ex) {

                response.Message = ex.Message;
                response.Successful = false;

                return response;

            }

        }


        public async Task<Response<LoanModel>> DeleteLoan(Guid id) {

            Response<LoanModel> response = new();

            try {

                var loanResp = await GetLoan(id);

                LoanModel? loan = loanResp.Data;

                if (loan == null) throw new Exception(loanResp.Message);

                loan.LastUpdateDate = DateTime.Now;
                loan.IsDeleted = true;
                loan.IsReturned = true;

                _context.Entry(loan).State = EntityState.Modified;

                _context.Loans.Update(loan);

                await _context.SaveChangesAsync();

                response.Message = "Livro excluído com sucesso!";
                response.Data = loan;

                return response;

            } catch (Exception ex) {

                response.Message = ex.Message;
                response.Successful = false;

                return response;

            }

        }


        public async Task<Response<LoanModel>> ReturnLoan(LoanModel loan) {

            Response<LoanModel> response = new();

            try {

                _context.Attach(loan);

                loan.LastUpdateDate = DateTime.Now;
                loan.IsReturned = true;

                _context.Entry(loan).State = EntityState.Modified;

                _context.Loans.Update(loan);

                await _context.SaveChangesAsync();

                response.Message = "Livro devolvido com sucesso!";
                response.Data = loan;

                return response;

            } catch (Exception ex) {

                response.Message = ex.Message;
                response.Successful = false;

                return response;

            }

        }


        public async Task<Response<LoanModel>> CancelReturn(Guid id) {

            Response<LoanModel> response = new();

            try {

                var isBorrowedBookResp = await _collectionService.IsBorrowedBook(id);

                if (!isBorrowedBookResp.Successful) throw new Exception(isBorrowedBookResp.Message);

                bool isBorrowed = isBorrowedBookResp.Data;

                if (!isBorrowed) {

                    var loanResp = await GetLoan(id);

                    LoanModel? loan = loanResp.Data;

                    if (loan == null) throw new Exception(loanResp.Message);

                    loan.LastUpdateDate = DateTime.Now;
                    loan.IsReturned = false;

                    _context.Entry(loan).State = EntityState.Modified;

                    _context.Loans.Update(loan);

                    await _context.SaveChangesAsync();

                    response.Message = "Devolução cancelada com sucesso!";
                    response.Data = loan;

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


    }


}

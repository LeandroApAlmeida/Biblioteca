using Library.Data;
using Library.Db.Models;
using Microsoft.EntityFrameworkCore;

namespace Library.Services.Collection {


    public class LoanService : ILoanService {


        private readonly ApplicationDbContext _context;


        public LoanService(ApplicationDbContext context) {
            _context = context;
        }


        public async Task<Response<List<LoanModel>>> GetLoans() {

            Response<List<LoanModel>> response = new();

            try {

                List<LoanModel> borrowedBooks = await _context.Loans
                .Include(l => l.Person)
                .Include(l => l.Book)
                .Where(l => l.IsDeleted == false)
                .OrderBy(l => l.Date)
                .ThenBy(l => l.Book.Title)
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


        public async Task<Response<List<Guid>>> GetBorrowedBooksIds() {

            Response<List<Guid>> response = new();

            try {

                List<Guid> booksIds = await _context.Loans
                .Where(l => l.IsReturned == false && l.IsDeleted == false)
                .Select(l => l.Book.Id).ToListAsync();

                response.Data = booksIds;

                return response;

            } catch (Exception ex) {

                response.Data = [];
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
                    .Include(l => l.Person)
                    .Include(l => l.Book)
                    .ThenInclude(l => l.Cover)
                    .Where(l => l.Id == id)
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

                var isBorrowedBookResp = await IsBorrowedBook(loan.Book.Id);

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

                var isBorrowedBookResp = await IsBorrowedBook(id);

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


        public async Task<Response<bool>> IsBorrowedBook(Guid id) {

            Response<bool> response = new();

            try {

                if (id != Guid.Empty) {

                    var loans = await _context.Loans
                    .Select(l => new { l.IsReturned, l.IsDeleted, l.Book.Id })
                    .Where(l => l.IsDeleted == false && l.IsReturned == false)
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


    }


}

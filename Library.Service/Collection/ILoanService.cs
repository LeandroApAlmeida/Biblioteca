using Library.Db.Models;

namespace Library.Services.Collection {


    public interface ILoanService {

        public Task<Response<List<LoanModel>>> GetLoans();

        public Task<Response<LoanModel?>> GetLoan(Guid id);

        public Task<Response<LoanModel>> RegisterLoan(LoanModel loan);

        public Task<Response<LoanModel>> EditLoan(LoanModel loan);

        public Task<Response<LoanModel>> DeleteLoan(Guid id);

        public Task<Response<LoanModel>> ReturnLoan(LoanModel loan);

        public Task<Response<LoanModel>> CancelReturn(Guid id);

    }


}

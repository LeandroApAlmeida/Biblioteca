using Library.Models;

namespace Library.Services.LoanService {


    public interface ILoanService {

        public Task<Response<List<LoanModel>>> GetLoans();

        public Task<Response<LoanModel?>> GetLoan(Guid id);

        public Task<Response<LoanModel>> RegisterLoan(LoanModel loan);

        public Task<Response<LoanModel>> EditLoan(LoanModel loan);

        public Task<Response<LoanModel>> DeleteLoan(LoanModel loan);

        public Task<Response<LoanModel>> ReturnLoan(LoanModel loan);

        public Task<Response<LoanModel>> CancelReturn(LoanModel loan);

    }


}

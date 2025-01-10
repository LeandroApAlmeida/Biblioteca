using Library.Models;

namespace Library.Services.LoanService {


    public interface ILoanService {

        public Task<ResponseModel<List<LoanModel>>> GetLoans();

        public Task<ResponseModel<LoanModel?>> GetLoan(Guid id);

        public Task<ResponseModel<LoanModel>> RegisterLoan(LoanModel loan);

        public Task<ResponseModel<LoanModel>> EditLoan(LoanModel loan);

        public Task<ResponseModel<LoanModel>> DeleteLoan(LoanModel loan);

        public Task<ResponseModel<LoanModel>> ReturnLoan(LoanModel loan);

        public Task<ResponseModel<LoanModel>> CancelReturn(LoanModel loan);

    }


}

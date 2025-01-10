using Library.Models;

namespace Library.Services.DonationService {


    public interface IDonationService {

        public Task<ResponseModel<List<DonatedBookModel>>> GetDonatedBooks();

        public Task<ResponseModel<DonatedBookModel>> GetDonatedBook(Guid id);

        public Task<ResponseModel<DonatedBookModel>> RegisterDonatedBook(DonatedBookModel donatedBook);

        public Task<ResponseModel<DonatedBookModel>> EditDonatedBook(DonatedBookModel donatedBook);

        public Task<ResponseModel<DonatedBookModel>> DeleteDonatedBook(DonatedBookModel donatedBook);

    }


}
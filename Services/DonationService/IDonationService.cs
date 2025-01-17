using Library.Models;

namespace Library.Services.DonationService {


    public interface IDonationService {

        public Task<Response<List<DonatedBookModel>>> GetDonatedBooks();

        public Task<Response<DonatedBookModel>> GetDonatedBook(Guid id);

        public Task<Response<DonatedBookModel>> RegisterDonatedBook(DonatedBookModel donatedBook);

        public Task<Response<DonatedBookModel>> EditDonatedBook(DonatedBookModel donatedBook);

        public Task<Response<DonatedBookModel>> DeleteDonatedBook(DonatedBookModel donatedBook);

    }


}
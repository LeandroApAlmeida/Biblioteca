using Library.Db.Models;

namespace Library.Services.Collection {


    public interface IDonationService {

        public Task<Response<List<DonatedBookModel>>> GetDonatedBooks();

        public Task<Response<List<Guid>>> GetDonatedBooksIds();

        public Task<Response<DonatedBookModel>> GetDonatedBook(Guid id);

        public Task<Response<DonatedBookModel>> RegisterDonatedBook(DonatedBookModel donatedBook);

        public Task<Response<DonatedBookModel>> EditDonatedBook(DonatedBookModel donatedBook);

        public Task<Response<DonatedBookModel>> DeleteDonatedBook(Guid id);

        public Task<Response<bool>> IsDonatedBook(Guid id);

    }


}
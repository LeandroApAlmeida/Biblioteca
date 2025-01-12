using Library.Models;

namespace Library.Services.CollectionService {


    public interface ICollectionService {

        public Task<ResponseModel<List<BookModel>>> GetAvailableBooks();

        public Task<ResponseModel<bool>> IsBorrowedBook(Guid id);

        public Task<ResponseModel<List<Guid>>> GetBooksIds();

        public Task<ResponseModel<List<Guid>>> BorrowedBooksIds();

        public Task<ResponseModel<List<Guid>>> DiscardedBooksIds();

        public Task<ResponseModel<List<Guid>>> DonatedBooksIds();

    }


}
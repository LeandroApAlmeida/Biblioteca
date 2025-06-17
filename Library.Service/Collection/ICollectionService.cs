using Library.Db.Models;

namespace Library.Services.Collection {


    public interface ICollectionService {

        public Task<Response<List<BookModel>>> GetAvailableBooks();

        public Task<Response<List<BookModel>>> GetCollectionBooks();

        public Task<Response<List<BookModel>>> GetDeletedBooks();

        public Task<Response<bool>> IsBorrowedBook(Guid id);

        public Task<Response<bool>> IsDiscardedBook(Guid id);

        public Task<Response<bool>> IsDonatedBook(Guid id);

        public Task<Response<List<Guid>>> GetBooksIds();

        public Task<Response<List<Guid>>> BorrowedBooksIds();

        public Task<Response<List<Guid>>> DiscardedBooksIds();

        public Task<Response<List<Guid>>> DonatedBooksIds();

    }


}
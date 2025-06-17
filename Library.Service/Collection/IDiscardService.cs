using Library.Db.Models;

namespace Library.Services.Collection {


    public interface IDiscardService {

        public Task<Response<List<DiscardedBookModel>>> GetDiscardedBooks();

        public Task<Response<DiscardedBookModel>> GetDiscardedBook(Guid id);

        public Task<Response<DiscardedBookModel>> RegisterDiscardedBook(DiscardedBookModel discardedBook);

        public Task<Response<DiscardedBookModel>> EditDiscardedBook(DiscardedBookModel discardedBook);

        public Task<Response<DiscardedBookModel>> DeleteDiscardedBook(Guid id);

    }


}

using Library.Models;

namespace Library.Services.DiscardService {


    public interface IDiscardService {

        public Task<ResponseModel<List<DiscardedBookModel>>> GetDiscardedBooks();

        public Task<ResponseModel<DiscardedBookModel>> GetDiscardedBook(Guid id);

        public Task<ResponseModel<DiscardedBookModel>> RegisterDiscardedBook(DiscardedBookModel discardedBook);

        public Task<ResponseModel<DiscardedBookModel>> EditDiscardedBook(DiscardedBookModel discardedBook);

        public Task<ResponseModel<DiscardedBookModel>> DeleteDiscardedBook(DiscardedBookModel discardedBook);

    }


}

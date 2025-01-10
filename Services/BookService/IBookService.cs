using Library.Models;

namespace Library.Services.BookService {


    public interface IBookService {

        public Task<ResponseModel<List<BookModel>>> GetBooks();

        public Task<ResponseModel<BookModel>> GetBook(Guid id);

        public Task<ResponseModel<Guid>> NextBookId(Guid id);

        public Task<ResponseModel<Guid>> PreviousBookId(Guid id);

        public Task<ResponseModel<Guid>> FirstBookId();

        public Task<ResponseModel<Guid>> LastBookId();

        public Task<ResponseModel<BookModel>> RegisterBook(BookModel book);

        public Task<ResponseModel<BookModel>> EditBook(BookModel book);

        public Task<ResponseModel<BookModel>> DeleteBook(BookModel book);

    }


}
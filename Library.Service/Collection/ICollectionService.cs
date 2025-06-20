using Library.Db.Models;

namespace Library.Services.Collection {


    public interface ICollectionService {

        public Task<Response<List<BookModel>>> GetAvailableBooks();

        public Task<Response<List<BookModel>>> GetCollectionBooks();

        public Task<Response<List<BookModel>>> GetCollectionBooksWithThumbnails();

    }


}
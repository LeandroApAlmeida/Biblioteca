﻿using Library.Models;

namespace Library.Services.CollectionService {


    public interface ICollectionService {

        public Task<Response<List<BookModel>>> GetAvailableBooks();

        public Task<Response<List<BookModel>>> GetCollectionBooks();

        public Task<Response<List<BookModel>>> GetDeletedBooks();

        public Task<Response<Boolean>> IsBorrowedBook(Guid id);

        public Task<Response<Boolean>> IsDiscardedBook(Guid id);

        public Task<Response<Boolean>> IsDonatedBook(Guid id);

        public Task<Response<List<Guid>>> GetBooksIds();

        public Task<Response<List<Guid>>> BorrowedBooksIds();

        public Task<Response<List<Guid>>> DiscardedBooksIds();

        public Task<Response<List<Guid>>> DonatedBooksIds();

    }


}
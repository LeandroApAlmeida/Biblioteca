using Library.Models;

namespace Library.Services.ReportService {
    

    public interface IHtmlReportService {

        public string BookDetailed(BookModel book);

        public string BooksInTheCollection(IEnumerable<BookModel> booksList);

        public string RegisteredBooks(IEnumerable<BookModel> booksList);

        public string BorrowedBooks(IEnumerable<LoanModel> loanList);

        public string DiscardedBooks(IEnumerable<DiscardedBookModel> discardedBooksList);

        public string DonatedBooks(IEnumerable<DonatedBookModel> donatedBooksList);

        public string BookDetailedWithTitle(BookModel book);

        public string BooksInTheCollectionWithTitle(IEnumerable<BookModel> booksList);

        public string RegisteredBooksWithTitle(IEnumerable<BookModel> booksList);

        public string BorrowedBooksWithTitle(IEnumerable<LoanModel> loanList);

        public string DiscardedBooksWithTitle(IEnumerable<DiscardedBookModel> discardedBooksList);

        public string DonatedBooksWithTitle(IEnumerable<DonatedBookModel> donatedBooksList);

    }


}

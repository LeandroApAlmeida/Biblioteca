using Library.Models;

namespace Library.Services.ReportService {
    

    public interface IHtmlReportService {

        public string BookDetailed(BookModel book);

        public string BooksInTheCollection(IEnumerable<BookModel> booksList);

        public string RegisteredBooks(IEnumerable<BookModel> booksList);

        public string BorrowedBooks(IEnumerable<LoanModel> loanList);

        public string DiscardedBooks(IEnumerable<DiscardedBookModel> discardedBooksList);

        public string DonatedBooks(IEnumerable<DonatedBookModel> donatedBooksList);

    }


}

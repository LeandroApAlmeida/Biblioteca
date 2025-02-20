using Library.Models;

namespace Library.Services.ReportService {
    

    public interface IHtmlReportService {

        public string BookDetailed(BookModel book, bool renderTitle);

        public string BooksInTheCollection(IEnumerable<BookModel> booksList, bool renderTitle);

        public string RegisteredBooks(IEnumerable<BookModel> booksList, bool renderTitle);

        public string BorrowedBooks(IEnumerable<LoanModel> loanList, bool renderTitle);

        public string DiscardedBooks(IEnumerable<DiscardedBookModel> discardedBooksList, bool renderTitle);

        public string DonatedBooks(IEnumerable<DonatedBookModel> donatedBooksList, bool renderTitle);

    }


}

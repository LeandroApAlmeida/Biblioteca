using Library.Models;

namespace Library.Services.ReportService {
    
    
    public interface IPdfReportService {

        public byte[] BookDetailed(BookModel book);

        public byte[] BooksInTheCollection(IEnumerable<BookModel> booksList);

        public byte[] RegisteredBooks(IEnumerable<BookModel> booksList);

        public byte[] DiscardedBooks(IEnumerable<DiscardedBookModel> discardedBooksList);

        public byte[] DonatedBooks(IEnumerable<DonatedBookModel> donatedBooksList);

        public byte[] BorrowedBooks(IEnumerable<LoanModel> loanList);

    }


}

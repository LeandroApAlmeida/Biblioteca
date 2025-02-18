using Library.Models;

namespace Library.Services.ReportService {


    public class ReportService : IReportService {


        byte[] IReportService.BookDetailed(BookModel book) {
            throw new NotImplementedException();
        }

        byte[] IReportService.BooksInTheCollection(IEnumerable<BookModel> booksList) {
            throw new NotImplementedException();
        }

        byte[] IReportService.BorrowedBooks(IEnumerable<LoanModel> loanList) {
            throw new NotImplementedException();
        }

        byte[] IReportService.DiscardedBooks(IEnumerable<DiscardedBookModel> discardedBooksList) {
            throw new NotImplementedException();
        }

        byte[] IReportService.DonatedBooks(IEnumerable<DonatedBookModel> donatedBooksList) {
            throw new NotImplementedException();
        }


    }


}

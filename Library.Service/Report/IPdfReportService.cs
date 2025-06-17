namespace Library.Services.Report {
    
    
    public interface IPdfReportService {

        public byte[] BookDetailed(Guid id);

        public byte[] BooksInTheCollection();

        public byte[] RegisteredBooks();

        public byte[] DiscardedBooks();

        public byte[] DonatedBooks();

        public byte[] BorrowedBooks();

    }


}

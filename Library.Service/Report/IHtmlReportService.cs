namespace Library.Services.Report {
    

    public interface IHtmlReportService {

        public string BookDetailed(Guid id, bool renderTitle);

        public string BooksInTheCollection(bool renderTitle);

        public string RegisteredBooks(bool renderTitle);

        public string DiscardedBooks(bool renderTitle);

        public string DonatedBooks(bool renderTitle);

        public string BorrowedBooks(bool renderTitle);

    }


}

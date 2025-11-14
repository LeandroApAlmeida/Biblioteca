namespace Library.Services.Report {


    /// <summary>
    /// Interface que define um gerador de relatórios em formato PDF.
    /// </summary>
    public interface IPdfReportService {


        /// <summary>
        /// Gerar o relatório "Detalhes do Livro".
        /// </summary>
        /// <param name="id">Identificador chave primária do livro.</param>
        /// <returns>Stream do relatório.</returns>
        public byte[] BookDetailed(Guid id);


        /// <summary>
        /// Gerar o relatório "Livros no Acervo".
        /// </summary>
        /// <returns>Stream do relatório.</returns>
        public byte[] BooksInTheCollection();


        /// <summary>
        /// Gerar o relatório "Livros Cadastrados".
        /// </summary>
        /// <returns>Stream do relatório.</returns>
        public byte[] RegisteredBooks();


        /// <summary>
        /// Gerar o relatório "Livros Descartados".
        /// </summary>
        /// <returns>Stream do relatório.</returns>
        public byte[] DiscardedBooks();


        /// <summary>
        /// Gerar o relatório "Livros Doados".
        /// </summary>
        /// <returns>Stream do relatório.</returns>
        public byte[] DonatedBooks();


        /// <summary>
        /// Gerar o relatório "Livros Emprestados".
        /// </summary>
        /// <returns>Stream do relatório.</returns>
        public byte[] BorrowedBooks();


    }


}

namespace Library.Services.Report {
    

    /// <summary>
    /// Interface que define um gerador de relatórios em formato HTML.
    /// </summary>
    public interface IHtmlReportService {


        /// <summary>
        /// Gerar o relatório "Detalhes do Livro".
        /// </summary>
        /// <param name="id">Identificador chave primária do livro.</param>
        /// <param name="renderTitle">Renderizar o título no cabeçalho do HTML.</param>
        /// <returns>Script HTML do relatório.</returns>
        public string BookDetailed(Guid id, bool renderTitle);


        /// <summary>
        /// Gerar o relatório "Livros no Acervo".
        /// </summary>
        /// <param name="renderTitle">Renderizar o título no cabeçalho do HTML.</param>
        /// <returns>Script HTML do relatório.</returns>
        public string BooksInTheCollection(bool renderTitle);


        /// <summary>
        /// Gerar o relatório "Livros Cadastrados".
        /// </summary>
        /// <param name="renderTitle">Renderizar o título no cabeçalho do HTML.</param>
        /// <returns>Script HTML do relatório.</returns>
        public string RegisteredBooks(bool renderTitle);


        /// <summary>
        /// Gerar o relatório "Livros Descartados".
        /// </summary>
        /// <param name="renderTitle">Renderizar o título no cabeçalho do HTML.</param>
        /// <returns>Script HTML do relatório.</returns>
        public string DiscardedBooks(bool renderTitle);


        /// <summary>
        /// Gerar o relatório "Livros Doados".
        /// </summary>
        /// <param name="renderTitle">Renderizar o título no cabeçalho do HTML.</param>
        /// <returns>Script HTML do relatório.</returns>
        public string DonatedBooks(bool renderTitle);


        /// <summary>
        /// Gerar o relatório "Livros Emprestados".
        /// </summary>
        /// <param name="renderTitle">Renderizar o título no cabeçalho do HTML.</param>
        /// <returns>Script HTML do relatório.</returns>
        public string BorrowedBooks(bool renderTitle);


    }


}

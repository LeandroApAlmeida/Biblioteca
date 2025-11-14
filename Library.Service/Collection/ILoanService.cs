using Library.Db.Models;

namespace Library.Services.Collection {


    /// <summary>
    /// Interface que define um serviço para manutenção de livros emprestados.
    /// </summary>
    public interface ILoanService {


        /// <summary>
        /// Obter a lista com os livros que foram emprestados, sem as imagens de capa.
        /// </summary>
        /// <returns>Lista com os livros que foram emprestados, sem as imagens de capa.</returns>
        public Task<Response<List<LoanModel>>> GetLoans();


        /// <summary>
        /// Obter a lista com os identificadores chave primária dos livros que foram emprestados.
        /// </summary>
        /// <returns>Lista com os identificadores chave primária dos livros que foram emprestados.</returns>
        public Task<Response<List<Guid>>> GetBorrowedBooksIds();


        /// <summary>
        /// Obter o livro emprestado com o identificador chave primária passado como parâmetro.
        /// </summary>
        /// <param name="id">Identificador chave primária do livro</param>
        /// <returns>Livro associado ao identificador chave primária.</returns>
        public Task<Response<LoanModel?>> GetLoan(Guid id);


        /// <summary>
        /// Emprestar um livro do acervo. Caso o livro esteja emprestado, é necessário registrar a
        /// devolução do mesmo antes.
        /// </summary>
        /// <param name="loan">Registro de empréstimo.</param>
        /// <returns>Registro de empréstimo inserido.</returns>
        public Task<Response<LoanModel>> RegisterLoan(LoanModel loan);


        /// <summary>
        /// Editar um registro de empréstimo.
        /// </summary>
        /// <param name="loan">Registro de empréstimo a ser alterado.</param>
        /// <returns>Registro de empréstimo alterado.</returns>
        public Task<Response<LoanModel>> EditLoan(LoanModel loan);


        /// <summary>
        /// Excluir um registro de empréstimo.
        /// </summary>
        /// <param name="id">Identificador chave primária do registro de empréstimo
        /// a ser excluído.</param>
        /// <returns>Registro de empréstimo excluído.</returns>
        public Task<Response<LoanModel>> DeleteLoan(Guid id);


        /// <summary>
        /// Devolver um empréstimo.
        /// </summary>
        /// <param name="loan">Registro de empréstimo a ser devolvido.</param>
        /// <returns>Registro de empréstimo devolvido.</returns>
        public Task<Response<LoanModel>> ReturnLoan(LoanModel loan);


        /// <summary>
        /// Cancelar a devolução de um empréstimo.
        /// </summary>
        /// <param name="id">Identificador chave primária do registro de empréstimo.</param>
        /// <returns>Registro de empréstimo cancelado.</returns>
        public Task<Response<LoanModel>> CancelReturn(Guid id);


        /// <summary>
        /// Verificar se o livro com o identificador chave primária passado como parâmetro foi
        /// emprestado.
        /// </summary>
        /// <param name="id">Identificador chave primária do livro.</param>
        /// <returns>True, o livro foi emprestado. False, o livro não foi emprestado.</returns>
        public Task<Response<bool>> IsBorrowedBook(Guid id);


    }


}

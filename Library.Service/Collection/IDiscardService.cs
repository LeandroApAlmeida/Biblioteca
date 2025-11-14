using Library.Db.Models;

namespace Library.Services.Collection {


    /// <summary>
    /// Interface que define um serviço para manutenção de livros descartados.
    /// </summary>
    public interface IDiscardService {


        /// <summary>
        /// Obter a lista com os livros que foram descartados, sem as imagens de capa.
        /// </summary>
        /// <returns>Lista com os livros que foram descartados, sem as imagens de capa.</returns>
        public Task<Response<List<DiscardedBookModel>>> GetDiscardedBooks();


        /// <summary>
        /// Obter a lista com os identificadores chave primária dos livros que foram descartados.
        /// </summary>
        /// <returns>Lista com os identificadores chave primária dos livros que foram descartados.</returns>
        public Task<Response<List<Guid>>> GetDiscardedBooksIds();


        /// <summary>
        /// Obter o livro descartado com o identificador chave primária passado como parâmetro.
        /// </summary>
        /// <param name="id">Identificador chave primária do livro</param>
        /// <returns>Livro associado ao identificador chave primária.</returns>
        public Task<Response<DiscardedBookModel>> GetDiscardedBook(Guid id);


        /// <summary>
        /// Descartar um livro do acervo. Caso o livro esteja emprestado, é necessário registrar a
        /// devolução do mesmo antes.
        /// </summary>
        /// <param name="discardedBook">Livro a ser descartado.</param>
        /// <returns>Livro descartado.</returns>
        public Task<Response<DiscardedBookModel>> RegisterDiscardedBook(DiscardedBookModel discardedBook);


        /// <summary>
        /// Editar um livro descartado.
        /// </summary>
        /// <param name="discardedBook">Livro a ser editado.</param>
        /// <returns>Livro alterado.</returns>
        public Task<Response<DiscardedBookModel>> EditDiscardedBook(DiscardedBookModel discardedBook);


        /// <summary>
        /// Excluir um livro descartado.
        /// </summary>
        /// <param name="id">Identificador chave primária do livro a ser excluído.</param>
        /// <returns>Livro excluído.</returns>
        public Task<Response<DiscardedBookModel>> DeleteDiscardedBook(Guid id);


        /// <summary>
        /// Verificar se o livro com o identificador chave primária passado como parâmetro foi
        /// descartado.
        /// </summary>
        /// <param name="id">Identificador chave primária do livro.</param>
        /// <returns>True, o livro foi descartado. False, o livro não foi descartado.</returns>
        public Task<Response<bool>> IsDiscardedBook(Guid id);


    }


}

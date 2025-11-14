using Library.Db.Models;

namespace Library.Services.Collection {


    /// <summary>
    /// Interface que define um serviço para a manutenção de livros do acervo.
    /// </summary>
    public interface IBookService {


        /// <summary>
        /// Obter a lista com os livros cadastrados no banco de dados, sem imagens de capa.
        /// </summary>
        /// <returns>Lista com os livros cadastrados no banco de dados, sem imagens de capa.</returns>
        public Task<Response<List<BookModel>>> GetBooks();


        /// <summary>
        /// Obter a lista com os livros cadastrados no banco de dados, com as imagens de miniatura.
        /// </summary>
        /// <returns>Lista com os livros cadastrados no banco de dados, com as imagens de miniatura.</returns>
        public Task<Response<List<BookModel>>> GetBooksWithThumbnails();


        /// <summary>
        /// Obter a lista com os IDs dos livros cadastrados no banco de dados.
        /// </summary>
        /// <returns>Lista com os IDs dos livros cadastrados no banco de dados.</returns>
        public Task<Response<List<Guid>>> GetBooksIds();


        /// <summary>
        /// Obter a lista dos livros com os cadastros excluídos.
        /// </summary>
        /// <returns>Lista dos livros com os cadastros excluídos.</returns>
        public Task<Response<List<BookModel>>> GetDeletedBooks();


        /// <summary>
        /// Obter o livro pelo identificador chave primária passado como parâmetro.
        /// </summary>
        /// <param name="id">Identificador chave primária do livro.</param>
        /// <returns>Livro associado ao identificador chave primária.</returns>
        public Task<Response<BookModel>> GetBook(Guid id);


        /// <summary>
        /// Obter o próximo livro com relação ao livro com o identificador chave primária
        /// passado.
        /// </summary>
        /// <param name="id">Identificador chave primária do livro de referência.</param>
        /// <returns>Livro adiante ao de referência.</returns>
        public Task<Response<Guid>> NextBookId(Guid id);


        /// <summary>
        /// Obter o livro anterior com relação ao livro com o identificador chave primária
        /// passado.
        /// </summary>
        /// <param name="id">Identificador chave primária do livro de referência.</param>
        /// <returns>Livro anterior ao de referência.</returns>
        public Task<Response<Guid>> PreviousBookId(Guid id);


        /// <summary>
        /// Obter o primeiro livro do cadastro.
        /// </summary>
        /// <returns>Primeiro livro do cadastro.</returns>
        public Task<Response<Guid>> FirstBookId();


        /// <summary>
        /// Obter o último livro do cadastro.
        /// </summary>
        /// <returns>Último livro do cadastro.</returns>
        public Task<Response<Guid>> LastBookId();


        /// <summary>
        /// Cadastrar um novo livro.
        /// </summary>
        /// <param name="book">Livro a ser cadastrado.</param>
        /// <returns>Livro cadastrado.</returns>
        public Task<Response<BookModel>> RegisterBook(BookModel book);


        /// <summary>
        /// Alterar o cadastro de um livro.
        /// </summary>
        /// <param name="book">Livro a ser alterado.</param>
        /// <returns>Livro alterado.</returns>
        public Task<Response<BookModel>> EditBook(BookModel book);


        /// <summary>
        /// Excluir um livro (não faz a remoção do registro no banco de dados). Se o livro
        /// estiver emprestado, é necessário fazer a devolução antes.
        /// </summary>
        /// <param name="id">Identificador chave primária do livro a ser excluído.</param>
        /// <returns>Livro excluído.</returns>
        public Task<Response<BookModel>> DeleteBook(Guid book);


        /// <summary>
        /// Retornar um livro excluído.
        /// </summary>
        /// <param name="id">Identificador chave primária do livro a ser retornado.</param>
        /// <returns>Livro retornado.</returns>
        public Task<Response<BookModel>> UndeleteBook(Guid book);


    }


}
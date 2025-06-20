using Library.Db.Models;

namespace Library.Services.Collection {


    /// <summary>
    /// Interface que define um service para a manutenção de livros do acervo.
    /// </summary>
    public interface IBookService {


        /// <summary>
        /// Obter a lista com todos os livros no acervo. Neste método, a capa do livro,
        /// que está no formato string base64, não é retornada, recebendo um "" no lugar,
        /// para otimização da consulta. A lista não contém livros excluídos, livros 
        /// descartados e nem livros doados, pois estes não estão mais disponíveis no acervo.
        /// </summary>
        /// <returns>Instância de ResponseModel contendo a Lista com todos os livros que 
        /// estão no acervo.</returns>
        public Task<Response<List<BookModel>>> GetBooks();


        public Task<Response<List<BookModel>>> GetBooksWithThumbnails();


        public Task<Response<List<Guid>>> GetBooksIds();


        public Task<Response<List<BookModel>>> GetDeletedBooks();


        /// <summary>
        /// Obter o livro com o Identificador passado no parâmetro.
        /// </summary>
        /// <param name="id">Identificador do livro</param>
        /// <returns>Instância de ResponseModel contendo o livro com o Identificador passado 
        /// no parâmetro</returns>
        public Task<Response<BookModel>> GetBook(Guid id);


        /// <summary>
        /// Obter o Identificador do livro seguinte ao com o Identificador
        /// passado como parâmetro.
        /// </summary>
        /// <param name="id">Identificador do livro atual</param>
        /// <returns>Instância de ResponseModel contendo o identificador do livro seguinte
        /// na lista</returns>
        public Task<Response<Guid>> NextBookId(Guid id);


        /// <summary>
        /// Obter o Identificador do livro anterior ao com o Identificador
        /// passado como parâmetro.
        /// </summary>
        /// <param name="id">Identificador do livro atual</param>
        /// <returns>Instância de ResponseModel contendo o identificador do livro anterior
        /// na lista</returns>
        public Task<Response<Guid>> PreviousBookId(Guid id);


        /// <summary>
        /// Obter o Identificador do primeiro livro da lista.
        /// </summary>
        /// <returns>Instância de ResponseModel contendo o identificador do primeiro livro
        /// da lista</returns>
        public Task<Response<Guid>> FirstBookId();


        /// <summary>
        /// Obter o Identificador do último livro da lista.
        /// </summary>
        /// <returns>Instância de ResponseModel contendo o identificador do último livro
        /// da lista</returns>
        public Task<Response<Guid>> LastBookId();


        /// <summary>
        /// Cadastrar um novo livro no acervo.
        /// </summary>
        /// <param name="book">Livro a ser cadastrado.</param>
        /// <returns>Instância de ResponseModel contendo o livro cadastrado</returns>
        public Task<Response<BookModel>> RegisterBook(BookModel book);


        /// <summary>
        /// Alterar o cadastro de um livro com o Identificador passado como
        /// parâmetro.
        /// </summary>
        /// <param name="book">Identificador do livro alterado</param>
        /// <returns>Instância de ResponseModel contendo o livro alterado</returns>
        public Task<Response<BookModel>> EditBook(BookModel book);


        /// <summary>
        /// Excluir o livro com o identificador passado como parâmetro. A exclusão não
        /// se dá pela remoção do registro no banco de dados, e sim pela atribuição de
        /// "true" ao campo "IsDeleted".
        /// </summary>
        /// <param name="book">Identificador do livro a ser excluído</param>
        /// <returns>Instância de ResponseModel contendo o livro excluído</returns>
        public Task<Response<BookModel>> DeleteBook(Guid book);


        public Task<Response<BookModel>> UndeleteBook(Guid book);


    }


}
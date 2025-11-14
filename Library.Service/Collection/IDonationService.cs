using Library.Db.Models;

namespace Library.Services.Collection {


    /// <summary>
    /// Interface que define um serviço para manutenção de livros doados.
    /// </summary>
    public interface IDonationService {


        /// <summary>
        /// Obter a lista com os livros que foram doados, sem as imagens de capa.
        /// </summary>
        /// <returns>Lista com os livros que foram doados, sem as imagens de capa.</returns>
        public Task<Response<List<DonatedBookModel>>> GetDonatedBooks();


        /// <summary>
        /// Obter a lista com os identificadores chave primária dos livros que foram doados.
        /// </summary>
        /// <returns>Lista com os identificadores chave primária dos livros que foram doados.</returns>
        public Task<Response<List<Guid>>> GetDonatedBooksIds();


        /// <summary>
        /// Obter o livro doado com o identificador chave primária passado como parâmetro.
        /// </summary>
        /// <param name="id">Identificador chave primária do livro</param>
        /// <returns>Livro associado ao identificador chave primária.</returns>
        public Task<Response<DonatedBookModel>> GetDonatedBook(Guid id);


        /// <summary>
        /// Doar um livro do acervo. Caso o livro esteja emprestado, é necessário registrar a
        /// devolução do mesmo antes.
        /// </summary>
        /// <param name="donatedBook">Livro a ser doado.</param>
        /// <returns>Livro doado.</returns>
        public Task<Response<DonatedBookModel>> RegisterDonatedBook(DonatedBookModel donatedBook);


        /// <summary>
        /// Editar um livro doado.
        /// </summary>
        /// <param name="donatedBook">Livro a ser editado.</param>
        /// <returns>Livro alterado</returns>
        public Task<Response<DonatedBookModel>> EditDonatedBook(DonatedBookModel donatedBook);


        /// <summary>
        /// Excluir um livro doado.
        /// </summary>
        /// <param name="id">Identificador chave primária do livro a ser excluído.</param>
        /// <returns>Livro excluído.</returns>
        public Task<Response<DonatedBookModel>> DeleteDonatedBook(Guid id);


        /// <summary>
        /// Verificar se o livro com o identificador chave primária passado como parâmetro foi
        /// doado.
        /// </summary>
        /// <param name="id">Identificador chave primária do livro.</param>
        /// <returns>True, o livro foi doado. False, o livro não foi doado.</returns>
        public Task<Response<bool>> IsDonatedBook(Guid id);

    }


}
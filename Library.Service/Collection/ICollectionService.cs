using Library.Db.Models;

namespace Library.Services.Collection {


    /// <summary>
    /// Interface que define um serviço para a manutenção do acervo.
    /// </summary>
    public interface ICollectionService {


        /// <summary>
        /// Obter a lista com os livros disponíveis no acervo (não emprestados, não doados e 
        /// não descartados), sem as imagens de capa.
        /// </summary>
        /// <returns>Lista com os livros disponíveis no acervo, sem as imagens de capa.</returns>
        public Task<Response<List<BookModel>>> GetAvailableBooks();


        /// <summary>
        /// Obter a lista com todos os livros no acervo (não doados e não descartados), sem as 
        /// imagens de capa.
        /// </summary>
        /// <returns>Lista com todos os livros no acervo, sem as imagens de capa.</returns>
        public Task<Response<List<BookModel>>> GetCollectionBooks();


        /// <summary>
        /// Obter a lista com todos os livros no acervo (não doados e não descartados), com as
        /// miniaturas de capa.
        /// </summary>
        /// <returns>Lista com todos os livros no acervo, com as miniaturas de capa.</returns>
        public Task<Response<List<BookModel>>> GetCollectionBooksWithThumbnails();


    }


}
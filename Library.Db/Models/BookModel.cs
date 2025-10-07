using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;

namespace Library.Db.Models {


    /// <summary>
    /// Entidade que representa um livro ou outro tipo de publicação impressa.
    /// </summary>
    [Table("Book")]
    public class BookModel {

        /// <summary> Identificador chave primária do livro. </summary>
        [Key, DatabaseGenerated(DatabaseGeneratedOption.None)]
        public Guid Id { get; set; }
        
        /// <summary> Capa do livro. </summary>
        [Required]
        public required CoverModel Cover { get; set; }

        /// <summary> Título do livro. </summary>
        [Required(ErrorMessage = "Digite o título do livro")]
        public required string Title { get; set; }

        /// <summary> Subtítulo do livro. </summary>
        public string? Subtitle { get; set; }

        /// <summary> Autor do livro. </summary>
        [Required(ErrorMessage = "Digite o autor do livro")]
        public required string Author { get; set; }

        /// <summary> Editora do livro. </summary>
        [Required(ErrorMessage = "Digite a editora do livro")]
        public required string Publisher { get; set; }

        /// <summary> Número ISBN do livro. </summary>
        [Required(ErrorMessage = "Digite o ISBN do livro")]
        public required string Isbn { get; set; }

        /// <summary> Número da edição do livro. </summary>
        [Required(ErrorMessage = "Digite o número de edição do livro")]
        public required int Edition { get; set; }

        /// <summary> Número do volume do livro. </summary>
        [Required(ErrorMessage = "Digite o número do volume do livro")]
        public required int Volume { get; set; }

        /// <summary> Ano de publicação do livro. </summary>
        [Required(ErrorMessage = "Digite o ano de lançamento do livro")]
        public required int ReleaseYear { get; set; }

        /// <summary> Número de páginas do livro. </summary>
        [Required(ErrorMessage = "Digite o número de páginas do livro")]
        public required int NumberOfPages { get; set; }

        /// <summary> Sinopse do livro. </summary>
        public string? Summary { get; set; }
        
        /// <summary> Data da aquisição do livro. </summary>
        [DataType(DataType.DateTime)]
        [Required(ErrorMessage = "Digite a data de aquisição do livro")]
        public required DateTime AcquisitionDate { get; set; }

        /// <summary> Data de cadastro do livro. </summary>
        [DataType(DataType.DateTime)]
        public required DateTime RegistrationDate { get; set; } = DateTime.Now;

        /// <summary> Data da última atualização de cadastro do livro. </summary>
        [DataType(DataType.DateTime)]
        public required DateTime LastUpdateDate { get; set; } = DateTime.Now;

        /// <summary> Status de livro excluído. </summary>
        public required bool IsDeleted { get; set; } = false;

        /// <summary> Status de livro emprestado. </summary>
        [NotMapped]
        public bool IsBorrowed { get; set; } = false;

        /// <summary> Status de livro doado. </summary>
        [NotMapped]
        public bool IsDonated { get; set; } = false;

        /// <summary> Status de livro descartado. </summary>
        [NotMapped]
        public bool IsDiscarded { get; set; } = false;

    }


}
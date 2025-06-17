using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;

namespace Library.Db.Models {


    [Table("Book")]
    public class BookModel {

        [Key, DatabaseGenerated(DatabaseGeneratedOption.None)]
        public Guid Id { get; set; }
        
        [Required]
        public required CoverModel Cover { get; set; }

        [Required(ErrorMessage = "Digite o título do livro")]
        public required string Title { get; set; }

        public string? Subtitle { get; set; }

        [Required(ErrorMessage = "Digite o autor do livro")]
        public required string Author { get; set; }

        [Required(ErrorMessage = "Digite a editora do livro")]
        public required string Publisher { get; set; }

        [Required(ErrorMessage = "Digite o ISBN do livro")]
        public required string Isbn { get; set; }

        [Required(ErrorMessage = "Digite o número de edição do livro")]
        public required int Edition { get; set; }

        [Required(ErrorMessage = "Digite o número do volume do livro")]
        public required int Volume { get; set; }

        [Required(ErrorMessage = "Digite o ano de lançamento do livro")]
        public required int ReleaseYear { get; set; }

        [Required(ErrorMessage = "Digite o número de páginas do livro")]
        public required int NumberOfPages { get; set; }

        public string? Summary { get; set; }

        [DataType(DataType.DateTime)]
        [Required(ErrorMessage = "Digite a data de aquisição do livro")]
        public required DateTime AcquisitionDate { get; set; }

        [DataType(DataType.DateTime)]
        public required DateTime RegistrationDate { get; set; } = DateTime.Now;

        [DataType(DataType.DateTime)]
        public required DateTime LastUpdateDate { get; set; } = DateTime.Now;

        public required bool IsDeleted { get; set; } = false;

        [NotMapped]
        public bool IsBorrowed { get; set; } = false;

        [NotMapped]
        public bool IsDonated { get; set; } = false;

        [NotMapped]
        public bool IsDiscarded { get; set; } = false;

    }


}
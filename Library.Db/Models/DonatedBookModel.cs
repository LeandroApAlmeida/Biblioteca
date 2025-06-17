using Microsoft.EntityFrameworkCore.Metadata.Internal;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;

namespace Library.Db.Models {


    [Table("DonatedBook")]
    public class DonatedBookModel {

        [Key, DatabaseGenerated(DatabaseGeneratedOption.None)]
        public Guid Id { get; set; }

        [ForeignKey("Id")]
        [Required(ErrorMessage = "Selecione um livro")]
        public required BookModel Book { get; set; }

        [Required(ErrorMessage = "Selecione uma pessoa")]
        public required PersonModel Person { get; set; }

        public string? Notes { get; set; }

        [DataType(DataType.DateTime)]
        [Required(ErrorMessage = "Digite a data de doação do livro")]
        public required DateTime Date { get; set; }

        [DataType(DataType.DateTime)]
        public required DateTime RegistrationDate { get; set; } = DateTime.Now;

        [DataType(DataType.DateTime)]
        public required DateTime LastUpdateDate { get; set; } = DateTime.Now;

    }


}
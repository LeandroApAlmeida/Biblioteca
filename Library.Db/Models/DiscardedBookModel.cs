using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;

namespace Library.Db.Models {


    [Table("DiscardedBook")]
    public class DiscardedBookModel {

        [Key, DatabaseGenerated(DatabaseGeneratedOption.None)]
        public Guid Id { get; set; }

        [ForeignKey("Id")]
        [Required(ErrorMessage = "Selecione um livro")]
        public required BookModel Book { get; set; }

        public string? Reason { get; set; }

        [DataType(DataType.DateTime)]
        [Required(ErrorMessage = "Digite a data de descarte do livro")]
        public required DateTime Date { get; set; }

        [DataType(DataType.DateTime)]
        public required DateTime RegistrationDate { get; set; } = DateTime.Now;

        [DataType(DataType.DateTime)]
        public required DateTime LastUpdateDate { get; set; } = DateTime.Now;

    }


}
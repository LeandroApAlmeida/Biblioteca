using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;

namespace Library.Db.Models {


    [Table("Loan")]
    public class LoanModel {

        [Key, DatabaseGenerated(DatabaseGeneratedOption.None)]
        public Guid Id { get; set; }

        [Required(ErrorMessage = "Selecione um livro")]
        public required BookModel Book { get; set; }

        [Required(ErrorMessage = "Selecione uma pessoa")]
        public required PersonModel Person { get; set; }

        public string? Notes { get; set; }

        [DataType(DataType.DateTime)]
        [Required (ErrorMessage = "Digite a data do empréstimo do livro")]
        public required DateTime Date { get; set; }

        [DataType(DataType.DateTime)]
        public DateTime ReturnDate { get; set; }

        [DataType(DataType.DateTime)]
        public required DateTime RegistrationDate { get; set; } = DateTime.Now;

        [DataType(DataType.DateTime)]
        public required DateTime LastUpdateDate { get; set; } = DateTime.Now;

        public required bool IsDeleted { get; set; } = false;

        public required bool IsReturned { get; set; } = false;

    }


}
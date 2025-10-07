using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;

namespace Library.Db.Models {


    /// <summary>
    /// Entidade que representa um livro emprestado. 
    /// </summary>
    [Table("Loan")]
    public class LoanModel {

        /// <summary> Identificador chave primária do livro emprestado. </summary>
        [Key, DatabaseGenerated(DatabaseGeneratedOption.None)]
        public Guid Id { get; set; }

        /// <summary> Instância do livro emprestado. </summary>
        [Required(ErrorMessage = "Selecione um livro")]
        public required BookModel Book { get; set; }

        /// <summary> Instância do tomador do empréstimo. </summary>
        [Required(ErrorMessage = "Selecione uma pessoa")]
        public required PersonModel Person { get; set; }

        ///<summary> Anotações sobre o empréstimo do livro. </summary>
        public string? Notes { get; set; }

        /// <summary> Data do empréstimo do livro. </summary>
        [DataType(DataType.DateTime)]
        [Required (ErrorMessage = "Digite a data do empréstimo do livro")]
        public required DateTime Date { get; set; }

        /// <summary> Data da devolução do livro. </summary>
        [DataType(DataType.DateTime)]
        public DateTime ReturnDate { get; set; }

        /// <summary> Data de cadastro do livro emprestado. </summary>
        [DataType(DataType.DateTime)]
        public required DateTime RegistrationDate { get; set; } = DateTime.Now;

        /// <summary> Data da última atualização de cadastro do livro emprestado. </summary>
        [DataType(DataType.DateTime)]
        public required DateTime LastUpdateDate { get; set; } = DateTime.Now;

        /// <summary> Status de livro excluído. </summary>
        public required bool IsDeleted { get; set; } = false;

        /// <summary> Status de livro devolvido. </summary>
        public required bool IsReturned { get; set; } = false;

    }


}
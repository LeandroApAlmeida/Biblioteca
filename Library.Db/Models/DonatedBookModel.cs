using Microsoft.EntityFrameworkCore.Metadata.Internal;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;

namespace Library.Db.Models {


    /// <summary>
    /// Entidade que representa um livro doado. Um livro pode ser doado para uma pessoa física, e,
    /// eventualmente, para uma biblioteca.
    /// </summary>
    [Table("DonatedBook")]
    public class DonatedBookModel {

        /// <summary> Identificador chave primária do livro doado. </summary>
        [Key, DatabaseGenerated(DatabaseGeneratedOption.None)]
        public Guid Id { get; set; }

        /// <summary> Instância do livro doado. </summary>
        [ForeignKey("Id")]
        [Required(ErrorMessage = "Selecione um livro")]
        public required BookModel Book { get; set; }

        /// <summary> Instância do beneficiário da doação. </summary>
        [Required(ErrorMessage = "Selecione uma pessoa")]
        public required PersonModel Person { get; set; }

        ///<summary> Anotações sobre a doação do livro. </summary>
        public string? Notes { get; set; }

        /// <summary> Data da doação do livro. </summary>
        [DataType(DataType.DateTime)]
        [Required(ErrorMessage = "Digite a data de doação do livro")]
        public required DateTime Date { get; set; }

        /// <summary> Data de cadastro do livro doado. </summary>
        [DataType(DataType.DateTime)]
        public required DateTime RegistrationDate { get; set; } = DateTime.Now;

        /// <summary> Data da última atualização de cadastro do livro doado. </summary>
        [DataType(DataType.DateTime)]
        public required DateTime LastUpdateDate { get; set; } = DateTime.Now;

    }


}
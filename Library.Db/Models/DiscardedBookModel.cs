using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;

namespace Library.Db.Models {


    /// <summary>
    /// Entidade que representa um livro descartado. Um livro é descartado em razão de dano, liberação
    /// de espaço físico, perda ou furto.
    /// </summary>
    [Table("DiscardedBook")]
    public class DiscardedBookModel {

        /// <summary> Identificador chave primária do livro descartado. </summary>
        [Key, DatabaseGenerated(DatabaseGeneratedOption.None)]
        public Guid Id { get; set; }

        /// <summary> Instância do livro descartado. </summary>
        [ForeignKey("Id")]
        [Required(ErrorMessage = "Selecione um livro")]
        public required BookModel Book { get; set; }

        /// <summary> Motivo do descarte do livro. </summary>
        public string? Reason { get; set; }

        /// <summary> Data do descarte do livro. </summary>
        [DataType(DataType.DateTime)]
        [Required(ErrorMessage = "Digite a data de descarte do livro")]
        public required DateTime Date { get; set; }

        /// <summary> Data de cadastro do livro descartado. </summary>
        [DataType(DataType.DateTime)]
        public required DateTime RegistrationDate { get; set; } = DateTime.Now;

        /// <summary> Data da última atualização de cadastro do livro descartado. </summary>
        [DataType(DataType.DateTime)]
        public required DateTime LastUpdateDate { get; set; } = DateTime.Now;

    }


}
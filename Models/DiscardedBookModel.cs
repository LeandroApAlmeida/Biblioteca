﻿using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;

namespace Library.Models {

    [Table("DiscardedBook")]
    public class DiscardedBookModel {

        [Key, DatabaseGenerated(DatabaseGeneratedOption.None)]
        public Guid Id { get; set; }

        [ForeignKey("Id")]
        [Required(ErrorMessage = "Selecione um livro")]
        public required BookModel Book { get; set; }

        public string? Reason { get; set; }

        [DataType(DataType.Date)]
        [Required(ErrorMessage = "Digite a data de aquisição do livro")]
        public required DateTime Date { get; set; }

        [DataType(DataType.Date)]
        public required DateTime RegistrationDate { get; set; } = DateTime.Now;

        [DataType(DataType.Date)]
        public required DateTime LastUpdateDate { get; set; } = DateTime.Now;

    }

}
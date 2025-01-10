﻿using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;

namespace Library.Models {

    [Table("Loan")]
    public class LoanModel {

        [Key, DatabaseGenerated(DatabaseGeneratedOption.None)]
        public Guid Id { get; set; }

        [Required(ErrorMessage = "Selecione um livro")]
        public required BookModel Book { get; set; }

        [Required(ErrorMessage = "Selecione uma pessoa")]
        public required PersonModel Person { get; set; }

        public string? Notes { get; set; }

        [DataType(DataType.Date)]
        [Required (ErrorMessage = "Digite a data do empréstimo")]
        public required DateTime Date { get; set; }

        [DataType(DataType.Date)]
        public DateTime ReturnDate { get; set; }

        [DataType(DataType.Date)]
        public required DateTime RegistrationDate { get; set; } = DateTime.Now;

        [DataType(DataType.Date)]
        public required DateTime LastUpdateDate { get; set; } = DateTime.Now;

        public required bool IsDeleted { get; set; } = false;

        public required bool IsReturned { get; set; } = false;

    }

}

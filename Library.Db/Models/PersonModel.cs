using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;

namespace Library.Db.Models {


    [Table("Person")]
    public class PersonModel {

        [Key, DatabaseGenerated(DatabaseGeneratedOption.None)]
        public Guid Id { get; set; }

        [Required(ErrorMessage = "Digite o nome da pessoa")]
        public required string Name { get; set; }

        public string? Street { get; set; }

        public string? Number { get; set; }

        public string? District {  get; set; }
        
        public string? PostalCode { get; set; }

        public string? Complement { get; set; }

        public string? City { get; set; }

        public string? FederalState { get; set; }

        public string? Country { get; set; }

        public string? Description { get; set; }

        [DataType(DataType.DateTime)]
        public required DateTime RegistrationDate { get; set; } = DateTime.Now;

        [DataType(DataType.DateTime)]
        public required DateTime LastUpdateDate { get; set; } = DateTime.Now;

        public required bool IsDeleted { get; set; } = false;

    }


}
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;

namespace Library.Models {

    [Table("Session")]
    public class SessionModel {

        [Key, DatabaseGenerated(DatabaseGeneratedOption.None)]
        public int Id { get; set; }

        public required UserModel User { get; set; }

        [DataType(DataType.Date)]
        public required DateTime LoginDate { get; set; }

        [DataType(DataType.Date)]
        public required DateTime LogoutDate { get; set; }

    }

}
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;

namespace Library.Db.Models {


    [Table("Session")]
    public class SessionModel {

        [Key, DatabaseGenerated(DatabaseGeneratedOption.Identity)]
        public Guid Id { get; set; }

        public required UserModel User { get; set; }

        public string? Ip { get; set; }

        [DataType(DataType.DateTime)]
        public required DateTime LoginDate { get; set; }

        [DataType(DataType.DateTime)]
        public required DateTime LogoutDate { get; set; }

    }


}
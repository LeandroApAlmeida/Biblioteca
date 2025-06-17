using System.ComponentModel.DataAnnotations.Schema;
using System.ComponentModel.DataAnnotations;

namespace Library.Db.Models {


    [Table("UserRole")]
    public class UserRoleModel {

        [Key, DatabaseGenerated(DatabaseGeneratedOption.None)]
        public int Id { get; set; }

        public required string Description { get; set; }

    }


}

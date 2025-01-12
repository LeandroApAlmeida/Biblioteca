using Microsoft.EntityFrameworkCore;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;
namespace Library.Models {


    [Table("User")]
    [Index(nameof(UserName), IsUnique = true)]
    public class UserModel {

        [Key, DatabaseGenerated(DatabaseGeneratedOption.None)]
        public Guid Id { get; set; }

        public required UserRoleModel Role { get; set; }

        public required string Name { get; set; }

        public required string UserName { get; set; }

        public required byte[] PasswordHash { get; set; }

        [DataType(DataType.Date)]
        public required DateTime RegistrationDate { get; set; } = DateTime.Now;

        [DataType(DataType.Date)]
        public required DateTime LastUpdateDate { get; set; } = DateTime.Now;

        public required bool IsDeleted { get; set; } = false;

        public required bool IsActive { get; set; } = true;

    }


}
using Microsoft.EntityFrameworkCore;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;

namespace Library.Db.Models {


    [Table("User")]
    [Index(nameof(UserName), IsUnique = true)]
    public class UserModel {

        [Key, DatabaseGenerated(DatabaseGeneratedOption.None)]
        public Guid Id { get; set; }

        [Required(ErrorMessage = "Informe o privilégio de acesso do usuário")]
        public required UserRoleModel Role { get; set; }

        [Required(ErrorMessage = "Digite o nome completo")]
        public required string Name { get; set; }

        [Required(ErrorMessage = "Digite o nome de usuário")]
        public required string UserName { get; set; }

        [Required(ErrorMessage = "Digite a senha")]
        public required byte[] PasswordHash { get; set; }

        [DataType(DataType.DateTime)]
        public required DateTime RegistrationDate { get; set; } = DateTime.Now;

        [DataType(DataType.DateTime)]
        public required DateTime LastUpdateDate { get; set; } = DateTime.Now;

        public required bool IsDeleted { get; set; } = false;

        public required bool IsActive { get; set; } = true;

    }


}
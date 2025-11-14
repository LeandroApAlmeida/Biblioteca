using Microsoft.EntityFrameworkCore;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;

namespace Library.Db.Models {


    /// <summary>
    /// Entidade que representa um usuário da aplicação. Existem dois tipos de usuário:
    /// 
    /// <br/> <br/>
    /// 
    /// <u>Usuário Administrador:</u> tem acesso irrestrito a todas às funcionalidades da aplicação.
    /// 
    /// <br/> <br/>
    /// 
    /// <u>Usuário Convidado:</u> tem acesso a funcionalidades de manutenção do acervo apenas.
    /// 
    /// <br/> <br/>
    /// 
    /// A interface da aplicação será configurada de acordo com o privilégio de cada usuário.
    /// </summary>
    [Table("User")]
    [Index(nameof(UserName), IsUnique = true)]
    public class UserModel {

        /// <summary> Identificador chave primária do usuário. </summary>
        [Key, DatabaseGenerated(DatabaseGeneratedOption.None)]
        public Guid Id { get; set; }

        /// <summary> Privilégio de acesso do usuário. </summary>
        [Required(ErrorMessage = "Informe o privilégio de acesso do usuário")]
        public required UserRoleModel Role { get; set; }

        /// <summary> Nome completo do usuário. </summary>
        [Required(ErrorMessage = "Digite o nome completo")]
        public required string Name { get; set; }

        /// <summary> Nome de usuário da aplicação. </summary>
        [Required(ErrorMessage = "Digite o nome de usuário")]
        public required string UserName { get; set; }

        /// <summary> Hash da senha do usuário. </summary>
        [Required(ErrorMessage = "Digite a senha")]
        public required byte[] PasswordHash { get; set; } = [0];

        /// <summary> Data de cadastro do usuário. </summary>
        [DataType(DataType.DateTime)]
        public required DateTime RegistrationDate { get; set; } = DateTime.Now;

        /// <summary> Data da última atualização de cadastro do usuário. </summary>
        [DataType(DataType.DateTime)]
        public required DateTime LastUpdateDate { get; set; } = DateTime.Now;

        /// <summary> Status de usuário excluído. </summary>
        public required bool IsDeleted { get; set; } = false;

        /// <summary> Status de usuário ativo. </summary>
        public required bool IsActive { get; set; } = true;

    }


}
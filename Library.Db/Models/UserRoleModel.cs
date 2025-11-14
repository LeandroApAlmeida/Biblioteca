using System.ComponentModel.DataAnnotations.Schema;
using System.ComponentModel.DataAnnotations;

namespace Library.Db.Models {


    /// <summary>
    /// Entidade que representa a função de usuário. Há dois tipos de usuário:
    /// 
    /// <br/> <br/>
    /// 
    /// <u>Administrador:</u> tem acesso irrestrito a todas às funcionalidades da aplicação.
    /// 
    /// <br/> <br/>
    /// 
    /// <u>Convidado:</u> tem acesso a funcionalidades de manutenção do acervo apenas.
    /// 
    /// <br/> <br/>
    /// 
    /// </summary>
    [Table("UserRole")]
    public class UserRoleModel {

        /// <summary> Identificador chave primária da função de usuário. </summary>
        [Key, DatabaseGenerated(DatabaseGeneratedOption.None)]
        public int Id { get; set; }

        /// <summary> Descrição da função de usuário. </summary>
        public required string Description { get; set; }

    }


}
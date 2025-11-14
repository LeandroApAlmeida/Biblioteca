using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;

namespace Library.Db.Models {


    /// <summary>
    /// Entidade que representa uma sessão de usuário no sistema.
    /// </summary>
    [Table("Session")]
    public class SessionModel {

        /// <summary> Chave primária da sessão de usuário. </summary>
        [Key, DatabaseGenerated(DatabaseGeneratedOption.Identity)]
        public Guid Id { get; set; }

        /// <summary> Instância do usuário da sessão. </summary>
        public required UserModel User { get; set; }

        /// <summary> Número do endereço IP e MAC do host do usuário. </summary>
        public string? Ip { get; set; }

        /// <summary> Data da realização do login. </summary>
        [DataType(DataType.DateTime)]
        public required DateTime LoginDate { get; set; }

        /// <summary> Data da realização do logout. </summary>
        [DataType(DataType.DateTime)]
        public required DateTime LogoutDate { get; set; }

    }


}
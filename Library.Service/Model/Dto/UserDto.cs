using System.ComponentModel.DataAnnotations;

namespace Library.Services.Model.Dto {
    
    
    /// <summary>
    /// DTO para cadastro de usuário.
    /// </summary>
    public class UserDto {

        /// <summary> Identificador chave primária do usuário. </summary>
        public Guid Id { get; set; }

        /// <summary> Privilégio de acesso à aplicação. </summary>
        public required int Role { get; set; }

        /// <summary> Nome completo do usuário. </summary>
        [Required(ErrorMessage = "Digite o nome completo")]
        public required string Name { get; set; }

        /// <summary> Nome de usuário. </summary>
        [Required(ErrorMessage = "Digite o nome de usuário")]
        public required string UserName { get; set; }

        /// <summary> Senha de acesso. </summary>
        [Required(ErrorMessage = "Digite a senha")]
        public required string Password { get; set; }

        /// <summary> Confirmação da senha de acesso. </summary>
        [Required(ErrorMessage = "Digite a confirmação da senha"),
        Compare("Password", ErrorMessage ="Campos Senha e Confirme Senha não conferem")]
        public required string ConfPassword { get; set; }

    }


}
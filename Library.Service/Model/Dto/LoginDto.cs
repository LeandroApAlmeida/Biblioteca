using System.ComponentModel.DataAnnotations;

namespace Library.Services.Model.Dto {


    /// <summary>
    /// DTO para login.
    /// </summary>
    public class LoginDto {

        /// <summary> Nome de usuário. </summary>
        [Required(ErrorMessage = "Digite o nome de usuário")]
        public required string UserName { get; set; }

        /// <summary> Senha de acesso. </summary>
        [Required(ErrorMessage = "Digite a senha de acesso")]
        public required string Password { get; set; }

    }


}
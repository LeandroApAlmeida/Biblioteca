using System.ComponentModel.DataAnnotations;

namespace Library.Services.Model.Dto {
    
    
    public class UserDto {

        public Guid Id { get; set; }

        public required int Role { get; set; }

        [Required(ErrorMessage = "Digite o nome completo")]
        public required string Name { get; set; }

        [Required(ErrorMessage = "Digite o nome de usuário")]
        public required string UserName { get; set; }

        [Required(ErrorMessage = "Digite a senha")]
        public required string Password { get; set; }

        [Required(ErrorMessage = "Digite a confirmação da senha"),
        Compare("Password", ErrorMessage ="Campos Senha e Confirme Senha não conferem")]
        public required string ConfPassword { get; set; }

    }


}
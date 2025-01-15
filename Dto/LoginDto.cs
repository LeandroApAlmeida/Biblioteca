using System.ComponentModel.DataAnnotations;

namespace Library.Dto {


    public class LoginDto {


        [Required(ErrorMessage = "Digite o nome de usuário")]
        public required string UserName { get; set; }


        [Required(ErrorMessage = "Digite a senha de acesso")]
        public required string Password { get; set; }

    
    }


}

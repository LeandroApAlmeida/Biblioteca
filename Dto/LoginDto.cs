using System.ComponentModel.DataAnnotations;

namespace Library.Dto {


    public class LoginDto {


        [Required(ErrorMessage = "Digite o nome de usuário")]
        public string UserName { get; set; }


        [Required(ErrorMessage = "Digite a senha de acesso")]
        public string Password { get; set; }

    
    }


}

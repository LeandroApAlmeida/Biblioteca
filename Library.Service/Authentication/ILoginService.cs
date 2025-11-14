using Library.Db.Models;
using Library.Services.Model.Dto;

namespace Library.Services.Authentication {


    /// <summary>
    /// Interface que define um serviço de login.
    /// </summary>
    public interface ILoginService {


        /// <summary>
        /// Realizar o login na aplicação.
        /// </summary>
        /// <param name="login">Dados para o login.</param>
        /// <param name="ip">Endereço IP do host conectado.</param>
        /// <returns>Dados do login.</returns>
        public Task<Response<SessionModel?>> Login(LoginDto login, string? ip);


        /// <summary>
        /// Realizar o logout na aplicação.
        /// </summary>
        /// <returns>True, se fez o logout. False, em caso de erro no logout.</returns>
        public Task<Response<bool?>> Logout();


    }


}

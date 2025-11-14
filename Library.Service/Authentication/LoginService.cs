using Library.Db.Models;
using Library.Services.Model.Dto;
using Library.Services.Session;
using Library.Services.User;

namespace Library.Services.Authentication {


    /// <summary>
    /// Classe para gerenciamento do acesso à aplicação.
    /// </summary>
    public class LoginService : ILoginService {


        /// <summary> Objeto para manutenção de contas de usuário. </summary>
        private readonly IUserService _userService;

        /// <summary> Objeto para geração de hash da senha. </summary>
        private readonly IPasswordService _passwordService;

        /// <summary> Objeto para manutenção de sessão de usuário. </summary>
        private readonly ISessionService _sessionService;


        public LoginService(IUserService userService, IPasswordService passwordService,
        ISessionService sessionService) {
            _userService = userService;
            _passwordService = passwordService;
            _sessionService = sessionService;
        }


        public async Task<Response<SessionModel?>> Login(LoginDto loginData, string? ip) {

            Response<SessionModel?> response = new();

            try {

                // Obtém o usuário com o nome definido em loginData.
                var userResp = await _userService.GetUserWithHash(loginData.UserName);

                if (!userResp.Successful) throw new Exception(userResp.Message);
                 
                if (userResp.Data != null) {

                    // Se recuperou o usuário...

                    // Se o usuário foi deletado, impede o acesso à aplicação.
                    if (userResp.Data.IsDeleted) {
                        throw new Exception("Acesso negado!");
                    }

                    // Verifica se a senha do usuário confere com a senha que gerou o hash
                    // salvo no banco de dados.
                    var isTheSamePassword = _passwordService.IsTheSamePassword(
                        loginData.Password,
                        userResp.Data.PasswordHash
                    );

                    if (isTheSamePassword) {

                        // Sendo a mesma senha, inicia uma sessão da aplicação.

                        var createSessionResp = await _sessionService.CreateSession(userResp.Data, ip);

                        if (!createSessionResp.Successful) {
                            throw new Exception(createSessionResp.Message);
                        }

                        response.Message = "Login efetuado!";

                        response.Data = createSessionResp.Data;

                        return response;

                    } else {

                        // Não sendo a mesma senha, impede o acesso à aplicação.

                        response.Message = "Credenciais inválidas!";

                        response.Data = null;

                        return response;

                    }

                } else {

                    // Se não recuperou o usuário, impede o acesso à aplicação.

                    response.Message = "Credenciais inválidas!";

                    response.Data = null;

                    return response;

                }

            } catch (Exception ex) {

                response.Message = ex.Message;
                response.Successful = false;
                response.Data = null;

                return response;

            }

        }


        public async Task<Response<bool?>> Logout() {

            Response<bool?> response = new();

            var removeSessionResp = await _sessionService.RemoveSession();

            if (removeSessionResp.Successful) {

                response.Data = true;

                return response;

            } else {

                response.Data = false;

                response.Message = removeSessionResp.Message;

                return response;

            }


        }


    }


}
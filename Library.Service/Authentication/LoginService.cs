using Library.Db.Models;
using Library.Services.Model.Dto;
using Library.Services.Session;
using Library.Services.User;

namespace Library.Services.Authentication {


    public class LoginService : ILoginService {


        private readonly IUserService _userService;

        private readonly IPasswordService _passwordService;

        private readonly ISessionService _sessionService;


        public LoginService(IUserService userService, IPasswordService passwordService,
        ISessionService sessionService) {
            _userService = userService;
            _passwordService = passwordService;
            _sessionService = sessionService;
        }


        public async Task<Response<SessionModel?>> Login(LoginDto login, string? ip) {

            Response<SessionModel?> response = new();

            try {

                var userResp = await _userService.GetUser(login.UserName);

                if (!userResp.Successful) throw new Exception(userResp.Message);

                if (userResp.Data != null) {

                    if (userResp.Data.IsDeleted) {
                        throw new Exception("Acesso negado!");
                    }

                    var isTheSamePassword = _passwordService.IsTheSamePassword(
                        login.Password,
                        userResp.Data.PasswordHash
                    );

                    if (isTheSamePassword) {

                        var createSessionResp = await _sessionService.CreateSession(userResp.Data, ip);

                        if (!createSessionResp.Successful) {
                            throw new Exception(createSessionResp.Message);
                        }

                        response.Message = "Login efetuado!";

                        response.Data = createSessionResp.Data;

                        return response;

                    } else {

                        response.Message = "Credenciais inválidas!";

                        response.Data = null;

                        return response;

                    }

                } else {

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
using Library.Dto;
using Library.Models;
using Library.Services.PasswordService;
using Library.Services.SessionService;
using Library.Services.UserService;

namespace Library.Services.LoginService {


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


        public async Task<ResponseModel<SessionModel?>> Login(LoginDto login) {

            ResponseModel<SessionModel?> response = new();

            try {

                var userResp = await _userService.GetUser(login.UserName);

                if (!userResp.Successful) throw new Exception(userResp.Message);

                if (userResp.Data != null) {

                    var isItTheSamePassword = _passwordService.IsItTheSamePassword(
                        login.Password,
                        userResp.Data.PasswordHash
                    );

                    if (isItTheSamePassword) {

                        var createSessionResp = await _sessionService.CreateSession(userResp.Data);

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


    }


}
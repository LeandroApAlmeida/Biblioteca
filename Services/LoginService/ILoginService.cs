using Library.Dto;
using Library.Models;

namespace Library.Services.LoginService {


    public interface ILoginService {

        public Task<ResponseModel<SessionModel?>> Login(LoginDto login);

    }


}

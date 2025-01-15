using Library.Dto;
using Library.Models;
using System.Net;

namespace Library.Services.LoginService {


    public interface ILoginService {

        public Task<ResponseModel<SessionModel?>> Login(LoginDto login, string? ip);


        public Task<ResponseModel<bool?>> Logout();


    }


}

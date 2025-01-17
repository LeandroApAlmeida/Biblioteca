using Library.Dto;
using Library.Models;
using System.Net;

namespace Library.Services.LoginService {


    public interface ILoginService {

        public Task<Response<SessionModel?>> Login(LoginDto login, string? ip);


        public Task<Response<bool?>> Logout();


    }


}

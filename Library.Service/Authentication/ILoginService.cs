using Library.Db.Models;
using Library.Services.Model.Dto;

namespace Library.Services.Authentication {


    public interface ILoginService {

        public Task<Response<SessionModel?>> Login(LoginDto login, string? ip);


        public Task<Response<bool?>> Logout();


    }


}

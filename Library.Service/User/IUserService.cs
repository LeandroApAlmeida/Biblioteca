using Library.Db.Models;
using Library.Services.Model.Dto;

namespace Library.Services.User {


    public interface IUserService {

        public Task<Response<List<UserRoleModel>>> GetUserRoles();

        public Task<Response<UserRoleModel>> GetUserRole(int id);

        public Task<Response<List<UserModel>>> GetUsers();

        public Task<Response<UserModel>> GetUser(Guid id);

        public Task<Response<UserModel>> GetUserWithHash(Guid id);

        public Task<Response<UserModel>> GetUser(string userName);

        public Task<Response<bool>> RegisteredAdmin();

        public Task<Response<UserModel>> RegisterUser(UserDto user);

        public Task<Response<UserModel>> EditUser(UserDto user);

        public Task<Response<UserModel>> DeleteUser(Guid id);

        public Task<Response<UserModel>> UndeleteUser(Guid id);

    }


}

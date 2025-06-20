using Library.Db.Models;
using Library.Services.Model.Dto;

namespace Library.Services.User {


    /// <summary>
    /// Interface para manutenção de usuários.
    /// </summary>
    public interface IUserService {

        public Task<Response<List<UserRoleModel>>> GetUserRoles();

        public Task<Response<UserRoleModel>> GetUserRole(int id);

        public Task<Response<List<UserModel>>> GetUsers();

        public Task<Response<UserModel>> GetUserWithoutHash(Guid id);

        public Task<Response<UserModel>> GetUserWithHash(Guid id);

        public Task<Response<UserModel>> GetUserWithHash(string userName);

        public Task<Response<bool>> RegisteredAdmin();

        public Task<Response<UserModel>> RegisterUser(UserDto user);

        public Task<Response<UserModel>> EditUser(UserDto user);

        public Task<Response<UserModel>> DeleteUser(Guid id);

        public Task<Response<UserModel>> UndeleteUser(Guid id);

    }


}

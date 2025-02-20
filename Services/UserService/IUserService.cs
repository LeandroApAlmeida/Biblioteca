using Library.Dto;
using Library.Models;

namespace Library.Services.UserService {


    public interface IUserService {

        public Task<Response<List<UserRoleModel>>> GetUserRoles();

        public Task<Response<UserRoleModel>> GetUserRole(Int32 id);

        public Task<Response<List<UserModel>>> GetUsers();

        public Task<Response<UserModel>> GetUser(Guid id);

        public Task<Response<UserModel>> GetUserWithHash(Guid id);

        public Task<Response<UserModel>> GetUser(string userName);

        public Task<Response<Boolean>> RegisteredAdmin();

        public Task<Response<UserModel>> RegisterUser(UserDto user);

        public Task<Response<UserModel>> EditUser(UserDto user);

        public Task<Response<UserModel>> DeleteUser(Guid id);

        public Task<Response<UserModel>> UndeleteUser(Guid id);

    }


}

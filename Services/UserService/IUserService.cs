using Library.Dto;
using Library.Models;

namespace Library.Services.UserService {


    public interface IUserService {

        public Task<ResponseModel<List<UserRoleModel>>> GetUserRoles();

        public Task<ResponseModel<UserRoleModel>> GetUserRole(Int32 id);

        public Task<ResponseModel<List<UserModel>>> GetUsers();

        public Task<ResponseModel<UserModel>> GetUser(Guid id);

        public Task<ResponseModel<UserModel>> GetUser(string userName);

        public Task<ResponseModel<Boolean>> RegisteredAdmin();

        public Task<ResponseModel<UserModel>> RegisterUser(UserDto user);

        public Task<ResponseModel<UserModel>> EditUser(UserDto user);

        public Task<ResponseModel<UserModel>> DeleteUser(UserModel user);

        public Task<ResponseModel<UserModel>> UndeleteUser(UserModel user);

    }


}

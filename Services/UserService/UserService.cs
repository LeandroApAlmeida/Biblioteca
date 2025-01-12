using Library.Data;
using Library.Dto;
using Library.Models;
using Library.Services.PasswordService;
using Microsoft.EntityFrameworkCore;

namespace Library.Services.UserService {


    public class UserService : IUserService {


        private readonly ApplicationDbContext _context;

        private readonly IPasswordService _passwordService;


        public UserService(ApplicationDbContext context, IPasswordService passwordService) {
            _context = context;
            _passwordService = passwordService;
        }


        public async Task<ResponseModel<List<UserRoleModel>>> GetUserRoles() {

            ResponseModel<List<UserRoleModel>> response = new();

            try {

                List<UserRoleModel> roles = await _context.UserRoles
                .OrderBy(ur => ur.Id)
                .AsNoTracking()
                .ToListAsync();

                response.Data = roles;

                return response;

            } catch (Exception ex) {

                response.Message = ex.Message;
                response.Successful = false;

                return response;

            }

        }


        public async Task<ResponseModel<UserRoleModel>> GetUserRole(int id) {

            ResponseModel<UserRoleModel> response = new();

            try {

                List<UserRoleModel> role = await _context.UserRoles
                .Where(ur => ur.Id == id)
                .AsNoTracking()
                .ToListAsync();

                response.Data = role.First();

                return response;

            } catch (Exception ex) {

                response.Message = ex.Message;
                response.Successful = false;

                return response;

            }

        }


        public async Task<ResponseModel<List<UserModel>>> GetUsers() {

            ResponseModel<List<UserModel>> response = new();

            try {

                List<UserModel> users = await _context.Users
                .Select(um => new UserModel {

                    Id = um.Id,

                    Role = new UserRoleModel {
                        Id = um.Role.Id,
                        Description = um.Role.Description
                    },

                    Name = um.Name,

                    UserName = um.UserName,

                    PasswordHash = new byte[0],

                    RegistrationDate = um.RegistrationDate,

                    LastUpdateDate = um.LastUpdateDate,

                    IsDeleted = um.IsDeleted,

                    IsActive = um.IsActive

                })
                .OrderBy(um => um.Role.Id)
                .ThenBy(um => um.Name)
                .AsNoTracking()
                .ToListAsync();

                response.Data = users;

                return response;

            } catch (Exception ex) {

                response.Message = ex.Message;
                response.Successful = false;

                return response;

            }

        }


        public async Task<ResponseModel<UserModel>> GetUser(Guid id) {

            ResponseModel<UserModel> response = new();

            try {

                List<UserModel> users = await _context.Users
                .Select(um => new UserModel {

                    Id = um.Id,

                    Role = new UserRoleModel {
                        Id = um.Role.Id,
                        Description = um.Role.Description
                    },

                    Name = um.Name,

                    UserName = um.UserName,

                    PasswordHash = new byte[0],

                    RegistrationDate = um.RegistrationDate,

                    LastUpdateDate = um.LastUpdateDate,

                    IsDeleted = um.IsDeleted,

                    IsActive = um.IsActive

                })
                .Where(um => um.Id == id)
                .AsNoTracking()
                .ToListAsync();

                response.Data = users.First();

                return response;

            } catch (Exception ex) {

                response.Message = ex.Message;
                response.Successful = false;

                return response;

            }

        }


        public async Task<ResponseModel<UserModel>> GetUser(string userName) {

            ResponseModel<UserModel> response = new();

            try {

                List<UserModel> users = await _context.Users
                .Select(um => new UserModel {

                    Id = um.Id,

                    Role = new UserRoleModel {
                        Id = um.Role.Id,
                        Description = um.Role.Description
                    },

                    Name = um.Name,

                    UserName = um.UserName,

                    PasswordHash = um.PasswordHash,

                    RegistrationDate = um.RegistrationDate,

                    LastUpdateDate = um.LastUpdateDate,

                    IsDeleted = um.IsDeleted,

                    IsActive = um.IsActive

                })
                .Where(um => um.UserName == userName)
                .AsNoTracking()
                .ToListAsync();

                if (users.Count > 0) {
                    response.Data = users.First();
                } else {
                    response.Data = null;
                }

                return response;

            } catch (Exception ex) {

                response.Message = ex.Message;
                response.Successful = false;

                return response;

            }

        }


        public async Task<ResponseModel<Boolean>> RegisteredAdmin() {

            ResponseModel<Boolean> response = new();

            try {

                DateTime date = DateTime.Now;

                List<UserModel> users = await _context.Users
                .Where(um => um.Role.Id == (int)UserRole.Admin)
                .ToListAsync();

                response.Data = users.Count > 0;

                return response;

            } catch (Exception ex) {

                response.Message = ex.Message;
                response.Successful = false;

                return response;

            }

        }


        private async Task<ResponseModel<Boolean>> UserNameAlreadyExist(string userName) {

            ResponseModel<Boolean> response = new();

            try {

                DateTime date = DateTime.Now;

                List<UserModel> users = await _context.Users
                .Where(um => um.UserName == userName)
                .ToListAsync();

                response.Data = users.Count > 0;

                return response;

            } catch (Exception ex) {

                response.Message = ex.Message;
                response.Successful = false;

                return response;

            }

        }


        public async Task<ResponseModel<UserModel>> RegisterUser(UserDto user) {

            ResponseModel<UserModel> response = new();

            try {

                DateTime date = DateTime.Now;

                var userRoleResp = await GetUserRole(user.Role);

                if (!userRoleResp.Successful) throw new Exception(userRoleResp.Message);

                if (userRoleResp.Data!.Id == (int) UserRole.Admin) {

                    // É permitido apenas um usuário administrador cadastrado. Neste
                    // ponto é feito este controle, de tal forma que se já existir um
                    // administrador, não permite o cadastro de outro.
                    
                    var registeredAdminResp = await RegisteredAdmin();

                    if (!registeredAdminResp.Successful) {
                        throw new Exception(registeredAdminResp.Message);
                    }
                        
                    if (registeredAdminResp.Data == true) {
                        throw new Exception("Já existe um usuário administrador cadastrado!");
                    }

                }

                var userNameAlreadyExistResp = await UserNameAlreadyExist(user.UserName);

                if (!userNameAlreadyExistResp.Successful) {
                    throw new Exception(userNameAlreadyExistResp.Message);
                }

                if (userNameAlreadyExistResp.Data == true) {

                    // Já existe uma conta de usuário com o mesmo nome de usuário que o que
                    // está sendo cadastrado. Neste caso, não permite cadastrar.

                    throw new Exception("Usuário " + user.UserName + " já está cadastrado!");

                }

                UserRoleModel role = userRoleResp.Data!;

                _context.Attach(role);

                UserModel model = new UserModel {
                    Id = Guid.NewGuid(),
                    Role = role,
                    Name = user.Name,
                    UserName = user.UserName,
                    PasswordHash = _passwordService.GeneratePasswordHash(user.Password),
                    RegistrationDate = date,
                    LastUpdateDate = date,
                    IsDeleted = false,
                    IsActive = true
                };

                _context.Users.Add(model);

                await _context.SaveChangesAsync();

                response.Message = "Usuário cadastrado com sucesso!";
                response.Data = model;

                return response;

            } catch (Exception ex) {

                response.Message = ex.Message;
                response.Successful = false;

                return response;

            }

        }


        public Task<ResponseModel<UserModel>> EditUser(UserDto user) {
            throw new NotImplementedException();
        }


        public Task<ResponseModel<UserModel>> DeleteUser(UserDto user) {
            throw new NotImplementedException();
        }

        public Task<ResponseModel<UserModel>> DismissUser(UserDto user) {
            throw new NotImplementedException();
        }

        public Task<ResponseModel<UserModel>> RestoreUser(UserDto user) {
            throw new NotImplementedException();
        }

    }


}

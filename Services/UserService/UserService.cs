﻿using Library.Data;
using Library.Dto;
using Library.Models;
using Library.Services.PasswordService;
using Microsoft.EntityFrameworkCore;
using Library.Utils;
using System;

namespace Library.Services.UserService {


    public class UserService : IUserService {


        private readonly ApplicationDbContext _context;

        private readonly IPasswordService _passwordService;


        public UserService(ApplicationDbContext context, IPasswordService passwordService) {
            _context = context;
            _passwordService = passwordService;
        }


        public async Task<Response<List<UserRoleModel>>> GetUserRoles() {

            Response<List<UserRoleModel>> response = new();

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


        public async Task<Response<UserRoleModel>> GetUserRole(int id) {

            Response<UserRoleModel> response = new();

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


        public async Task<Response<List<UserModel>>> GetUsers() {

            Response<List<UserModel>> response = new();

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

                    PasswordHash = new byte[] { 0 },

                    RegistrationDate = um.RegistrationDate,

                    LastUpdateDate = um.LastUpdateDate,

                    IsDeleted = um.IsDeleted,

                    IsActive = um.IsActive

                })
                //.Where(um => !um.IsDeleted && um.IsActive)
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


        public async Task<Response<UserModel>> GetUser(Guid id) {

            Response<UserModel> response = new();

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

                    PasswordHash = new byte[] { 0 },

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


        public async Task<Response<UserModel>> GetUserWithHash(Guid id) {

            Response<UserModel> response = new();

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


        public async Task<Response<UserModel>> GetUser(string userName) {

            Response<UserModel> response = new();

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


        public async Task<Response<Boolean>> RegisteredAdmin() {

            Response<Boolean> response = new();

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


        private async Task<Response<Boolean>> UserNameAlreadyExist(string userName) {

            Response<Boolean> response = new();

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


        public async Task<Response<UserModel>> RegisterUser(UserDto user) {

            Response<UserModel> response = new();

            try {

                if (user.Password.Length < 5) {
                    throw new Exception("A senha do usuário deve conter no mínimo 5 caracteres.");
                }

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


        public async Task<Response<UserModel>> EditUser(UserDto user) {

            Response<UserModel> response = new();

            try {

                DateTime date = DateTime.Now;

                var userResp = await GetUser(user.Id);

                if (!userResp.Successful) throw new Exception(userResp.Message);

                if (userResp.Data!.Role.Id != user.Role) {
                    throw new Exception("Não é permitido alterar o privilégio de acesso.");
                }

                if (userResp.Data!.UserName != user.UserName) {

                    var userNameAlreadyExistResp = await UserNameAlreadyExist(user.UserName);

                    if (!userNameAlreadyExistResp.Successful) {

                        throw new Exception(userNameAlreadyExistResp.Message);

                    }

                    if (userNameAlreadyExistResp.Data == true) {

                        throw new Exception("Usuário " + user.UserName + " já está cadastrado!");

                    }

                }

                UserModel model = _context.Users.Where(m => m.Id == user.Id).First();

                model.LastUpdateDate = DateTime.Now;
                model.UserName = user.UserName;
                model.Name = user.Name;

                if (user.Password != "-") {
                    if (user.Password.Length >= 5) {
                        model.PasswordHash = _passwordService.GeneratePasswordHash(user.Password);
                    } else {
                        throw new Exception("A senha precisa ter pelo menos 5 caracteres");
                    }
                }

                _context.Entry(model).State = EntityState.Modified;

                _context.Users.Update(model);

                await _context.SaveChangesAsync();

                response.Message = "Usuário alterado com sucesso!";
                response.Data = model;

                return response;

            } catch (Exception ex) {

                response.Message = ex.Message;
                response.Successful = false;

                return response;

            }

        }


        public async Task<Response<UserModel>> DeleteUser(Guid id) {

            Response<UserModel> response = new();

            try {

                var userResp = await GetUser(id);

                UserModel? user = userResp.Data;

                if (user == null) throw new Exception(userResp.Message);

                if (user.Role.Id == (int)UserRole.Admin) {
                    throw new Exception("Não é permitido excluir um usuário administrador.");
                }

                var userWithHashResp = await GetUserWithHash(user.Id);

                if (!userWithHashResp.Successful) throw new Exception(userWithHashResp.Message);

                UserModel model = userWithHashResp.Data!;

                model.IsDeleted = true;
                model.LastUpdateDate = DateTime.Now;

                _context.Entry(model).State = EntityState.Modified;

                _context.Users.Update(model);

                await _context.SaveChangesAsync();

                response.Message = "Usuário excluído com sucesso!";
                response.Data = user;

                return response;

            } catch (Exception ex) {

                response.Message = ex.Message;
                response.Successful = false;

                return response;

            }

        }


        public async Task<Response<UserModel>> UndeleteUser(Guid id) {

            Response<UserModel> response = new();

            try {

                var userResp = await GetUser(id);

                UserModel? user = userResp.Data;

                if (user == null) throw new Exception(userResp.Message);

                var userWithHashResp = await GetUserWithHash(user.Id);

                if (!userWithHashResp.Successful) throw new Exception(userWithHashResp.Message);

                UserModel model = userWithHashResp.Data!;

                _context.Attach(model);

                model.IsDeleted = false;
                model.LastUpdateDate = DateTime.Now;

                _context.Entry(model).State = EntityState.Modified;

                _context.Users.Update(model);

                await _context.SaveChangesAsync();

                response.Message = "Usuário restaurado com sucesso!";
                response.Data = user;

                return response;

            } catch (Exception ex) {

                response.Message = ex.Message;
                response.Successful = false;

                return response;

            }

        }
    }


}

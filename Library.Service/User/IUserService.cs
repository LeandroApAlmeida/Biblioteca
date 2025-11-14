using Library.Db.Models;
using Library.Services.Model.Dto;

namespace Library.Services.User {


    /// <summary>
    /// Interface que define um serviço para manutenção de usuários.
    /// </summary>
    public interface IUserService {


        /// <summary>
        /// Obter a lista com os privilégios de acesso à aplicação.
        /// </summary>
        /// <returns>Lista com os privilégios de acesso à aplicação.</returns>
        public Task<Response<List<UserRoleModel>>> GetUserRoles();


        /// <summary>
        /// Obter o privilégio de acesso com o identificador chave primária 
        /// passado como parâmetro.
        /// </summary>
        /// <param name="id">Identificador chave primário do privilégio de acesso.</param>
        /// <returns>Privilégio de acesso associado.</returns>
        public Task<Response<UserRoleModel>> GetUserRole(int id);


        /// <summary>
        /// Obter a lista com todos os usuário cadastrados, sem carregar os hashs das senhas.
        /// </summary>
        /// <returns>Lista com todos os usuário cadastrados, sem os hashs das senhas.</returns>
        public Task<Response<List<UserModel>>> GetUsers();


        /// <summary>
        /// Obter o usuário com o identificador chave primária passado como parâmetro,
        /// sem carregar o hash da senha.
        /// </summary>
        /// <param name="id">Identificador chave primária do usuário.</param>
        /// <returns>Usuário associado.</returns>
        public Task<Response<UserModel>> GetUserWithoutHash(Guid id);


        /// <summary>
        /// Obter o usuário com o identificador chave primária passado como parâmetro,
        /// incluindo o hash da senha.
        /// </summary>
        /// <param name="id">Identificador chave primária do usuário.</param>
        /// <returns>Usuário associado.</returns>
        public Task<Response<UserModel>> GetUserWithHash(Guid id);


        /// <summary>
        /// Obter o usuário com o nome passado como parâmetro, incluindo o hash da senha.
        /// </summary>
        /// <param name="userName">Nome de usuário.</param>
        /// <returns>Usuário associado.</returns>
        public Task<Response<UserModel>> GetUserWithHash(string userName);


        /// <summary>
        /// Status de usuário administrador cadastrado.
        /// </summary>
        /// <returns>True, o administrador está cadastrado. False, o administrador não está
        /// cadastrado.</returns>
        public Task<Response<bool>> RegisteredAdmin();


        /// <summary>
        /// Cadastrar um novo usuário.
        /// </summary>
        /// <param name="user">Usuário a ser cadastrado.</param>
        /// <returns>Usuário cadastrado.</returns>
        public Task<Response<UserModel>> RegisterUser(UserDto user);


        /// <summary>
        /// Alterar o cadastro de um usuário.
        /// </summary>
        /// <param name="user">Usuário a ser alterado.</param>
        /// <returns>Usuário alterado.</returns>
        public Task<Response<UserModel>> EditUser(UserDto user);


        /// <summary>
        /// Excluir um usuário (não faz a remoção do registro no banco de dados). Ao excluir,
        /// não permite mais o acesso à aplicação via login.
        /// </summary>
        /// <param name="id">Identificador chave primária do usuário a ser excluído.</param>
        /// <returns>Usuário excluído.</returns>
        public Task<Response<UserModel>> DeleteUser(Guid id);


        /// <summary>
        /// Retornar um usuário excluído. Ao retornar, permite novamente o acesso à aplicação
        /// via login.
        /// </summary>
        /// <param name="id">Identificador chave primária do usuário a ser retornado.</param>
        /// <returns>Usuário retornado.</returns>
        public Task<Response<UserModel>> UndeleteUser(Guid id);


    }


}

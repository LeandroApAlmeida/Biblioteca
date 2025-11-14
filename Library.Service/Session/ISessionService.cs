using Library.Db.Models;

namespace Library.Services.Session {


    /// <summary>
    /// Interface que define um serviço para gerenciamento da sessão de usuário.
    /// </summary>
    public interface ISessionService {


        /// <summary>
        /// Criar uma sessão de usuário.
        /// </summary>
        /// <param name="user">Usuário em sessão.</param>
        /// <param name="ip">Endereço IP do computador do usuário.</param>
        /// <returns>Sessão ativa.</returns>
        public Task<Response<SessionModel>> CreateSession(UserModel user, string? ip);
        

        /// <summary>
        /// Encerra a sessão corrente.
        /// </summary>
        /// <returns>Sessão encerrada.</returns>
        public Task<Response<SessionModel>> RemoveSession();


        /// <summary>
        /// Obter os dados da sessão corrente.
        /// </summary>
        /// <returns>Dados da sessão corrente.</returns>
        public SessionModel? GetSessionData();


        /// <summary>
        /// Status de sessão ativa.
        /// </summary>
        /// <returns>True, há uma sessão ativa. False, não há uma sessão ativa.</returns>
        public bool IsSessionActive();


        /// <summary>
        /// Status de sessão de administrador.
        /// </summary>
        /// <returns>True, o usuário em sessão é administrador. False, o usuário em 
        /// sessão é o convidado.</returns>
        public bool IsAdminSession();


    }


}
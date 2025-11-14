using Library.Db.Models;

namespace Library.Services.Session {


    /// <summary>
    /// Interface que define um serviço de gerenciamento de log de sessão.
    /// </summary>
    public interface ILogService {


        /// <summary>
        /// Obter a lista com os logs de sessão gerados entre data inicial e data final.
        /// </summary>
        /// <param name="beginDate">data inicial do período.</param>
        /// <param name="endDate">data final do período.</param>
        /// <returns>Lista com os logs de sessão gerados entre data inicial e data final.</returns>
        public Task<Response<List<SessionModel>>> GetSessionLog(DateTime beginDate,
        DateTime endDate);


    }


}
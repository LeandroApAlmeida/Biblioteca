using Library.Db.Models;

namespace Library.Services.Session {


    public interface ISessionService {

        public Task<Response<SessionModel>> CreateSession(UserModel user, string? ip);

        public Task<Response<SessionModel>> RemoveSession();

        public SessionModel? GetSessionData();

        public bool IsSessionActive();

        public bool IsAdminSession();

    }


}
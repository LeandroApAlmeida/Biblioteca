using Library.Models;
using Microsoft.AspNetCore.Mvc;

namespace Library.Services.SessionService {


    public interface ISessionService {

        public Task<ResponseModel<SessionModel>> CreateSession(UserModel user);

        public Task<ResponseModel<SessionModel>> RemoveSession();

        public SessionModel? GetSessionData();

        public bool IsTheSessionActive();

        public void SetLayout(Controller c);

    }


}
using Library.Models;
using Microsoft.AspNetCore.Mvc;
using System.Net;

namespace Library.Services.SessionService {


    public interface ISessionService {

        public Task<ResponseModel<SessionModel>> CreateSession(UserModel user, string? ip);

        public Task<ResponseModel<SessionModel>> RemoveSession();

        public SessionModel? GetSessionData();

        public bool IsSessionActive();

        public bool IsAdminSession();

        public void SetLayout(Controller c);

    }


}
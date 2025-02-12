using Library.Models;
using Microsoft.AspNetCore.Mvc;
using System.Net;

namespace Library.Services.SessionService {


    public interface ISessionService {

        public Task<Response<SessionModel>> CreateSession(UserModel user, string? ip);

        public Task<Response<SessionModel>> RemoveSession();

        public SessionModel? GetSessionData();

        public bool IsSessionActive();

        public bool IsAdminSession();

    }


}
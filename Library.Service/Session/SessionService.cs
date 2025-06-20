using Library.Data;
using Library.Db.Models;
using Library.Services.User;
using Microsoft.AspNetCore.Http;
using Microsoft.EntityFrameworkCore;
using Newtonsoft.Json;

namespace Library.Services.Session {


    public class SessionService : ISessionService {


        private readonly IHttpContextAccessor _contextAccessor;

        private readonly ApplicationDbContext _context;


        public SessionService(IHttpContextAccessor contextAccessor, ApplicationDbContext context) {
            _contextAccessor = contextAccessor;
            _context = context;
        }


        public async Task<Response<SessionModel>> CreateSession(UserModel user, string? ip) {

            Response<SessionModel> response = new();

            try {

                _context.Attach(user);

                SessionModel session = new SessionModel() {
                    Id = Guid.NewGuid(),
                    Ip = ip,
                    User = user,
                    LoginDate = DateTime.Now,
                    LogoutDate = DateTime.Now
                };

                _context.Sessions.Add(session);

                await _context.SaveChangesAsync();

                var sessionJson = JsonConvert.SerializeObject(session);

                _contextAccessor.HttpContext!.Session.SetString("user_session", sessionJson);

                response.Data = session;

                return response;

            } catch (Exception ex) {

                response.Message = ex.Message;
                response.Successful = false;

                return response;

            }

        }



        public async Task<Response<SessionModel>> RemoveSession() {

            Response<SessionModel> response = new();

            try {

                SessionModel sessionData = GetSessionData()!;

                _contextAccessor.HttpContext!.Session.Remove("user_session");

                _context.Attach(sessionData!);

                sessionData.LogoutDate = DateTime.Now;

                _context.Entry(sessionData).State = EntityState.Modified;

                _context.Sessions.Update(sessionData);

                await _context.SaveChangesAsync();

                response.Data = sessionData;

                return response;

            } catch (Exception ex) {

                response.Message = ex.Message;

                response.Successful = false;

                response.Data = null;

                return response;

            }

        }



        public SessionModel? GetSessionData() {

            var sessionData = _contextAccessor.HttpContext!.Session.GetString("user_session");

            if (!string.IsNullOrEmpty(sessionData)) {
                return JsonConvert.DeserializeObject<SessionModel>(sessionData);
            } else {
                return null;
            }

        }


        public bool IsSessionActive() {
            
            var sessionData = GetSessionData();

            return sessionData != null;

        }


        public bool IsAdminSession() {

            var sessionData = GetSessionData();

            if (sessionData != null) {
                return sessionData.User.Role.Id == (int) UserRole.Admin;
            } else {
                return false;
            }
            
        }

        
    }


}
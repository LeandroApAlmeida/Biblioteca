﻿using Library.Data;
using Library.Models;
using Library.Services.UserService;
using Library.Utils;
using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;
using Newtonsoft.Json;

namespace Library.Services.SessionService {


    public class SessionService : ISessionService {


        private readonly IHttpContextAccessor _contextAccessor;

        private readonly ApplicationDbContext _context;


        public SessionService(IHttpContextAccessor contextAccessor, ApplicationDbContext context) {
            _contextAccessor = contextAccessor;
            _context = context;
        }


        public async Task<ResponseModel<SessionModel>> CreateSession(UserModel user) {

            ResponseModel<SessionModel> response = new();

            try {

                _context.Attach(user);

                SessionModel session = new SessionModel() {
                    Id = Guid.NewGuid(),
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



        public async Task<ResponseModel<SessionModel>> RemoveSession() {

            ResponseModel<SessionModel> response = new();

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

        public bool IsTheSessionActive() {
            
            var sessionData = GetSessionData();

            return sessionData != null;

        }

        public void SetLayout(Controller c) {
            c.ViewBag.Layout = GetSessionData()!.User.Role.Id switch {
                (int)UserRole.Admin => Constants.ADMIN_LAYOUT,
                (int)UserRole.Guest => Constants.DEFAULT_LAYOUT,
                _ => Constants.ADMIN_LAYOUT,
            };
        }


    }


}
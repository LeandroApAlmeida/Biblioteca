using Library.Data;
using Library.Db.Models;
using Microsoft.EntityFrameworkCore;

namespace Library.Services.Session {


    /// <summary>
    /// Classe para gerenciamento dos logs de sessão.
    /// </summary>
    public class LogService : ILogService {


        /// <summary> Objeto para acesso ao banco de dados. </summary>
        private readonly ApplicationDbContext _context;


        public LogService(ApplicationDbContext context) {
            _context = context;
        }


        public async Task<Response<List<SessionModel>>> GetSessionLog(DateTime beginDate, DateTime endDate) {


            Response<List<SessionModel>> response = new();

            try {

                List<SessionModel> sessions = await _context.Sessions
                .Select(s => new SessionModel {
                    
                    Id = s.Id,

                    Ip = s.Ip,
                    
                    LoginDate = s.LoginDate,
                    
                    LogoutDate = s.LogoutDate,
                    
                    User = new UserModel() {
                    
                        Id = s.User.Id,
                        
                        Name = s.User.Name,
                        
                        UserName = s.User.Name,
                        
                        PasswordHash = new byte[0],
                        
                        RegistrationDate = s.User.RegistrationDate,
                        
                        LastUpdateDate = s.User.LastUpdateDate,
                        
                        IsActive = s.User.IsActive,
                        
                        IsDeleted = s.User.IsDeleted,
                        
                        Role = new UserRoleModel() {
                            Id = s.User.Role.Id,
                            Description = s.User.Role.Description
                        }

                    }

                })
                .Where(s =>
                    s.LoginDate >= beginDate && s.LoginDate <= endDate 
                )
                .OrderByDescending(s => s.LoginDate)
                .AsNoTracking()
                .ToListAsync();

                response.Data = sessions;

                return response;

            } catch (Exception ex) {

                response.Message = ex.Message;
                response.Successful = false;

                return response;

            }


        }


    }


}

using Library.Db.Models;

namespace Library.Services.Session {


    public interface ILogService {


        public Task<Response<List<SessionModel>>> GetSessionLog(DateTime beginDate,
        DateTime endDate);


    }


}

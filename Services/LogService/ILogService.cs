using Library.Models;

namespace Library.Services.LogService {


    public interface ILogService {


        public Task<Response<List<SessionModel>>> GetSessionLog(DateTime beginDate,
        DateTime endDate);


    }


}

using Library.Models;

namespace Library.Services.LogService {


    public interface ILogService {


        public Task<ResponseModel<List<SessionModel>>> GetSessionLog(DateTime beginDate,
        DateTime endDate);


    }


}

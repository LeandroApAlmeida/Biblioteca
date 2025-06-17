using Library.Db.Models;

namespace Library.Services.Collection {


    public interface IPersonService {

        public Task<Response<List<PersonModel>>> GetPersons();

        public Task<Response<List<PersonModel>>> GetDeletedPersons();

        public Task<Response<PersonModel?>> GetPerson(Guid id);

        public Task<Response<PersonModel>> RegisterPerson(PersonModel person);

        public Task<Response<PersonModel>> EditPerson(PersonModel person);

        public Task<Response<PersonModel>> DeletePerson(Guid id);

        public Task<Response<PersonModel>> UndeletePerson(Guid id);

    }


}
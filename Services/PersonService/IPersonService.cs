using Library.Models;

namespace Library.Services.PersonService {


    public interface IPersonService {

        public Task<Response<List<PersonModel>>> GetPersons();

        public Task<Response<PersonModel?>> GetPerson(Guid id);

        public Task<Response<PersonModel>> RegisterPerson(PersonModel person);

        public Task<Response<PersonModel>> EditPerson(PersonModel person);

        public Task<Response<PersonModel>> DeletePerson(PersonModel person);

    }


}
using Library.Models;

namespace Library.Services.PersonService {
    
    
    public interface IPersonService {

        public Task<ResponseModel<List<PersonModel>>> GetPersons();

        public Task<ResponseModel<PersonModel?>> GetPerson(Guid id);

        public Task<ResponseModel<PersonModel>> RegisterPerson(PersonModel person);

        public Task<ResponseModel<PersonModel>> EditPerson(PersonModel person);

        public Task<ResponseModel<PersonModel>> DeletePerson(PersonModel person);

    }


}
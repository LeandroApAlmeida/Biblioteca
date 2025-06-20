using Library.Data;
using Library.Db.Models;
using Microsoft.EntityFrameworkCore;

namespace Library.Services.Collection {


    public class PersonService : IPersonService {


        private readonly ApplicationDbContext _context;


        public PersonService(ApplicationDbContext context) {
            _context = context;
        }


        public async Task<Response<List<PersonModel>>> GetPersons() {

            Response<List<PersonModel>> response = new();

            try {

                List<PersonModel> persons = await _context.Persons
                .Select(p => p)
                .Where(p => p.IsDeleted == false)
                .OrderBy(p => p.Name)
                .AsNoTracking()
                .ToListAsync();

                response.Data = persons;

                return response;

            } catch (Exception ex) {

                response.Data = null;
                response.Message = ex.Message;
                response.Successful = false;

                return response;

            }

        }


        public async Task<Response<List<PersonModel>>> GetDeletedPersons() {

            Response<List<PersonModel>> response = new();

            try {

                List<PersonModel> persons = await _context.Persons
                .Select(p => p)
                .Where(p => p.IsDeleted == true)
                .OrderBy(p => p.Name)
                .AsNoTracking()
                .ToListAsync();

                response.Data = persons;

                return response;

            } catch (Exception ex) {

                response.Data = null;
                response.Message = ex.Message;
                response.Successful = false;

                return response;

            }

        }


        public async Task<Response<PersonModel?>> GetPerson(Guid id) {

            Response<PersonModel?> response = new();

            try {

                if (id != Guid.Empty) {

                    List<PersonModel> persons = await _context.Persons
                    .Where(p => p.Id == id)
                    .AsNoTracking()
                    .ToListAsync();

                    if (persons != null && persons.Count > 0) {

                        PersonModel? person = persons.First();

                        response.Data = person;

                        return response;

                    } else {

                        throw new Exception("Pessoa não encontrada!");

                    }

                } else {

                    throw new Exception("Identificador da pessoa inválido.");

                }

            } catch (Exception ex) {

                response.Data = null;
                response.Message = ex.Message;
                response.Successful = false;

                return response;

            }

        }


        public async Task<Response<PersonModel>> RegisterPerson(PersonModel person) {

            Response<PersonModel> response = new();

            try {

                person.Id = Guid.NewGuid();
                person.RegistrationDate = DateTime.Now;
                person.LastUpdateDate = person.RegistrationDate;

                _context.Persons.Add(person);

                await _context.SaveChangesAsync();

                response.Message = "Pessoa cadastrada com sucesso!";
                response.Data = person;

                return response;

            } catch (Exception ex) {

                response.Data = null;
                response.Message = ex.Message;
                response.Successful = false;

                return response;

            }

        }


        public async Task<Response<PersonModel>> EditPerson(PersonModel person) {

            Response<PersonModel> response = new();

            try {

                _context.Attach(person);

                person.LastUpdateDate = DateTime.Now;

                _context.Entry(person).State = EntityState.Modified;

                _context.Persons.Update(person);

                await _context.SaveChangesAsync();

                response.Message = "Pessoa alterada com sucesso!";
                response.Data = person;

                return response;

            } catch (Exception ex) {

                response.Message = ex.Message;
                response.Successful = false;

                return response;

            }

        }


        public async Task<Response<PersonModel>> DeletePerson(Guid id) {

            Response<PersonModel> response = new();

            try {

                var personResp = await GetPerson(id);

                PersonModel? person = personResp.Data;

                if (person == null) throw new Exception(personResp.Message);

                person.IsDeleted = true;
                person.LastUpdateDate = DateTime.Now;

                _context.Entry(person).State = EntityState.Modified;

                _context.Persons.Update(person);

                await _context.SaveChangesAsync();

                response.Message = "Pessoa excluída com sucesso!";
                response.Data = person;

                return response;

            } catch (Exception ex) {

                response.Message = ex.Message;
                response.Successful = false;

                return response;

            }

        }


        public async Task<Response<PersonModel>> UndeletePerson(Guid id) {

            Response<PersonModel> response = new();

            try {

                var personResp = await GetPerson(id);

                PersonModel? person = personResp.Data;

                if (person == null) throw new Exception(personResp.Message);

                person.IsDeleted = false;
                person.LastUpdateDate = DateTime.Now;

                _context.Entry(person).State = EntityState.Modified;

                _context.Persons.Update(person);

                await _context.SaveChangesAsync();

                response.Message = "Pessoa restaurada com sucesso!";
                response.Data = person;

                return response;

            } catch (Exception ex) {

                response.Message = ex.Message;
                response.Successful = false;

                return response;

            }

        }


    }


}

using Library.Data;
using Library.Models;
using Library.Services.CollectionService;
using Microsoft.EntityFrameworkCore;

namespace Library.Services.PersonService {


    public class PersonService : IPersonService {


        private readonly ApplicationDbContext _context;


        public PersonService(ApplicationDbContext context) {
            _context = context;
        }


        public async Task<Response<List<PersonModel>>> GetPersons() {

            Response<List<PersonModel>> response = new();

            try {

                List<PersonModel> persons = await _context.Persons
                .Select(b => b)
                .Where(b => b.IsDeleted == false)
                .OrderBy(b => b.Name)
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
                    .Where(p => p.Id == id && p.IsDeleted == false)
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


        public async Task<Response<PersonModel>> DeletePerson(PersonModel person) {

            Response<PersonModel> response = new();

            try {

                _context.Attach(person);

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


    }


}

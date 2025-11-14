using Library.Db.Models;

namespace Library.Services.Collection {


    /// <summary>
    /// Interface que define um serviço para manutenção de pessoas.
    /// </summary>
    public interface IPersonService {


        /// <summary>
        /// Obter a lista com as pessoas cadastradas.
        /// </summary>
        /// <returns>Lista com as pessoas cadastradas.</returns>
        public Task<Response<List<PersonModel>>> GetPersons();


        /// <summary>
        /// Obter a lista com as pessoas excluídas.
        /// </summary>
        /// <returns>Lista com as pessoas excluídas.</returns>
        public Task<Response<List<PersonModel>>> GetDeletedPersons();


        /// <summary>
        /// Obter a pessoa com o identificador chave primária passado como parâmetro.
        /// </summary>
        /// <param name="id">Identificador chave primária da pessoa</param>
        /// <returns>Pessoa associada ao identificador chave primária.</returns>
        public Task<Response<PersonModel?>> GetPerson(Guid id);


        /// <summary>
        /// Cadastrar uma nova pessoa.
        /// </summary>
        /// <param name="person">Pessoa a ser cadastrada.</param>
        /// <returns>Pessoa cadastrada.</returns>
        public Task<Response<PersonModel>> RegisterPerson(PersonModel person);


        /// <summary>
        /// Alterar o cadastro de uma pessoa.
        /// </summary>
        /// <param name="book">Pessoa a ser alterada.</param>
        /// <returns>Pessoa alterada.</returns>
        public Task<Response<PersonModel>> EditPerson(PersonModel person);


        /// <summary>
        /// Excluir uma pessoa (não faz a remoção do registro no banco de dados).
        /// </summary>
        /// <param name="id">Identificador chave primária da pessoa a ser excluída.</param>
        /// <returns>Pessoa excluída.</returns>
        public Task<Response<PersonModel>> DeletePerson(Guid id);


        /// <summary>
        /// Retornar uma pessoa excluída.
        /// </summary>
        /// <param name="id">Identificador chave primária da pessoa a ser retornada.</param>
        /// <returns>Pessoa retornada.</returns>
        public Task<Response<PersonModel>> UndeletePerson(Guid id);


    }


}
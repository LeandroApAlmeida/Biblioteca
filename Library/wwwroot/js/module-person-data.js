
/**
 * Módulo para tratamento de campos ocultos de uma pessoa na página.
 */


/**
 * Atualiza os dados de uma pessoa selecionada nos campos ocultos da página.
 * 
 * @param {any} document página a ser atualizada.
 * @param {any} person dados da pessoa.
 */
function setPersonData(document, person) {
    document.getElementsByName("Person.Id")[0].value = person.Id;
    document.getElementsByName("Person.Name")[0].value = person.Name;
    document.getElementsByName("Person.City")[0].value = person.City;
    document.getElementsByName("Person.FederalState")[0].value = person.FederalState;
    document.getElementsByName("Person.Street")[0].value = person.Street;
    document.getElementsByName("Person.Number")[0].value = person.Number;
    document.getElementsByName("Person.District")[0].value = person.District;
    document.getElementsByName("Person.PostalCode")[0].value = person.PostalCode;
    document.getElementsByName("Person.Complement")[0].value = person.Complement;
    document.getElementsByName("Person.Country")[0].value = person.Country;
    document.getElementsByName("Person.Description")[0].value = person.Description;
    document.getElementsByName("Person.RegistrationDate")[0].value = person.RegistrationDate;
    document.getElementsByName("Person.LastUpdateDate")[0].value = person.LastUpdateDate;
    document.getElementsByName("Person.IsDeleted")[0].value = person.IsDeleted;
}


export { setPersonData };
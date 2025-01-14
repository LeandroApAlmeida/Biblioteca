
// Tratador de eventos da página ~\Views\Donation\Edit.cshtml. É feita a conversão da lista
// de objetos PersonModel que contém os cadastros de todas as pessoas para o formato Json
// na Razor Page, e passado este Json para o parâmetro "window.personsData". Da mesma forma,
// é feita a conversão do objeto de PersonModel que representa a pessoa para quem foi feita a
// doação do livro para o formato Json, e passado para o parâmetro "window.personData".
//
// Quando a página é carregada, primeiramente é identificada a pessoa para quem foi feita a
// doação e seleciona esta pessoa no seletor, carregando os dados sobre a mesma nos campos
// correspondentes da página. Feito isso, é atribuído o tratador de eventos no seletor de pessoa,
// de tal forma que, quando for selecionada uma outra pessoa, os respectivos campos da página
// sejam atualizados.


// Campo seletor de pessoas.
var selector = document.getElementById('select-person');

// Recupera o Json com a lista de pessoas.
var personsData = window.personsData;

// Recupera o Json com a pessoa que recebeu a doação.
var personData = window.personData;


// Seleciona no seletor de pessoas o índice da pessoa para quem foi feita a doação e atualiza os
// dados da mesma na página.

for (var i = 0; i < personsData.length; i++) {

    var person = personsData[i];

    if (person.Id == personData.Id) {

        selector.options.selectedIndex = i;

        setPerson(person);

        break;

    }

}


// Atribui o tratador de eventos ao seletor de pessoas.
selector.addEventListener('change', onSelectedPerson);


/**
 * No evento do seletor, basicamente identifica a pessoa que o usuário selecionou
 * e atualiza os dados da mesma na página.
 */
function onSelectedPerson() {

    var jsonData = window.personsData;

    for (var i = 0; i < jsonData.length; i++) {

        var person = jsonData[i];

        if (person.Id == selector.value) {

            setPerson(person);

            break;

        }

    }

}


/**
 * Atualiza os dados da pessoa selecionada na página.
 * 
 * @param {any} person pessoa selecionada.
 */
function setPerson(person) {

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
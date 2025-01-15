
// Tratador de eventos da página ~\Views\Loan\Edit.cshtml. É feita a conversão da lista
// de objetos PersonModel que contém os cadastros de todas as pessoas para o formato Json
// na Razor Page, e passado este Json para o parâmetro "window.personsData". Da mesma forma,
// é feita a conversão do objeto de PersonModel que representa a pessoa para quem foi feito o
// empréstimo do livro para o formato Json, e passado para o parâmetro "window.personData".
//
// Quando a página é carregada, primeiramente é identificada a pessoa para quem foi feito o
// empréstimo e seleciona esta pessoa no seletor, carregando os dados sobre a mesma nos campos
// correspondentes da página. Feito isso, é atribuído o tratador de eventos no seletor de pessoa,
// de tal forma que, quando for selecionada uma outra pessoa, os respectivos campos da página
// sejam atualizados.


import { setPersonData } from "./data-person.js"


// Campo seletor de pessoas.
var selector = document.getElementById('select-person');

// Recupera o Json com a lista de pessoas.
var personsData = window.personsData;

// Recupera o Json com a pessoa que obteve o empréstimo.
var personData = window.personData;


// Seleciona no seletor de pessoas o índice da pessoa para quem foi feito o empréstimo e atualiza
// os dados da mesma na página.

for (var i = 0; i < personsData.length; i++) {

    var person = personsData[i];

    if (person.Id == personData.Id) {

        selector.options.selectedIndex = i;

        setPersonData(document, person);

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

            setPersonData(document, person);

            break;

        }

    }

}
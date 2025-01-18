
// Tratador de eventos da página ~\Views\Loan\Edit.cshtml.


import { setPersonData } from "./data-person.js"


// Campo seletor de pessoas.
var selector = document.getElementById('select-person');

// Recupera o Json com a lista de pessoas.
var personsData = window.personsData;

// Recupera o Json com a pessoa que obteve o empréstimo.
var personData = window.personData;


// Seleciona o índice da pessoa para quem foi feito o empréstimo no seletor e atualiza
// os dados da mesma na página.

for (var i = 0; i < personsData.length; i++) {

    var person = personsData[i];

    if (person.Id == personData.Id) {

        selector.options.selectedIndex = i;

        setPersonData(document, person);

        break;

    }

}


// Atribui o tratador de eventos ao seletor de pessoas. No evento do seletor, basicamente
// identifica a pessoa que o usuário selecionou e atualiza os dados da mesma na página.
selector.addEventListener('change', function () {

    var jsonData = window.personsData;

    for (var i = 0; i < jsonData.length; i++) {

        var person = jsonData[i];

        if (person.Id == selector.value) {

            setPersonData(document, person);

            break;

        }

    }

});
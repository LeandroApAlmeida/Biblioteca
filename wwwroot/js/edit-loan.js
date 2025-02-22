
/*
    Tratador de eventos da página de edição de um empréstimo (~\Views\Loan\Edit.cshtml).
*/


import { setPersonData } from "./module-person-data.js"


document.addEventListener('DOMContentLoaded', function () {

    var selector = document.getElementById('select-person');

    var personsData = window.personsData;
    var personData = window.personData;

    for (var i = 0; i < personsData.length; i++) {

        var person = personsData[i];

        if (person.Id == personData.Id) {

            selector.options.selectedIndex = i;

            setPersonData(document, person);

            break;

        }

    }

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

});
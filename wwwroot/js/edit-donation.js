
/*
    Tratador de eventos da página de edição de uma doação (~\Views\Donation\Edit.cshtml).
*/


import { setPersonData } from "./module-person-data.js"


document.addEventListener('DOMContentLoaded', function () {

    var personSelector = document.getElementById('select-person');

    personSelector.focus();

    var personsData = window.personsData;
    var personData = window.personData;

    for (var i = 0; i < personsData.length; i++) {

        var person = personsData[i];

        if (person.Id == personData.Id) {

            personSelector.options.selectedIndex = i;

            setPersonData(document, person);

            break;

        }

    }

    personSelector.addEventListener('change', function () {

        var jsonData = window.personsData;

        for (var i = 0; i < jsonData.length; i++) {

            var person = jsonData[i];

            if (person.Id == personSelector.value) {

                setPersonData(document, person);

                break;

            }

        }

    });

    document.getElementById('donate-book-form').addEventListener('submit', function (event) {

        document.getElementById('spinner').style.display = 'block';
        document.body.style.overflow = 'hidden';

        document.getElementById('img-preview').setAttribute('readonly', 'readonly');
        document.getElementById('select-person').setAttribute('readonly', 'readonly');
        document.getElementById('donation-date').setAttribute('readonly', 'readonly');
        document.getElementById('notes').setAttribute('readonly', 'readonly');

        document.getElementById('save-button').disabled = true;
        document.getElementById('cancel-button').classList.add('disabled');

    });

    document.addEventListener('keydown', function (event) {
        if (event.ctrlKey && event.key.toLowerCase() === 's') {
            event.preventDefault();
            document.getElementById('save-button').click();
        } else if (event.key === 'Escape') {
            event.preventDefault();
            document.getElementById('cancel-button').click();
        }
    });

});

/*
    Tratador de eventos da página de cadastro de empréstimo (~\Views\Loan\Register.cshtml).
*/


import { formatToDatetimeLocal } from "./module-datetime.js"
import { setPersonData } from "./module-person-data.js"


document.addEventListener('DOMContentLoaded', function () {

    var personSelector = document.getElementById('select-person');

    personSelector.focus();

    personSelector.addEventListener('change', onSelectedPerson);

    document.getElementById("loan-date").value = formatToDatetimeLocal(new Date());

    personSelector.dispatchEvent(new Event('change'));

    document.getElementById('borrow-book-form').addEventListener('submit', function (event) {

        document.getElementById('spinner').style.display = 'block';
        document.body.style.overflow = 'hidden';

        document.getElementById('img-preview').setAttribute('readonly', 'readonly');
        document.getElementById('select-person').setAttribute('readonly', 'readonly');
        document.getElementById('loan-date').setAttribute('readonly', 'readonly');
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


function onSelectedPerson(event) {

    var personsData = window.personsData;

    for (var i = 0; i < personsData.length; i++) {

        var person = personsData[i];

        if (person.Id == event.target.value) {

            setPersonData(document, person);

            break;

        }

    }

}
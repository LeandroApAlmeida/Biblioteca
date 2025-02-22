
/*
    Tratador de eventos da página de cadastro de doação (~\Views\Donation\Register.cshtml).
*/


import { formatToDatetimeLocal } from "./module-datetime.js"
import { setPersonData } from "./module-person-data.js"


document.addEventListener('DOMContentLoaded', function () {

    var selector = document.getElementById("select-person");

    selector.addEventListener('change', onSelectedPerson);

    document.getElementById("donation-date").value = formatToDatetimeLocal(new Date());

    selector.dispatchEvent(new Event('change'));

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

import { formatToDatetimeLocal } from "./utils.js"
import { setPersonData } from "./data-person.js"


document.addEventListener('DOMContentLoaded', function () {

    var selector = document.getElementById('select-person');

    selector.addEventListener('change', onSelectedPerson);

    document.getElementById("loan-date").value = formatToDatetimeLocal(new Date());

    selector.dispatchEvent(new Event('change'));

});


function onSelectedPerson(event) {

    var jsonData = window.personsData;

    for (var i = 0; i < jsonData.length; i++) {

        var person = jsonData[i];

        if (person.Id == event.target.value) {

            setPersonData(document, person);

            break;

        }

    }

}
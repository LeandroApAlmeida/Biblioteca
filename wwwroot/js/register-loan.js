import { formatToDatetimeLocal } from "./utils.js"
import { setPersonData } from "./data-person.js"


var selector = document.getElementById('select-person');
selector.addEventListener('change', onSelectedPerson);

document.getElementById("loan-date").value = formatToDatetimeLocal(new Date());


onSelectedPerson();


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
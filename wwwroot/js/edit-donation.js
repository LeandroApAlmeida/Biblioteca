var selector = document.getElementById('select-person');

var personsData = window.personsData;
var personData = window.personData;

for (var i = 0; i < personsData.length; i++) {

    var person = personsData[i];

    if (person.Id == personData.Id) {

        selector.options.selectedIndex = i;

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

        break;

    }

}


selector.addEventListener('change', onSelectedPerson);


function onSelectedPerson() {

    var jsonData = window.personsData;

    for (var i = 0; i < jsonData.length; i++) {

        var person = jsonData[i];

        if (person.Id == selector.value) {

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

            break;

        }

    }

}
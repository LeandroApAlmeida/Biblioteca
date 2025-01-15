
import { formatToDatetimeLocal } from "./utils.js"
import { setPersonData } from "./data-person.js"
import { setBookData } from "./data-book.js"


var personSelector = document.getElementById('select-person');
personSelector.addEventListener('change', onSelectedPerson);

var bookSelector = document.getElementById('select-book');
bookSelector.addEventListener('change', onSelectedBook);

document.getElementById("donation-date").value = formatToDatetimeLocal(new Date());


onSelectedPerson();

onSelectedBook();


function onSelectedPerson() {

    var jsonData = window.personsData;

    for (var i = 0; i < jsonData.length; i++) {

        var person = jsonData[i];

        if (person.Id == personSelector.value) {

            setPersonData(document, person);

            break;

        }

    }

}


function onSelectedBook() {

    var jsonData = window.booksData;

    for (var i = 0; i < jsonData.length; i++) {

        var book = jsonData[i];

        if (book.Id == bookSelector.value) {

            const {
                host,
                hostname,
                href,
                origin,
                pathname,
                port,
                protocol,
                search
            } = window.location

            const url = `${origin}/api/CollectionApi/cover/${book.Id}`;
            fetch(url).then(response => {

                if (!response.ok) { throw new Error('Erro ao buscar os dados'); }

                return response.json();

            }).then(cover => {

                const imgPreview = document.getElementById("img-preview");
                imgPreview.style.display = "block";
                imgPreview.innerHTML = '<img src="' + cover + '" class="center"/>';
                imgPreview.removeAttribute("hidden");

                setBookData(document, book, cover);

            }).catch(error => {

                alert(error);

            });

            break;

        }

    }

}
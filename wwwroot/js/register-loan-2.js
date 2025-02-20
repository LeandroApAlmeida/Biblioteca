
import { formatToDatetimeLocal } from "./utils.js"
import { setPersonData } from "./data-person.js"
import { setBookData } from "./data-book.js"


document.addEventListener('DOMContentLoaded', function () {

    var personSelector = document.getElementById('select-person');
    var bookSelector = document.getElementById('select-book');

    personSelector.addEventListener('change', onSelectedPerson);
    bookSelector.addEventListener('change', onSelectedBook);

    document.getElementById("loan-date").value = formatToDatetimeLocal(new Date());

    personSelector.dispatchEvent(new Event('change'));
    bookSelector.dispatchEvent(new Event('change'));

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


function onSelectedBook(event) {

    var jsonData = window.booksData;

    for (var i = 0; i < jsonData.length; i++) {

        var book = jsonData[i];

        if (book.Id == event.target.value) {

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

            const url = `${origin}/api/CollectionApi/GetBookCover/${book.Id}`;
            fetch(url).then(response => {

                if (!response.ok) { throw new Error('Erro ao recuperar a capa do livro.'); }

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
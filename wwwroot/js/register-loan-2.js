
/*
    Tratador de eventos da página de cadastro de empréstimo (2) (~\Views\Loan\Register2.cshtml).
*/


import { formatToDatetimeLocal } from "./module-datetime.js"
import { setPersonData } from "./module-person-data.js"
import { setBookData } from "./module-book-data.js"


document.addEventListener('DOMContentLoaded', function () {

    var personSelector = document.getElementById('select-person');
    var bookSelector = document.getElementById('select-book');

    bookSelector.focus();

    personSelector.addEventListener('change', onSelectedPerson);
    bookSelector.addEventListener('change', onSelectedBook);

    document.getElementById("loan-date").value = formatToDatetimeLocal(new Date());

    personSelector.dispatchEvent(new Event('change'));
    bookSelector.dispatchEvent(new Event('change'));

    document.getElementById('borrow-book-form').addEventListener('submit', function (event) {

        document.getElementById('spinner').style.display = 'block';
        document.body.style.overflow = 'hidden';

        document.getElementById('select-book').setAttribute('readonly', 'readonly');
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


function onSelectedBook(event) {

    var booksData = window.booksData;

    for (var i = 0; i < booksData.length; i++) {

        var book = booksData[i];

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

            const url = `${origin}/Api/CollectionApi/GetBookCover/${encodeURIComponent(book.Id)}`;
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
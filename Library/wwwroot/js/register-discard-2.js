
/*
    Tratador de eventos da página de cadastro de livro descartado (2) (~\Views\Discard\Register2.cshtml).
    A capa do livro selecionado na página será recuperada pela chamada de api "/CollectionApi/GetBookCover/",
    pois os livros recuperados na lista todos estão com o campo capa definido como "", por uma questão de
    melhor eficiência na recuperação da lista de livros na página.
*/


import { formatToDatetimeLocal } from "./module-datetime.js"
import { setBookData } from "./module-book-data.js"


document.addEventListener('DOMContentLoaded', function () {


    var selector = document.getElementById('select-book');

    selector.addEventListener('change', onSelectedBook);

    selector.dispatchEvent(new Event('change'));

    document.getElementById("select-book").focus();

    document.getElementById("discard-date").value = formatToDatetimeLocal(new Date());


    document.getElementById('discard-book-form').addEventListener('submit', function (event) {

        document.getElementById('spinner').style.display = 'block';
        document.body.style.overflow = 'hidden';

        document.getElementById('select-book').setAttribute('readonly', 'readonly');
        document.getElementById('img-preview').setAttribute('readonly', 'readonly');
        document.getElementById('discard-date').setAttribute('readonly', 'readonly');
        document.getElementById('reason').setAttribute('readonly', 'readonly');

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
                imgPreview.innerHTML = '<img src="' + cover.Data + '" class="center"/>';
                imgPreview.removeAttribute("hidden");

                setBookData(document, book, cover);

            }).catch(error => {

                alert(error);

            });

            break;

        }

    }

}
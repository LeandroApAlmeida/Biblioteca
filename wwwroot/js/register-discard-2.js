﻿
/*
    Tratador de eventos da página de cadastro de livro descartado (2) (~\Views\Discard\Register2.cshtml).
    A capa do livro selecionado na página será recuperada pela chamada de api "/CollectionApi/GetBookCover/",
    pois os livros recuperados na lista todos estão com o campo capa definido como "", por uma questão de
    melhor eficiência na recuperação da lista de livros.
*/


import { formatToDatetimeLocal } from "./utils.js"
import { setBookData } from "./data-book.js"


document.addEventListener('DOMContentLoaded', function () {

    var selector = document.getElementById('select-book');

    document.getElementById("discard-date").value = formatToDatetimeLocal(new Date());

    selector.addEventListener('change', onSelectedBook);

    selector.dispatchEvent(new Event('change'));

});

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
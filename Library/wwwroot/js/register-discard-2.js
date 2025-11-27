
/**
 * Tratador de eventos da página de cadastro de livro descartado (2) (~\Views\Discard\Register2.cshtml).
 * A capa do livro selecionado na página será recuperada pela chamada de api "Api/CollectionApi/GetBookCover/",
 * pois os livros recuperados na lista todos estão com o campo capa definido como "", por uma questão de
 * melhor eficiência na recuperação da lista de livros na página.
 */


import { formatToDatetimeLocal } from "./module-datetime.js"
import { setBookData } from "./module-book-data.js"


document.addEventListener('DOMContentLoaded', function () {


    // Componente para seleção do livro a ser descartado.
    var bookSelector = document.getElementById('select-book');

    // Atribui o tratador de evento ao componente.
    bookSelector.addEventListener('change', onSelectedBook);

    // Força a execução do evento no componente, para atualizar o livro na página.
    bookSelector.dispatchEvent(new Event('change'));

    // Move o foco para o componente.
    bookSelector.focus();

    // Atribui a data atual ao campo Data do Descarte.
    document.getElementById("discard-date").value = formatToDatetimeLocal(new Date());


    /**
     * Evento disparado ao submeter o form. Neste caso, desabilita os campos de edição, os
     * botões e exibe o spinner, mostrando que há um processo em execução.
     */
    document.getElementById('discard-book-form').addEventListener('submit', function (event) {

        // Exibe o spinner.
        document.getElementById('spinner').style.display = 'block';
        document.body.style.overflow = 'hidden';

        // Desabilita os campos da página.
        document.getElementById('select-book').setAttribute('readonly', 'readonly');
        document.getElementById('img-preview').setAttribute('readonly', 'readonly');
        document.getElementById('discard-date').setAttribute('readonly', 'readonly');
        document.getElementById('reason').setAttribute('readonly', 'readonly');

        // Desabilita os botões da página.
        document.getElementById('save-button').disabled = true;
        document.getElementById('cancel-button').classList.add('disabled');

    });


    /**
     * Evento disparado ao pressionar alguma tecla/atalho de teclado. As teclas tratadas são:
     * 
     * CTRL + S: Salvar os campos da página.
     * 
     * ESC: Cancelar a edição.
     */
    document.addEventListener('keydown', function (event) {

        if (event.ctrlKey && event.key.toLowerCase() === 's') {
            // CTRL + S
            event.preventDefault();
            document.getElementById('save-button').click();
        } else if (event.key === 'Escape') {
            // ESC
            event.preventDefault();
            document.getElementById('cancel-button').click();
        }

    });


});


/**
 * Tratador do evento de seleção do livro no componente.
 * 
 * @param {any} event evento associado.
 */
function onSelectedBook(event) {

    // Lista com os livros cadastrados no banco de dados.
    var books = window.booksData;

    for (var i = 0; i < books.length; i++) {

        var book = books[i];

        // Compara o livro no índice i com o que está selecionado no componente.
        if (book.Id == event.target.value) {

            // Executa uma chamada à API /Api/CollectionApi/GetBookCover para recuperar a capa do
            // livro, no formato string base 64. Isto é necessário pois a lista books não traz a
            // capa do livro, por questão de otimização no carregamento da página.

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

                // Obtém o componente de vizualização da capa e atualiza a imagem do mesmo.

                const imgPreview = document.getElementById("img-preview");
                imgPreview.style.display = "block";
                imgPreview.innerHTML = '<img src="' + cover.Data + '" class="center"/>';
                imgPreview.removeAttribute("hidden");

                // Preenche os campos ocultos na página com os dados do livro e capa.

                setBookData(document, book, cover);

            }).catch(error => {

                alert(error);

            });

            break;

        }

    }

}
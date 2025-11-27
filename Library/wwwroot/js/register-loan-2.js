
/**
 * Tratador de eventos da página de cadastro de empréstimo (2) (~\Views\Loan\Register2.cshtml).
 */


import { formatToDatetimeLocal } from "./module-datetime.js"
import { setPersonData } from "./module-person-data.js"
import { setBookData } from "./module-book-data.js"


document.addEventListener('DOMContentLoaded', function () {


    // Componente para seleção do tomador de empréstimo.
    var personSelector = document.getElementById('select-person');

    // Componente para seleção do livro a ser emprestado.
    var bookSelector = document.getElementById('select-book');

    // Move o foco para o componente de seleção do livro a ser emprestado.
    bookSelector.focus();

    // Atribui os tratadores de eventos aos componentes.
    personSelector.addEventListener('change', onSelectedPerson);
    bookSelector.addEventListener('change', onSelectedBook);

    // Força a execução dos eventos nos componentes, para atualizar a página.
    personSelector.dispatchEvent(new Event('change'));
    bookSelector.dispatchEvent(new Event('change'));

    // Atribui a data atual ao campo Data do empréstimo.
    document.getElementById("loan-date").value = formatToDatetimeLocal(new Date());


    /**
     * Evento disparado ao submeter o form. Neste caso, desabilita os campos de edição, os
     * botões e exibe o spinner, mostrando que há um processo em execução.
     */
    document.getElementById('borrow-book-form').addEventListener('submit', function (event) {

        // Exibe o spinner.
        document.getElementById('spinner').style.display = 'block';
        document.body.style.overflow = 'hidden';

        // Desabilita os campos da página.
        document.getElementById('select-book').setAttribute('readonly', 'readonly');
        document.getElementById('img-preview').setAttribute('readonly', 'readonly');
        document.getElementById('select-person').setAttribute('readonly', 'readonly');
        document.getElementById('loan-date').setAttribute('readonly', 'readonly');
        document.getElementById('notes').setAttribute('readonly', 'readonly');

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
 * Tratador do evento de seleção do tomador de empréstimo no componente.
 * 
 * @param {any} event evento associado.
 */
function onSelectedPerson(event) {

    // Lista de pessoas cadastradas no banco de dados e listadas no componente.
    var persons = window.personsData;

    for (var i = 0; i < persons.length; i++) {

        var person = persons[i];

        // Compara o Id da pessoa no índice i com o do tomador selecionado.
        if (person.Id == event.target.value) {

            // Preenche os campos ocultos na página com os dados do tomador.
            setPersonData(document, person);

            break;

        }

    }

}


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
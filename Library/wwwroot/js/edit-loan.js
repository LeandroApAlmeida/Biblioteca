
/*
    Tratador de eventos da página de edição de um empréstimo (~\Views\Loan\Edit.cshtml).
*/


import { setPersonData } from "./module-person-data.js"


document.addEventListener('DOMContentLoaded', function () {


    // Componente para seleção do tomador do empréstimo.
    var personSelector = document.getElementById('select-person');

    // Move o foco para o componente.
    personSelector.focus()


    // Seleciona o índice do tomador do empréstimo no componente. Neste caso, verifica o Id
    // do tomador e seleciona no componente o índice com o mesmo Id.

    // Lista de pessoas cadastradas no banco de dados e listadas no componente.
    var personsData = window.personsData;
    // Tomador do empréstimo cadastrado.
    var personData = window.personData;

    for (var i = 0; i < personsData.length; i++) {

        var person = personsData[i];

        // Compara o Id da pessoa no índice i com o do tomador.
        if (person.Id == personData.Id) {

            // Seleciona o índice do tomador do empréstimo.
            personSelector.options.selectedIndex = i;

            // Preenche os campos ocultos na página com os dados do tomador.
            setPersonData(document, person);

            break;

        }

    }


    /**
     * Evento disparado ao selecionar um novo tomador no componente para seleção do tomador
     * do empréstimo.
     */
    personSelector.addEventListener('change', function () {

        // Lista de pessoas cadastradas no banco de dados e listadas no componente.
        var persons = window.personsData;

        for (var i = 0; i < persons.length; i++) {

            var person = persons[i];

            // Compara o Id da pessoa no índice i com o do novo tomador selecionado no componente.
            if (person.Id == personSelector.value) {

                // Preenche os campos ocultos na página com os dados do novo tomador.
                setPersonData(document, person);

                break;

            }

        }

    });


    /**
     * Evento disparado ao submeter o form. Neste caso, desabilita os campos de edição, os
     * botões e exibe o spinner, mostrando que há um processo em execução.
     */
    document.getElementById('borrow-book-form').addEventListener('submit', function (event) {

        // Exibe o spinner.
        document.getElementById('spinner').style.display = 'block';
        document.body.style.overflow = 'hidden';

        // Desabilita os campos da página.
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
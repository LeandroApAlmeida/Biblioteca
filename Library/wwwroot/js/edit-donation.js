
/**
 * Tratador de eventos da página de edição de uma doação (~\Views\Donation\Edit.cshtml).
 */


import { setPersonData } from "./module-person-data.js"


document.addEventListener('DOMContentLoaded', function () {


    // Componente para seleção do beneficiário da doação.
    var personSelector = document.getElementById('select-person');

    // Move o foco para o componente.
    personSelector.focus();


    // Seleciona o índice do beneficiário da doação no componente. Neste caso, verifica o Id
    // do beneficiário e seleciona no componente o índice com o mesmo Id.

    // Lista de pessoas cadastradas no banco de dados e listadas no componente.
    var personsData = window.personsData;
    // Beneficiário da doação cadastrado.
    var personData = window.personData;

    for (var i = 0; i < personsData.length; i++) {

        var person = personsData[i];

        // Compara o Id da pessoa no índice i com o do beneficiário.
        if (person.Id == personData.Id) {

            // Seleciona o índice do beneficiário da doação.
            personSelector.options.selectedIndex = i;

            // Preenche os campos ocultos na página com os dados do beneficiário.
            setPersonData(document, person);

            break;

        }

    }


    /**
     * Evento disparado ao selecionar um novo beneficiário no componente para seleção do beneficiário
     * da doação.
     */
    personSelector.addEventListener('change', function () {

        // Lista de pessoas cadastradas no banco de dados e listadas no componente.
        var persons = window.personsData;

        for (var i = 0; i < persons.length; i++) {

            var person = persons[i];

            // Compara o Id da pessoa no índice i com o do novo beneficiário selecionado no componente.
            if (person.Id == personSelector.value) {

                // Preenche os campos ocultos na página com os dados do novo beneficiário.
                setPersonData(document, person);

                break;

            }

        }

    });


    /**
     * Evento disparado ao submeter o form. Neste caso, desabilita os campos de edição, os
     * botões e exibe o spinner, mostrando que há um processo em execução.
     */
    document.getElementById('donate-book-form').addEventListener('submit', function (event) {

        // Exibe o spinner.
        document.getElementById('spinner').style.display = 'block';
        document.body.style.overflow = 'hidden';

        // Desabilita os campos da página.
        document.getElementById('img-preview').setAttribute('readonly', 'readonly');
        document.getElementById('select-person').setAttribute('readonly', 'readonly');
        document.getElementById('donation-date').setAttribute('readonly', 'readonly');
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

/**
 * Tratador de eventos da página de cadastro de doação (~\Views\Donation\Register.cshtml).
 */


import { formatToDatetimeLocal } from "./module-datetime.js"
import { setPersonData } from "./module-person-data.js"


document.addEventListener('DOMContentLoaded', function () {


    // Componente para seleção do beneficiário da doação.
    var personSelector = document.getElementById("select-person");

    // Atribui o tratador de evento ao componente.
    personSelector.addEventListener('change', onSelectedPerson);

    // Força a execução do evento no componente, para atualizar o beneficiário na página.
    personSelector.dispatchEvent(new Event('change'));

    // Move o foco para o componente.
    personSelector.focus();

    // Atribui a data atual ao campo Data da doação.
    document.getElementById("donation-date").value = formatToDatetimeLocal(new Date());


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


/**
 * Tratador do evento de seleção do beneficiário no componente.
 * 
 * @param {any} event evento associado.
 */
function onSelectedPerson(event) {

    // Lista de pessoas cadastradas no banco de dados e listadas no componente.
    var persons = window.personsData;

    for (var i = 0; i < persons.length; i++) {

        var person = persons[i];

        // Compara o Id da pessoa no índice i com o do beneficiário selecionado.
        if (person.Id == event.target.value) {

            // Preenche os campos ocultos na página com os dados do beneficiário.
            setPersonData(document, person);

            break;

        }

    }

}
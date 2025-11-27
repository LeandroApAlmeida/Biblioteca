
/**
 * Tratador de eventos da página de retorno de empréstimo (~\Views\Loan\Return.cshtml).
 */


import { formatToDatetimeLocal } from "./module-datetime.js"


document.addEventListener('DOMContentLoaded', function () {


    // Campo para seleção da data do retorno do empréstimo.
    var returnDate = document.getElementById("return-date");

    // Move o foco para o componente.
    returnDate.focus();

    // Atribui a data atual ao campo Data do retorno do empréstimo.
    returnDate.value = formatToDatetimeLocal(new Date());


    /**
     * Evento disparado ao submeter o form. Neste caso, desabilita os campos de edição, os
     * botões e exibe o spinner, mostrando que há um processo em execução.
     */
    document.getElementById('return-book-form').addEventListener('submit', function (event) {

        // Exibe o spinner.
        document.getElementById('spinner').style.display = 'block';
        document.body.style.overflow = 'hidden';

        // Desabilita os campos da página.
        document.getElementById('img-preview').setAttribute('readonly', 'readonly');
        document.getElementById('return-date').setAttribute('readonly', 'readonly');
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
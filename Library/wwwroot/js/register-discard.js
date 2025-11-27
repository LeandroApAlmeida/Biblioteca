
/**
 * Tratador de eventos da página de cadastro de livro descartado (~\Views\Discard\Register.cshtml).
 */


import { formatToDatetimeLocal } from "./module-datetime.js"


document.addEventListener('DOMContentLoaded', function () {


    // Campo Data do Descarte.
    var discardDate = document.getElementById("discard-date");

    // Move o foco para o campo Data do Descarte.
    discardDate.focus();

    // Atribui a data atual ao campo Data do Descarte.
    discardDate.value = formatToDatetimeLocal(new Date());


    /**
     * Evento disparado ao submeter o form. Neste caso, desabilita os campos de edição, os
     * botões e exibe o spinner, mostrando que há um processo em execução.
     */
    document.getElementById('discard-book-form').addEventListener('submit', function (event) {

        // Exibe o spinner.
        document.getElementById('spinner').style.display = 'block';
        document.body.style.overflow = 'hidden';

        // Desabilita os campos da página.
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
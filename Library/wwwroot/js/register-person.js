
/**
 * Tratador de eventos das páginas cadastro de pessoa (~\Views\Person\Register.cshtml) e edição de
 * pessoa (~\Views\Person\Edit.cshtml).
 */


document.addEventListener('DOMContentLoaded', function () {


    // Move o foco para o campo nome.
    document.getElementById('name').focus();


    /**
     * Evento disparado ao submeter o form. Neste caso, desabilita os campos de edição, os
     * botões e exibe o spinner, mostrando que há um processo em execução.
     */
    document.getElementById('person-form').addEventListener('submit', function (event) {

        // Exibe o spinner.
        document.getElementById('spinner').style.display = 'block';
        document.body.style.overflow = 'hidden';

        // Desabilita os campos da página.
        document.getElementById('name').setAttribute('readonly', 'readonly');
        document.getElementById('street').setAttribute('readonly', 'readonly');
        document.getElementById('number').setAttribute('readonly', 'readonly');
        document.getElementById('complement').setAttribute('readonly', 'readonly');
        document.getElementById('district').setAttribute('readonly', 'readonly');
        document.getElementById('postal-code').setAttribute('readonly', 'readonly');
        document.getElementById('city').setAttribute('readonly', 'readonly');
        document.getElementById('federal-state').setAttribute('readonly', 'readonly');
        document.getElementById('cowntry').setAttribute('readonly', 'readonly');
        document.getElementById('description').setAttribute('readonly', 'readonly');

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
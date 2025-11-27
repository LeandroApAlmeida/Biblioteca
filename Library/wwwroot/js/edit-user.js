
/**
 * Tratador de eventos da página de edição de um usuário (~\Views\User\Edit.cshtml). O processamento
 * deste script atribui uma máscara aos campos de senha, com 4 caracteres, apenas para efeito visual,
 * para não dar a impressão que o campo de senha não foi definido para o usuário.
 * 
 * Quando o usuário dirige o foco para um dos campos de senha, apaga a máscara, fazendo o campo ficar
 * vazio. Ao sair de foco, se o usuário não digitou a nova senha, atribui novamente a máscara, caso
 * contrário, mantém os caracteres que o usuário digitou para a senha. Como a senha deve ter pelo menos
 * 5 caracteres, uma máscara com 4 caracteres não gera qualquer tipo de conflito.
 */


// Máscara para a senha.
const passwordMask = '####';


document.addEventListener('DOMContentLoaded', function () {


    // Move o foco para o componente nome.
    document.getElementById('name').focus();


    // Campo de edição da senha.
    var passwordField = document.getElementById('password');

    // Campo de confirmação da senha.
    var cPasswordField = document.getElementById('conf-password');

    // Atribui a máscara aos componentes de edição da senha.
    passwordField.value = passwordMask;
    cPasswordField.value = passwordMask;

    // Atribui os tratadores de eventos de focus e de blur aos componentes de edição da senha.
    passwordField.addEventListener('focus', handleFocus);
    cPasswordField.addEventListener('focus', handleFocus);
    passwordField.addEventListener('blur', handleBlur);
    cPasswordField.addEventListener('blur', handleBlur);


    /**
     * Evento disparado ao submeter o form. Neste caso, configura a senha de acordo com a máscara. Se
     * a senha for a máscara, a senha fica "-", para indicar ao backend que não trocará a senha atual.
     */
    document.getElementById('user-form').addEventListener('submit', function (event) {

        if (passwordField.value === passwordMask) {
            passwordField.value = "-";
        }

        if (cPasswordField.value === passwordMask) {
            cPasswordField.value = "-";
        }

    });


    /**
     * Evento disparado ao submeter o form. Neste caso, desabilita os campos de edição, os
     * botões e exibe o spinner, mostrando que há um processo em execução.
     */
    document.getElementById('user-form').addEventListener('submit', function (event) {

        // Exibe o spinner.
        document.getElementById('spinner').style.display = 'block';
        document.body.style.overflow = 'hidden';

        // Desabilita os campos da página.
        document.getElementById('name').setAttribute('readonly', 'readonly');
        document.getElementById('user-name').setAttribute('readonly', 'readonly');
        document.getElementById('password').setAttribute('readonly', 'readonly');
        document.getElementById('conf-password').setAttribute('readonly', 'readonly');

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
 * Componente recebe o foco. Neste caso, apaga a máscara de senha e deixa o campo vazio.
 * @param {any} event evento associado.
 */
function handleFocus(event) {
    if (event.target.value === passwordMask) {
        event.target.value = "";
    }
}


/**
 * Componente perde o foco. Neste caso, caso o campo esteja vazio, restaura a máscara da senha.
 * @param {any} event evento associado.
 */
function handleBlur(event) {
    if (event.target.value === "") {
        event.target.value = passwordMask;
    }
}
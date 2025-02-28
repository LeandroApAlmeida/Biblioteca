
/*
    
    Tratador de eventos da página de edição de um usuário (~\Views\User\Edit.cshtml). O processamento
    deste script atribui uma máscara aos campos de senha, com 4 caracteres, apenas para efeito visual,
    para não dar a impressão que o campo de senha não foi definido para o usuário.
    
    Quando o usuário dirige o foco para um dos campos de senha, apaga a máscara, fazendo o campo ficar
    vazio. Ao sair de foco, se o usuário não digitou a nova senha, atribui novamente a máscara, caso
    contrário, mantém os caracteres que o usuário digitou para a senha. Como a senha deve ter pelo menos
    5 caracteres, uma máscara com 4 caracteres não gera qualquer tipo de conflito.

*/


const passwordMask = '####';


document.addEventListener('DOMContentLoaded', function () {

    document.getElementById('name').focus();

    var passwordField = document.getElementById('password');
    var cPasswordField = document.getElementById('conf-password');

    passwordField.value = passwordMask;
    cPasswordField.value = passwordMask;

    passwordField.addEventListener('focus', handleFocus);
    cPasswordField.addEventListener('focus', handleFocus);
    passwordField.addEventListener('blur', handleBlur);
    cPasswordField.addEventListener('blur', handleBlur);

    document.getElementById('user-form').addEventListener('submit', function (event) {

        if (passwordField.value === passwordMask) {
            passwordField.value = "-";
        }

        if (cPasswordField.value === passwordMask) {
            cPasswordField.value = "-";
        }

    });

    document.getElementById('user-form').addEventListener('submit', function (event) {

        document.getElementById('spinner').style.display = 'block';
        document.body.style.overflow = 'hidden';

        document.getElementById('name').setAttribute('readonly', 'readonly');
        document.getElementById('user-name').setAttribute('readonly', 'readonly');
        document.getElementById('password').setAttribute('readonly', 'readonly');
        document.getElementById('conf-password').setAttribute('readonly', 'readonly');

        document.getElementById('save-button').disabled = true;
        document.getElementById('cancel-button').classList.add('disabled');

    });

    document.addEventListener('keydown', function (event) {
        if (event.ctrlKey && event.key.toLowerCase() === 's') {
            event.preventDefault();
            document.getElementById('save-button').click();
        } else if (event.key === 'Escape') {
            event.preventDefault();
            document.getElementById('cancel-button').click();
        }
    });

});


function handleFocus(event) {
    if (event.target.value === passwordMask) {
        event.target.value = "";
    }
}


function handleBlur(event) {
    if (event.target.value === "") {
        event.target.value = passwordMask;
    }
}
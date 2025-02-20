﻿
/*
    
    Tratador de eventos da página de edição de um usuário (~\Views\User\Edit.cshtml). O processamento
    deste script atribui uma máscara aos campos de senha, com 4 caracteres, apenas para efeito visual,
    para não dar a impressão que o campo de senha não foi definido para o usuário.
    
    Quando o usuário dirige o foco para um dos campos de senha, apaga a máscara, fazendo o campo ficar
    vazio. Ao sair de foco, se o usuário não digitou a nova senha, atribui novamente a máscara, caso
    contrário, mantém os caracteres que o usuário digitou para a senha. Como a senha deve ter pelo menos
    5 caracteres, uma máscara com 4 caracteres não gera qualquer tipo de conflito.

*/


// Máscara para o campo vazio.

var passwordMask = '####';


// Campo senha

var passwordField = document.getElementById('password');

// Campo confirmação da senha.

var cPasswordField = document.getElementById('conf-password');


// Atribui a máscara aos campos de senha vazios.

passwordField.value = passwordMask;
cPasswordField.value = passwordMask;


// Atribui os tratadores de eventos aos campos de senha.

passwordField.addEventListener('focus', handleFocus);
cPasswordField.addEventListener('focus', handleFocus);
passwordField.addEventListener('blur', handleBlur);
cPasswordField.addEventListener('blur', handleBlur);


// No post da página, se os campos de senha estiverem com a máscara, envia o caractere "-" para
// indicar para o servidor que não houve modificação da senha.

document.getElementById('user-form').addEventListener('submit', function (event) {

    if (passwordField.value === passwordMask) {
        passwordField.value = "-";
    }

    if (cPasswordField.value === passwordMask) {
        cPasswordField.value = "-";
    }

});


/**
 * Evento foco recebido. Limpa o campo de edição da senha.
 * @param {any} event evento
 */
function handleFocus(event) {
    if (event.target.value === passwordMask) {
        event.target.value = "";
    }
}


/**
 * Evento foco perdido. Se o campo está vazio, Atribui a máscara novamente.
 * @param {any} event evento
 */
function handleBlur(event) {
    if (event.target.value === "") {
        event.target.value = passwordMask;
    }
}
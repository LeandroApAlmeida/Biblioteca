
// Tratador de eventos da página ~\Views\User\Edit.cshtml.


var passwordMask = '####';


var passwordField = document.getElementById('password');
var cPasswordField = document.getElementById('conf-password');


passwordField.value = passwordMask;
cPasswordField.value = passwordMask;


passwordField.addEventListener('focus', handleFocus);
cPasswordField.addEventListener('focus', handleFocus);
passwordField.addEventListener('blur', handleBlur);
cPasswordField.addEventListener('blur', handleBlur);


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


// No post, se os campos de senha estiverem vazios, envia o caractere "-", para indicar ao servidor
// que não houve modificação da senha.
document.getElementById('user-form').addEventListener('submit', function (event) {

    if (passwordField.value === passwordMask) {
        passwordField.value = "-";
    }

    if (cPasswordField.value === passwordMask) {
        cPasswordField.value = "-";
    }

});
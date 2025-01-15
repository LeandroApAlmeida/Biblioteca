
var passwordMask = document.getElementById('password').value;


var passwordField = document.getElementById('password')
var cPasswordField = document.getElementById('conf-password')


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
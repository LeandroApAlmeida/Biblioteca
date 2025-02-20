
/*
    Tratador de eventos da página de cadastro de livro descartado (~\Views\Discard\Register.cshtml).
*/


import { formatToDatetimeLocal } from "./utils.js"


document.addEventListener('DOMContentLoaded', function () {

    document.getElementById("discard-date").value = formatToDatetimeLocal(new Date());

});
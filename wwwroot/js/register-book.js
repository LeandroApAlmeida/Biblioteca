
/*
    Tratador de eventos da página de cadastro de um livro (~\Views\Book\Register.cshtml).
*/


import { formatToDatetimeLocal } from "./utils.js"
import { setCoverData } from "./data-cover.js"


document.addEventListener('DOMContentLoaded', function () {

    document.getElementById("acquisition-date").value = formatToDatetimeLocal(new Date());
    document.getElementById('edition').value = 1;
    document.getElementById('volume').value = 1;
    document.getElementById('release-year').value = 2000;
    document.getElementById('num-of-pages').value = 1;

    const chooseFile = document.getElementById("choose-file");

    chooseFile.addEventListener("change", function () {

        const imgPreview = document.getElementById("img-preview");
        const imgData = document.getElementById("cover-data");

        setCoverData(chooseFile.files[0], imgPreview, imgData);

    });

    document.getElementById('book-form').addEventListener('submit', function (event) {

        document.getElementById('title').setAttribute('readonly', 'readonly');
        document.getElementById('subtitle').setAttribute('readonly', 'readonly');
        document.getElementById('author').setAttribute('readonly', 'readonly');
        document.getElementById('publisher').setAttribute('readonly', 'readonly');
        document.getElementById('isbn').setAttribute('readonly', 'readonly');
        document.getElementById('edition').setAttribute('readonly', 'readonly');
        document.getElementById('volume').setAttribute('readonly', 'readonly');
        document.getElementById('release-year').setAttribute('readonly', 'readonly');
        document.getElementById('num-of-pages').setAttribute('readonly', 'readonly');
        document.getElementById('acquisition-date').setAttribute('readonly', 'readonly');
        document.getElementById('summary').setAttribute('readonly', 'readonly');
        document.getElementById('choose-file').disabled = true;

        document.getElementById('save-button').disabled = true;
        document.getElementById('cancel-button').classList.add('disabled');

    });

});
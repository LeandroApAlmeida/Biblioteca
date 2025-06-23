
/*
    Tratador de eventos da página de cadastro de um livro (~\Views\Book\Register.cshtml).
*/


import { formatToDatetimeLocal } from "./module-datetime.js"
import { setCoverData } from "./module-cover-data.js"


document.addEventListener('DOMContentLoaded', function () {


    document.getElementById('title').focus();

    document.getElementById("acquisition-date").value = formatToDatetimeLocal(new Date());
    document.getElementById('edition').value = 1;
    document.getElementById('volume').value = 1;
    document.getElementById('release-year').value = 2000;
    document.getElementById('num-of-pages').value = 1;


    document.getElementById("choose-file").addEventListener("change", function (event) {
        const imgPreview = document.getElementById("img-preview");
        const imgData = document.getElementById("cover-data");
        setCoverData(event.target.files[0], imgPreview, imgData);
    });


    document.getElementById('book-form').addEventListener('submit', function (event) {

        document.getElementById('spinner').style.display = 'block';
        document.body.style.overflow = 'hidden';

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
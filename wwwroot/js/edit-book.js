
/*
    Tratador de eventos da página para edição de um livro (~\Views\Book\Edit.cshtml).
*/


import { setCoverData } from "./data-cover.js"


// Caixa de diálogo para localizar o arquivo de capa no navegador.

const chooseFile = document.getElementById("choose-file");


// Atribui o tratador de evento para que quando o arquivo for selecionado, se faça o
// processamento do mesmo.

chooseFile.addEventListener("change", function () {

    const imgPreview = document.getElementById("img-preview");
    const imgData = document.getElementById("cover-data");

    setCoverData(chooseFile.files[0], imgPreview, imgData);

});


// Atribui o tratador de evento para que quando o post for realizado, os campos da
// página sejam desabilitados.

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